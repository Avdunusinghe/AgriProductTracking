import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  public productForm: FormGroup;

  private sub: any;
 

  constructor(public _formBuilder: FormBuilder, private activatedRoute: ActivatedRoute ) { }

  ngOnInit(): void {
  
   this.productForm = this.createProductForm();

  }

  
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
 
  public onSubmit(){
    console.log(this.productForm.value);
  }

  //getters
  get id()
  {
    return this.productForm.get('id').value;
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  } 



}
