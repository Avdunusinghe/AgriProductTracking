import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
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
 

  constructor
  (
    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _coreDataService : CoreDataService 
  ) 
  {

  }

  ngOnInit(): void {
   this._activatedRoute.params.subscribe(params=>{
     this.productId = +params.id;
     
   })
   this.getAllProductCategories();
   this.productForm = this.createProductForm();

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
  
 onFileChange(event:any, type:number){

 }

 /*
 *Get DropDown MasterData
 */
 getAllProductCategories()
 {
    this._coreDataService.getAllProductCategories()
      .subscribe((response)=>{
      this.productCategories = response;
    })   
 }

  public onSubmit(){
    console.log(this.productForm.value);
  }

 /*
 *Getters
 */
  get id()
  {
    return this.productForm.get('id').value;
  }

  ngOnDestroy() 
  {
    this.sub.unsubscribe();
  } 



}
