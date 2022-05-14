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
}
