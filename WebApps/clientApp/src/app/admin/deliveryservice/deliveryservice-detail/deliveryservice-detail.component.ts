import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DeliveryServiceModel } from 'src/app/models/deliveryservice/deliveryservice.model';
import { DeliveryserviceService } from 'src/app/services/deliveryservice/deliveryservice.service';

@Component({
  selector: 'app-deliveryservice-detail',
  templateUrl: './deliveryservice-detail.component.html',
  styleUrls: ['./deliveryservice-detail.component.scss']
})
export class DeliveryserviceDetailComponent implements OnInit {

  public deliveryserviceForm: FormGroup;
  deliveryserviceId:number;
  deliveryService : DeliveryServiceModel;

  constructor(

    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _deliveryserviceService : DeliveryserviceService,
    private _spinner: NgxSpinnerService,
    private _toastr: ToastrService,
    private _router:Router,
  ) { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params=>{
      this.deliveryserviceId = +params.id;


      if(this.deliveryserviceId > 0)
      {
        this.getDeliveryServicebyId();
      }
  this.deliveryserviceForm = this.createDeliveryServiceForm();
  })

  

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
     telePhoneNumber: [null, Validators.required],
     diliveryDetails: [null, Validators.required],
     
   })
 }

 createExsitingDEliveryServiceForm():FormGroup
 {
   return this._formBuilder.group({
    id:[this.deliveryService.id],
    name: [{value:this.deliveryService.name}, Validators.required],
    address:[{value:this.deliveryService.address}, Validators.required],
    email:[{value:this.deliveryService.email}, Validators.required],
    telePhoneNumber: [{value:this.deliveryService.telePhoneNumber}, Validators.required],
    diliveryDetails: [{value:this.deliveryService.diliveryDetails}, Validators.required],
    
   });
 }
  
 onFileChange(event:any, type:number){

 }

 saveDeliveryService()
  {
    this._spinner.show();
    if(this.deliveryserviceForm.valid){
      this._deliveryserviceService.deliveryServiceSave(this.deliveryserviceForm.getRawValue()).subscribe((response)=>{
        if(response.isSuccess){
            this._toastr.success(response.message, "success");
            this._router.navigate(["admin/deliveryservice/deliveryservice-list"])
        }
        else{
          this._toastr.success(response.message, "error");
        }
      },(error)=>{
        this._spinner.hide();
      })
    }  
  }

  getDeliveryServicebyId()
 {
   this._spinner.show();
   this._deliveryserviceService.getDeliveryServicebyId(this.deliveryserviceId).subscribe((response)=>{
      this.deliveryService = response;

      this.deliveryserviceForm.get("id").setValue(response.id);
      this.deliveryserviceForm.get("name").setValue(response.name);
      this.deliveryserviceForm.get("email").setValue(response.email);
      this.deliveryserviceForm.get("address").setValue(response.address);
      this.deliveryserviceForm.get("telePhoneNumber").setValue(response.telePhoneNumber);
      this.deliveryserviceForm.get("diliveryDetails").setValue(response.diliveryDetails);
     
   },(error)=>{
     this._spinner.hide();
   })
 }

/*
*Getters

get id()
{
  return this.deliveryserviceForm.get('id').value;
}
*/
}

