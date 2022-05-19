import { Injectable } from '@angular/core';
@Injectable()
export class CustomerOrderResponseModel
{
    isSuccess:boolean;
    message:string;
    customerId:number;
    customerEmail:string;
    customerMobileNumber:string;
}