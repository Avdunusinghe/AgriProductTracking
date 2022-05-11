import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DropDownModel } from './../../models/common/drop.down.model';
import { ProductModel } from './../../models/product/product.model';
import { ResponseModel } from './../../models/common/response.model';
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
