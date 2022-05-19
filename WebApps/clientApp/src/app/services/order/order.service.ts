import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CustomerOrderResponseModel } from 'src/app/models/order/customer.order.response.model';
import { ResponseModel } from 'src/app/models/common/response.model';
import { OrderModel } from 'src/app/models/order/order.model';
import { OrderConfirmResponseModel } from 'src/app/models/order/order.confirm.response.model';

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
  checkOutOrder(orderContainer:OrderContainerModel):Observable<CustomerOrderResponseModel>
  {
      return this.httpClient.post<CustomerOrderResponseModel>
      (environment.paymentApiUrl + 'PaymentService',orderContainer);
  }

  sendPaymentSuccessMesseage(model:CustomerOrderResponseModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
    (environment.smsApiUrl + "EmailSMSClientResponse",model);
  }

  sendMobilePaymentSuccessMessage(model:CustomerOrderResponseModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
    (environment.smsApiUrl + "EmailSMSClientResponse/sendMobilePaymentSuccessMessage", model);
  }

  sendDeliveryPatnerMessage(model:OrderConfirmResponseModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
    (environment.smsApiUrl + "EmailSMSClientResponse/sendDeliveryPatnerMessage", model);
  }

  gellAllProducts(id:number):Observable<OrderModel>
  {
    return this.httpClient.get<OrderModel>
     (environment.apiUrl + "Order/gellAllProducts" + "/" + id);
  }

  getAllOrders():Observable<OrderModel[]>
  {
    return this.httpClient.get<OrderModel[]>
     (environment.apiUrl + "Order/getAllOrders");
  }

  confirmOrder(orderId:number,deliveryPartnerId:number):Observable<OrderConfirmResponseModel>
  {
    return this.httpClient.get<OrderConfirmResponseModel>
      (environment.apiUrl + "Order/confirmOrder" + "/" +  orderId + "/" + deliveryPartnerId);
    
  }

  getOrderById(id:number):Observable<OrderModel>
  {
    return this.httpClient.get<OrderModel>
     (environment.apiUrl + "Order/getOrderById" + "/" + id);
  }


}
