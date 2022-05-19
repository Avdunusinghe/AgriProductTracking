import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AppService } from 'src/app/app.service';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';
import { CoreDataService } from 'src/app/services/core-data/core-data.service';
import { OrderService } from 'src/app/services/order/order.service';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  @ViewChild('horizontalStepper', { static: true }) horizontalStepper: MatStepper;
  @ViewChild('verticalStepper', { static: true }) verticalStepper: MatStepper;

  billingForm: FormGroup;
  deliveryForm: FormGroup;
  paymentForm: FormGroup;

  grandTotal = 0;
  orderContainer :OrderContainerModel;
  paymentTypes:DropDownModel[]=[];
  deliveryMethods = [];
  constructor
  (
    public _appService:AppService, 
    public _formBuilder: FormBuilder,
    private _coreDataService:CoreDataService,
    private _orderService: OrderService,
    private _spinner: NgxSpinnerService,
    private _toastr: ToastrService,
    private _router:Router
    
  ) 
  {
    this.orderContainer = new OrderContainerModel();
  }

  ngOnInit(): void 
  {
    this._appService.Data.cartList.forEach(product=>{
      this.orderContainer.orderItems.push(product);
      this.grandTotal += product.cartCount*product.price;
    });

    this.getPaymentType();

    this.billingForm = this._formBuilder.group({
      shippingAddress: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      paymentType:[[null],Validators.required],
      cardNumber:['',Validators.required],
      experationDate:['',Validators.required],
      cvv:['',Validators.required]
    });

  }

  checkOut(){

    let item = new OrderContainerModel();
    item = this.billingForm.getRawValue();

    this.orderContainer.cardNumber = item.cardNumber;
    this.orderContainer.experationDate = item.experationDate;
    this.orderContainer.paymentType = item.paymentType;
    this.orderContainer.cvv = item.cvv;
    this.orderContainer.shippingAddress = item.shippingAddress;
    this.orderContainer.city = item.city;
    this.orderContainer.postalCode = item.postalCode;
    this.orderContainer.amount = this.grandTotal;

    this._orderService.checkOutOrder(this.orderContainer).subscribe((response)=>{
        console.log(response);
        
      if(response.isSuccess === true)
      {
        if(this.paymentType === 1){
          this._orderService.SendPaymentSuccessMesseage(response).subscribe((apiResponse)=>{
            if(apiResponse.isSuccess)
            {
              this._toastr.success(apiResponse.message,"Success");
              this._router.navigate(['products']);
              
            }
          })
        }
        else
        {
          this._orderService.SendMobilePaymentSuccessMessage(response).subscribe((apiSmsResponse)=>{
            if(apiSmsResponse.isSuccess)
            {
              this._toastr.success(apiSmsResponse.message,"Success");
              this._router.navigate(['products']);
            }
          })
        }
       
      }
        
    }) 

   
    
  }

  getPaymentType(){
    this._spinner.show();
    this._coreDataService.getPaymentType().subscribe((response)=>{
      this.paymentTypes = response;
      this.getDeliveryMethods();
    },(error)=>{
      this._spinner.hide();
    })
  }

   getDeliveryMethods()
   {
     //this._spinner.show();
     this.deliveryMethods = this._coreDataService.getDeliveryMethods()
   }
   /*
 *Getters
 */
  get paymentType()
  {
    return this.billingForm.get('paymentType').value;
  }

}
