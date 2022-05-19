import { Injectable } from '@angular/core';
@Injectable()
export class OrderConfirmResponseModel
{
    isSuccess:boolean;
    message:string;
    deliveryPartnerId:number;
    deliveryServiceEmail:string;
    deliveryServicePhoneNumber:string;

}