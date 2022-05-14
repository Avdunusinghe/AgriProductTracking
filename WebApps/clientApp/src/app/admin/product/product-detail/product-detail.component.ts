import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ProductModel } from 'src/app/models/product/product.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
import { ProductService } from 'src/app/services/product/product.service';
import { DropDownModel } from './../../../models/common/drop.down.model';

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
  
 onFileChange(event:any, type:number){

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
