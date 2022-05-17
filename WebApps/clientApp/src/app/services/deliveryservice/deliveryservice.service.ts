import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/common/response.model';
import { DeliveryServiceFilterModel } from 'src/app/models/deliveryservice/deliveryservice.filter.model';
import { DeliveryServiceModel } from 'src/app/models/deliveryservice/deliveryservice.model';
import { DeliveryServicePaginatedItemModel } from 'src/app/models/deliveryservice/deliveryservice.paginated.item.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeliveryserviceService {

  constructor( private httpClient: HttpClient) { }

  deliveryServiceSave(model:DeliveryServiceModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
      (environment.apiUrl + 'Deliveryservice/deliveryServiceSave', model);
    
  }

  deliveryServiceDelete(id:number):Observable<ResponseModel>
  {
    return this.httpClient.delete<ResponseModel>
      (environment.apiUrl + "Deliveryservice" + "/" + id)
  }

  getDeliveryServicebyId(id:number):Observable<DeliveryServiceModel>
  {
    return this.httpClient.get<DeliveryServiceModel>
     (environment.apiUrl + "Deliveryservice/getDeliveryServicebyId" + "/" + id);
  }

  getAllDeliveryServices():Observable<DeliveryServiceModel>
  {
    return this.httpClient.get<DeliveryServiceModel>
     (environment.apiUrl + "Deliveryservice/getAllDeliveryServices" + "/");
  }

  
  getDeliveryServiceList(filter:DeliveryServiceFilterModel):Observable<DeliveryServicePaginatedItemModel>
  {
    return this.httpClient.post<DeliveryServicePaginatedItemModel>
    (environment.apiUrl + "Deliveryservice/getDeliveryServiceList" , filter);
  }
}
