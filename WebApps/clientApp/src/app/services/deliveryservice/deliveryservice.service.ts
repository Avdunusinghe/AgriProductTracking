import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/common/response.model';
import { DeliveryServiceModel } from 'src/app/models/deliveryservice/deliveryservice.mode';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeliveryserviceService {

  constructor( private httpClient: HttpClient) { }

  deliveryServiceSave(model:DeliveryServiceModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
      (environment.apiUrl + 'DeliveryService', model);
    
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


  /*
  getDeliveryServiceList(filter:ProductFilterModel):Observable<ProductPaginatedItemModel>
  {
    return this.httpClient.post<ProductPaginatedItemModel>
    (environment.apiUrl + "Product/getAllProducts" , filter);
  }*/
}
