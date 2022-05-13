import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { ProductModel } from 'src/app/models/product/product.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
import { ProductService } from 'src/app/services/product/product.service';
import { ProductFilterModel } from './../../../models/product/product.filter.model';
import { ConfirmDialogComponent } from './../../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  productFilterForm:FormGroup;
  productCategories:DropDownModel[]=[];
  rowData = new Array<ProductModel>();
  
  public searchText: string;
  constructor
  (
    private _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _coreDataService : CoreDataService,
    private _spinner: NgxSpinnerService,
    private _productService:ProductService,
    private _toastr: ToastrService,
    private _router:Router,
    private _dialog: MatDialog

  ) 
  { 

  }

    currentPage: number = 0;
    pageSize: number = 5;
    totalRecord: number = 0;

  ngOnInit(): void {
    this.getAllProductCategories();
    this.productFilterForm = this.createProductFilterForm();
  }

  /*
 *Get DropDown MasterData (Product Categories)
 */

 getAllProductCategories()
 {
   this._spinner.show();
    this._coreDataService.getAllProductCategories()
      .subscribe(response=>{
      this.productCategories = response;
      this.getAllProductDetails()
    },(error)=>{
      this._spinner.hide();
    })   
 }


  /*
 *Create Product Filter Form
 */


 createProductFilterForm():FormGroup{
   return this._formBuilder.group({
     searchText: new FormControl(""),
     categoryId:new FormControl(0),
   })
 }

 /*
 *Add New Product Route ProductDetail Compenent
 */

 addNewProduct()
 {
    this._router.navigate(["admin/product/product-detail"]);
 }

 /*
 *Update Product Route with param ProductDetail Compenent
 */

 updateProduct(id:number)
 {
    this._router.navigate(["admin/product/product-detail", id]);
 }

  /*
 *Delete Product
 */

 deleteProduct(id:number)
 {
    const dialogRef = this._dialog.open(ConfirmDialogComponent,{
      maxWidth: "400px",
      data: {
        title: "Confirm Delete Product",
        message: "Are you sure you want remove this Product?"
      }
    });
    dialogRef.afterClosed().subscribe(dialogResult => { 
      if(dialogResult){
       this._productService.productDelete(id).subscribe((response)=>{
          if(response.isSuccess)
          {
             this._toastr.success(response.message,"success");
             this.getAllProductDetails();
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
 *Category Filter Changed Fire Event
 */

onCategoryIdChanged(item:any)
{
  this.currentPage = 0;
  this.pageSize = 0;
  this.totalRecord = 0;
  this._spinner.show();
  this.getAllProductDetails();

}

/*
 *Search Filter Changed Fire Event
 */

filterDataTable(event)
{
  this.currentPage = 0;
  this.pageSize = 10;
  this._spinner.show();
  this.getAllProductDetails();

}

/*
 *Page Number Changed Fire Event
 */

onPageChanged(pageInfo)
{
  this._spinner.show();
  console.log(pageInfo);
  
  this.currentPage = pageInfo;
  this.getAllProductDetails();
}

/*
 *Get All Product Details
 */

  getAllProductDetails()
  {
    this._spinner.show();

    let filter = new ProductFilterModel();

    filter.categoryId = this.categoryFilterId;
    filter.searchText = this.searchFilter;
    filter.currentPage = this.currentPage + 1;
    filter.pageSize = this.pageSize;

    this._productService.getAllProductDerails(filter).subscribe((response)=>{
      this.rowData = response.data;
      this.totalRecord = response.totalRecordCount;
    },(error)=>{
      this._spinner.hide();
      this._toastr.error("Network error has been occured. Please try again.","error");
    });

  }


  /*
  *Getters
  */
  get categoryFilterId()
  {
    return this.productFilterForm.get("categoryId").value;
  }

  get searchFilter()
  {
    return this.productFilterForm.get("searchText").value;
  }

}
