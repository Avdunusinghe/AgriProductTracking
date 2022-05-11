import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/common/response.model';
import { ProductModel } from 'src/app/models/product/product.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor( private httpClient: HttpClient) { }

  saveProduct(model:ProductModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
      (environment.apiUrl + 'Product', model);
    
  }

  productDelete(id:number):Observable<ResponseModel>
  {
    return this.httpClient.delete<ResponseModel>
      (environment.apiUrl + "Product" + "/" + id)
  }

  getProductById(id:number,productCategoryId:number):Observable<ProductModel>
  {
    return this.httpClient.get<ProductModel>
     (environment.apiUrl + "Product" + "/" + id + "/" + productCategoryId);
  }
}
