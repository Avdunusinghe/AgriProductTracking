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

  getAllDeliveryServices():Observable<DropDownModel[]>
  {
    return this.httpClient.get<DropDownModel[]>
    (environment.apiUrl + 'CoreData/getAllDeliveryServices');
  }

  

}
