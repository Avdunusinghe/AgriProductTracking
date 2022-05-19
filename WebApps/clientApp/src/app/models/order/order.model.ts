import { Injectable } from '@angular/core';
import { OrderItemModel } from './order.item.model';
@Injectable()
export class OrderModel
{
    id:number;
    amount:number;
    deliveryPartnerId:number;
    cutomerId:number;
    cutomerName:string;
    dateTime:Date;
    isProcessed:boolean;
    shippingAdderess:string; 
    city:string;
    postalCode:string;
    
    orderItems:OrderItemModel[];
}