import { Component, OnInit } from '@angular/core';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  total = [];
  grandTotal = 0;
  cartItemCount = [];
  cartItemCountTotal = 0;

  constructor
  (
    public _appService:AppService
  ) { }

  ngOnInit(): void {
    this._appService.Data.cartList.forEach(product=>{
      console.log(product);
      this.total[product.id] = product.cartCount*product.price;
      this.grandTotal += product.cartCount*product.price;
      this.cartItemCount[product.id] = product.cartCount;
      this.cartItemCountTotal += product.cartCount;
  })


  
}

public updateCart(value){
  if(value){
    this.total[value.productId] = value.total;
    this.cartItemCount[value.productId] = value.soldQuantity;
    this.grandTotal = 0;
    this.total.forEach(price=>{
      this.grandTotal += price;
    });
    this.cartItemCountTotal = 0;
    this.cartItemCount.forEach(count=>{
      this.cartItemCountTotal +=count;
    });
   
    this._appService.Data.totalPrice = this.grandTotal;
    this._appService.Data.totalCartCount = this.cartItemCountTotal;

    this._appService.Data.cartList.forEach(product=>{
      this.cartItemCount.forEach((count,index)=>{
        if(product.id == index){
          product.cartCount = count;
        }
      });
    });
    
  }
}
public remove(product) {
  const index: number = this._appService.Data.cartList.indexOf(product);
  if (index !== -1) {
    this._appService.Data.cartList.splice(index, 1);
    this.grandTotal = this.grandTotal - this.total[product.id]; 
    this._appService.Data.totalPrice = this.grandTotal;       
    this.total.forEach(val => {
      if(val == this.total[product.id]){
        this.total[product.id] = 0;
      }
    });

    this.cartItemCountTotal = this.cartItemCountTotal - this.cartItemCount[product.id]; 
    this._appService.Data.totalCartCount = this.cartItemCountTotal;
    this.cartItemCount.forEach(val=>{
      if(val == this.cartItemCount[product.id]){
        this.cartItemCount[product.id] = 0;
      }
    });
    this._appService.resetProductCartCount(product);
  }
}

public clear(){
  this._appService.Data.cartList.forEach(product=>{
    this._appService.resetProductCartCount(product);
  });
  this._appService.Data.cartList.length = 0;
  this._appService.Data.totalPrice = 0;
  this._appService.Data.totalCartCount = 0;
} 

}
