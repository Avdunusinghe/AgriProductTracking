import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { EMPTY, Observable } from 'rxjs';
import { ProductModel } from 'src/app/models/product/product.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
import { ProductService } from 'src/app/services/product/product.service';
import { DropDownModel } from './../../../models/common/drop.down.model';
import { Upload } from 'src/app/models/common/upload';
import { HttpEventType } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  public productForm: FormGroup;
  productCategories:DropDownModel[]=[];
  private sub: any;
  productId:number;
  product:ProductModel;
 
/*
* Contructor Dependency Injection
*/
  constructor
  (
    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _coreDataService : CoreDataService,
    private _spinner: NgxSpinnerService,
    private _productService:ProductService,
    private _toastr: ToastrService,
    private _router:Router,
    private _dialog: MatDialog
  ) 
  {
    this.getAllProductCategories();
  }

  ngOnInit(): void 
  {
  
   this._activatedRoute.params.subscribe(params=>{
     this.productId = +params.id; 

      if(this.productId > 0)
      {
        this.getproductById();
      }
      else{
        this.product = new ProductModel();
      }
      this.productForm = this.createProductForm();   

   })  
  }

 /*
 *Creating Initial Product Form
 */
 createProductForm():FormGroup
 {
   return this._formBuilder.group({
     id:[0],
     name: [null, Validators.required],
     description:[null, Validators.required],
     categoryId:[[null], Validators.required],
     price: [null, Validators.required],
     quantity: [null, Validators.required]
   })
 }

 createExsitingProductForm():FormGroup
 {
   return this._formBuilder.group({
    id:[this.product.id],
    name: [{value:this.product.name}, Validators.required],
    description:[{value:this.product.description}, Validators.required],
    categoryId:[{value:this.product.categoryId}, Validators.required],
    price: [{value:this.product.price}, Validators.required],
    quantity: [{value:this.product.quantity}, Validators.required]
   });
 }

 upload$: Observable<Upload> = EMPTY;
 precentage:any;
 onFileChange(event:any, type:number)
 {   
   let file = event.srcElement;
   const formData = new FormData();

   formData.set("id",this.product.id.toString());
   formData.set("type", type.toString());

   if(file.files.length > 0)
   {
      this._spinner.show();

      for (let index = 0; index < file.files.length; index++) 
      {
        formData.append('file',file.files[index], file.files[index].name);
        
      }

      this._productService.uploadProductImage(formData).subscribe((response)=>{
          this.precentage = response;
          if(response.state === "DONE")
          {
            this._spinner.show();
            this.getproductById();

            this._toastr.success("Image has been uploaded successfully", 'Success');
          }
      },(error)=>{
        this._spinner.hide();
        this._toastr.error("Error has been occured image upload please try again","Eroor");
      })
   }
 }

 downloadPercentage:number = 0;
 isDownloading:boolean;
 downloadProductImage(id:number, attachmentName:string)
 {
    //this._spinner.show();
    this.isDownloading = true;

    this._productService.downloadProductImage(id).subscribe((response)=>{

      if (response.type === HttpEventType.DownloadProgress) {
        this.downloadPercentage = Math.round(100 * response.loaded / response.total);
      }
      
      if (response.type === HttpEventType.Response) {
        if(response.status == 204)
        {
          this.isDownloading=false;
          this.downloadPercentage=0;
          this._spinner.hide();
        }
        else
        {
          const objectUrl: string = URL.createObjectURL(response.body);
          const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;
  
          a.href = objectUrl;
          a.download = attachmentName;
          document.body.appendChild(a);
          a.click();
  
          document.body.removeChild(a);
          URL.revokeObjectURL(objectUrl);
          this.isDownloading=false;
          this.downloadPercentage=0;
          this._spinner.hide();
        }

      }
    },(error)=>{

      this._spinner.hide();
      this.isDownloading=false;
      this.downloadPercentage=0;

    });

 }

 deleteProductImage(id:number)
 {
  const dialogRef = this._dialog.open(ConfirmDialogComponent,{
    maxWidth: "400px",
    data: {
      title: "Confirm Delete Product Image",
      message: "Are you sure you want remove this product image?"
    }
  });
  dialogRef.afterClosed().subscribe(dialogResult => { 
    if(dialogResult){
     this._productService.deleteProductImage(id).subscribe((response)=>{
        if(response.isSuccess)
        {
           this._toastr.success(response.message,"success");
           this.getproductById();
        }else
        {
          this._toastr.error(response.message,"error")
        }
     })
    } 
  },(error)=>{
    this._toastr.error("Network error has been occured. Please try again.","error")
  }); 
 }

 /*
 *Get DropDown MasterData
 */
 getAllProductCategories()
 {
   this._spinner.show();
    this._coreDataService.getAllProductCategories()
      .subscribe(response=>{
      this.productCategories = response;
    },(error)=>{
      this._spinner.hide();
    })   
 }

  saveProduct()
  {
    this._spinner.show();
    if(this.productForm.valid){
      this._productService.saveProduct(this.productForm.getRawValue()).subscribe((response)=>{
        if(response.isSuccess){
            this._toastr.success(response.message, "success");
            this._router.navigate(["admin/product/product-list"])
        }
        else{
          this._toastr.success(response.message, "error");
        }
      },(error)=>{
        this._spinner.hide();
      })
    }  
  }

   /*
 *Get Product By Id
 */

 getproductById()
 {
   this._spinner.show();
   this._productService.getProductById(this.productId).subscribe((response)=>{
      this.product = response;

      this.productForm.get("id").setValue(response.id);
      this.productForm.get("name").setValue(response.name);
      this.productForm.get("description").setValue(response.description);
      this.productForm.get("categoryId").setValue(response.categoryId);
      this.productForm.get("price").setValue(response.price);
      this.productForm.get("quantity").setValue(response.quantity);
      
   },(error)=>{
     this._spinner.hide();
   })
 }

 /*
 *Getters
 */
  /*get id()
  {
    return this.productForm.get('id').value;
  }*/
}
