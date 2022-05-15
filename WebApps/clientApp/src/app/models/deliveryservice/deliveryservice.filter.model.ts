import { Injectable } from '@angular/core';
@Injectable()

export class DeliveryServiceFilterModel
{
    deliveryserviceId:number;
    searchText:string;
    currentPage:number;
    pageSize:number;
       
}