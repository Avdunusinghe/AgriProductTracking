import { Injectable } from '@angular/core';
import { NumberValueAccessor } from '@angular/forms';

@Injectable()
export class DeliveryServiceModel
{

  id : number;
  name :string ;
  address :  string;
  email : string ;
  telephoneNo :  string;
  deliveryDetails :string ;
  createdOn : Date;
  createdById :number
  createdByName :string;
  updatedOn : Date;
  updatedByName : string;
  updatedById: number;
 
}