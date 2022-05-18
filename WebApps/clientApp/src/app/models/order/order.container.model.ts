import { Injectable } from '@angular/core';
import { ProductModel } from 'src/app/models/product/product.model';
@Injectable()
export class OrderContainerModel{
    
    orderItems:ProductModel[] = [];
    cardNumber:string;
    experationDate:string;
    paymentType:number;
    cvv:string;
    shippingAddress:string;
    city: string;
    postalCode: string;
    amount:number;
}