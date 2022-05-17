import { Injectable } from '@angular/core';
import { ProductModel } from 'src/app/models/product/product.model';
import { CreditCardModel } from './credit.card.model';
@Injectable()
export class OrderContainerModel{
    
    productItems:ProductModel[] = [];
    cardNumber:string;
    experationDate:string;
    cvv:string;
    shippingAddress:string;
    city: string;
    postalCode: string;
    amount:number;
}