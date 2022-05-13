import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/common/response.model';
import { upload, Upload } from 'src/app/models/common/upload';
import { ProductModel } from 'src/app/models/product/product.model';
import { ProductPaginatedItemModel } from 'src/app/models/product/product.paginated.item.model';
import { environment } from 'src/environments/environment';
import { ProductFilterModel } from './../../models/product/product.filter.model';

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

  getProductById(id:number):Observable<ProductModel>
  {
    return this.httpClient.get<ProductModel>
     (environment.apiUrl + "Product/getPrductById" + "/" + id);
  }

  getAllProductDerails(filter:ProductFilterModel):Observable<ProductPaginatedItemModel>
  {
    return this.httpClient.post<ProductPaginatedItemModel>
    (environment.apiUrl + "Product/getAllProducts" , filter);
  }

  uploadProductImage(formData:FormData):Observable<Upload>{
      return this.httpClient.post(environment.apiUrl + "Product/uploadProductImage", 
      formData,{reportProgress: true,observe: 'events'}).pipe(upload());
  }
  
}
