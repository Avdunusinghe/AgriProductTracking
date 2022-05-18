import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CustomerOrderResponseModel } from 'src/app/models/order/customer.order.response.model';
import { ResponseModel } from 'src/app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor
  (
    private httpClient: HttpClient
  ) 
  { 

  }
  checkOutOrder(orderContainer:OrderContainerModel):Observable<CustomerOrderResponseModel>{
      return this.httpClient.post<CustomerOrderResponseModel>
      (environment.paymentApiUrl + 'PaymentService',orderContainer);
  }

  SendPaymentSuccessMesseage(model:CustomerOrderResponseModel):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>
    (environment.smsApiUrl + "CreaditCardClientResponse",model);
  }
}
