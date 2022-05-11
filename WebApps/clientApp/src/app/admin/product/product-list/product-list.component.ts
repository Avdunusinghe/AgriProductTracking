import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  productFilterForm:FormGroup;
  productCategories:DropDownModel[]=[];

  constructor(public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _coreDataService : CoreDataService,
    private _spinner: NgxSpinnerService,
    private _productService:ProductService,
    private _toastr: ToastrService) { }

    currentPage: number = 0;
    pageSize: number = 10;
    totalRecord: number = 0;

  ngOnInit(): void {
    this.getAllProductCategories();
    this.productFilterForm = this.createProductFilterForm();
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


  /*
 *Create Product Filter Form
 */


 createProductFilterForm():FormGroup{
   return this._formBuilder.group({
     searchText: new FormControl(""),
     categoryId:new FormControl(""),
   })
 }

 /*
 *CategoryFIlter chaged Fire Ivent
 */


onCategoryIdChanged(item:any){
  this.currentPage = 0;
  this.pageSize = 0;
  this.totalRecord = 0;
  this._spinner.show();
}


  getAllProductDetails(){
    this._spinner.show();

  }



  /*
  *Getters
  */

  
  get categoryFilterId()
  {
    return this.productFilterForm.get("categoryId").valid;
  }

  get searchFilterId()
  {
    return this.productFilterForm.get("searchText").valid;
  }

}
