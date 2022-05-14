import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DeliveryserviceService } from 'src/app/services/deliveryservice/deliveryservice.service';

@Component({
  selector: 'app-deliveryservice-detail',
  templateUrl: './deliveryservice-detail.component.html',
  styleUrls: ['./deliveryservice-detail.component.scss']
})
export class DeliveryserviceDetailComponent implements OnInit {

  public deliveryserviceForm: FormGroup;
  deliveryserviceId:number;

  constructor(

    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _deliveryserviceService : DeliveryserviceService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params=>{
      this.deliveryserviceId = +params.id;
  })

  this.deliveryserviceForm = this.createDeliveryServiceForm()

}
  /*
 *Creating Delivery service Form
 */
 createDeliveryServiceForm():FormGroup
 {
   return this._formBuilder.group({
     id:[0],
     name: [null, Validators.required],
     address:[[null], Validators.required],
     email:[null, Validators.required],
     telephoneNo: [null, Validators.required],
     deliveryDetails: [null, Validators.required],
     
   })
 }
  
 onFileChange(event:any, type:number){

 }

 public onSubmit(){
  console.log(this.deliveryserviceForm.value);
}

/*
*Getters
*/
get id()
{
  return this.deliveryserviceForm.get('id').value;
}
}

