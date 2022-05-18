import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DropDownModel } from './../../models/common/drop.down.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoreDataService {

  constructor( private httpClient: HttpClient) { }

  getAllProductCategories():Observable<DropDownModel[]>
  {
    return this.httpClient.get<DropDownModel[]>
      (environment.apiUrl + 'CoreData/getAllProductCatagories');
  }

  getPaymentType():Observable<DropDownModel[]>
  {
    return this.httpClient.get<DropDownModel[]>
      (environment.apiUrl + 'CoreData/getPaymentType');
  }

  getDeliveryMethods(){
    return [
        { value: 'free', name: 'Free Delivery', desc: '$0.00 / Delivery in 7 to 14 business Days' },
        { value: 'standard', name: 'Standard Delivery', desc: '$5.00 / Delivery in 5 to 7 business Days' },
        
    ]
}
  

}
