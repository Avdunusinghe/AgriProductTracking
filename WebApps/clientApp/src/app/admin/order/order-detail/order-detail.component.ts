import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';
import { OrderModel } from 'src/app/models/order/order.model';
import { DeliveryserviceService } from 'src/app/services/deliveryservice/deliveryservice.service';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {

  public orderForm: FormGroup;
  deliveryServices:DropDownModel[]=[];
  orderId:number;
  order:OrderModel;

   /*
* Constructor Dependency Injection
*/
  constructor(

    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _deliveryserviceService : DeliveryserviceService,
    private _orderService : OrderService,
    private _spinner: NgxSpinnerService,
    private _toastr: ToastrService,
    private _router:Router,
  )
   { 
     this.getAllDeliveryServices();
   }

  ngOnInit(): void {

    this._activatedRoute.params.subscribe(params=>{
      this.orderId = +params.id;

      
      if(this.orderId > 0)
      {
        this.getOrderById();
      }
      
      
  })

}
createExistingOrderForm():FormGroup
{
  return this._formBuilder.group({
    id:[this.order.id],
    amount: [{value:this.order.amount},, Validators.nullValidator],
    cutomerId:[{value:this.order.cutomerId},, Validators.nullValidator],
    cutomerName:[{value:this.order.cutomerName},, Validators.nullValidator],
    dateTime: [{value:this.order.dateTime},, Validators.nullValidator],
    isProcessed: [{value:this.order.isProcessed},, Validators.nullValidator],
    shippingAdderess: [{value:this.order.shippingAdderess},, Validators.nullValidator],
    city: [{value:this.order.city},, Validators.nullValidator],
    postalCode: [{value:this.order.postalCode},, Validators.nullValidator],
    deliveryServiceId: [[null], Validators.required] 
  })
}

onFileChange(event:any, type:number){

}


/*
*Get DropDown delivery roles
*/
getAllDeliveryServices()
{
   this._spinner.show();
   this._deliveryserviceService.getAllDeliveryServices()
     .subscribe(response=>{
     this.deliveryServices= response;
   },(error)=>{
     this._spinner.hide();
   })    
}

confirmOrder()
  {
    this._spinner.show();
    this._orderService.confirmOrder(this.orderId, this.deliveryService).subscribe((response)=>{
        if(response.isSuccess)
        {
          this._toastr.success(response.message);
        }
    })
    
  }



  get deliveryService()
  {
    return this.orderForm.get("deliveryServiceId").value;
  }

  getOrderById()
  {
    this._spinner.show();
    this._orderService.getOrderById(this.orderId).subscribe((response)=>{
       this.order = response;
 
       this.orderForm.get("id").setValue(response.id);
       this.orderForm.get("amount").setValue(response.amount);
       this.orderForm.get("cutomerId").setValue(response.cutomerId);
       this.orderForm.get("cutomerName").setValue(response.cutomerName);
       this.orderForm.get("dateTime").setValue(response.dateTime);
       this.orderForm.get("shippingAdderess").setValue(response.shippingAdderess);
       this.orderForm.get("city").setValue(response.city);
       this.orderForm.get("postalCode").setValue(response.postalCode);
       this.orderForm.get(" deliveryServiceId").setValue(response.deliverySeviceId);

      
       
    },(error)=>{
      this._spinner.hide();
    })
  }

}
