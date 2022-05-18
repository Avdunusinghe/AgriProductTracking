import { Injectable } from '@angular/core';

@Injectable()
export class OrderModel
{
    id:number;
    amount:number;
    deliverySeviceId:number[];
    cutomerId:number;
    cutomerName:string;
    dateTime:Date;
    isProcessed:boolean;
    shippingAdderess:string; 
    city:string;
    postalCode:string;
}