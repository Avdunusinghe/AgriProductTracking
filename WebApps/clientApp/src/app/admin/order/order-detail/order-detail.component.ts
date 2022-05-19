import { isNgTemplate } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';
import { OrderModel } from 'src/app/models/order/order.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
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
    private _coreDataService : CoreDataService,
    private _orderService : OrderService,
    private _spinner: NgxSpinnerService,
    private _toastr: ToastrService,
    private _router:Router,
  )
   { 
     this.order = new OrderModel();
     this.getAllDeliveryServices();
   }

  ngOnInit(): void {

    this._activatedRoute.params.subscribe(params=>{
      this.orderId = +params.id;
      
      if(this.orderId > 0)
      {
        this.getOrderById();
        this.orderForm = this.createExistingOrderForm()
      }
      
      
  })

}

createExistingOrderForm():FormGroup
{
  return this._formBuilder.group({
    id:[this.order.id],
    amount: [{value:this.order.amount}],
    cutomerName:[{value:this.order.cutomerName}],
    dateTime: [{value:this.order.dateTime}],
    isProcessed: [{value:this.order.isProcessed}],
    shippingAdderess: [{value:this.order.shippingAdderess}],
    city: [{value:this.order.city}],
    postalCode: [{value:this.order.postalCode}],
    deliveryPartnerId: [[null], Validators.required] 
  })
}


/*
*Get DropDown delivery roles
*/
getAllDeliveryServices()
{
   this._spinner.show();
   this._coreDataService.getAllDeliveryServices()
     .subscribe(response=>{
     this.deliveryServices= response;
     console.log(response);
     
   },(error)=>{
     this._spinner.hide();
   })    
}

confirmOrder()
  {
     this._spinner.show();

     let item = new OrderModel();
     item = this.orderForm.getRawValue();
     
     this._orderService.confirmOrder(item.id, item.deliveryPartnerId).subscribe((response)=>{
       
        if(response.isSuccess)
        {
          this._orderService.sendDeliveryPatnerMessage(response).subscribe((smsApiResponse)=>{
            if(smsApiResponse.isSuccess)
            {
                this._toastr.success(smsApiResponse.message,"Success");
                this._router.navigate(["admin/order/order-list"]);
            }
          },(error)=>{
            this._spinner.hide();
          });
        }
    },(error)=>{
      this._spinner.hide();
    }); 
    
  }

  getOrderById()
  {
    this._spinner.show();
    this._orderService.getOrderById(this.orderId).subscribe((response)=>{
       this.order = response;
    
       this.orderForm.get("id").setValue(response.id);
       this.orderForm.get("amount").setValue(response.amount);
       this.orderForm.get("cutomerName").setValue(response.cutomerName);
       this.orderForm.get("dateTime").setValue(response.dateTime);
       this.orderForm.get("shippingAdderess").setValue(response.shippingAdderess);
       this.orderForm.get("city").setValue(response.city);
       this.orderForm.get("postalCode").setValue(response.postalCode);
        
    },(error)=>{
      this._spinner.hide();
    })
  }

}
