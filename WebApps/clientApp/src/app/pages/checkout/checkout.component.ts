import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { AppService } from 'src/app/app.service';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';

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
  orderContainer :OrderContainerModel
  constructor
  (
    public _appService:AppService, 
    public _formBuilder: FormBuilder
    
  ) 
  {
    this.orderContainer = new OrderContainerModel();
  }

  ngOnInit(): void {
    this._appService.Data.cartList.forEach(product=>{
      this.orderContainer.productItems.push(product);
      this.grandTotal += product.cartCount*product.price;
    });
    this.billingForm = this._formBuilder.group({
      shippingAddress: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
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
    this.orderContainer.cvv = item.cvv;
    this.orderContainer.shippingAddress = item.shippingAddress;
    this.orderContainer.city = item.city;
    this.orderContainer.postalCode = item.postalCode;

    console.log(this.orderContainer);
    
    
    
  }

}
