import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { ProductFilterModel } from 'src/app/models/product/product.filter.model';
import { ProductModel } from 'src/app/models/product/product.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  public sidenavOpen:boolean = true;
  public viewType: string = 'grid';
  public viewCol: number = 25;
  public counts = [12, 24, 36];
  public count:any;
  public sortings = ['Sort by Default', 'Best match', 'Lowest first', 'Highest first'];
  public sort:any;
  public brands = [];
  public priceFrom: number = 750;
  public priceTo: number = 1599;

  products = new Array<ProductModel>();
  productCategories:DropDownModel[]=[];

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
    pageSize: number = 10;
    totalRecord: number = 0;

  ngOnInit(): void {

    this.getAllProductCategories();
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
 *Get All Product Details
 */

 getAllProductDetails()
 {
   this._spinner.show();

   let filter = new ProductFilterModel();

   filter.categoryId = 0;
   filter.searchText = "";
   filter.currentPage = this.currentPage + 1;
   filter.pageSize = this.pageSize;

   this._productService.getAllProductDetails(filter).subscribe((response)=>{
     this.products = response.data;
     console.log(this.products);
     
     this.totalRecord = response.totalRecordCount;
   },(error)=>{
     this._spinner.hide();
     this._toastr.error("Network error has been occured. Please try again.","error");
   });

 }

 onPageChanged($event){

 }

 public changeViewType(viewType, viewCol){
  this.viewType = viewType;
  this.viewCol = viewCol;
}

}
