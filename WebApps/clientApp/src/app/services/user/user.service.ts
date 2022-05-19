import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { UserModel } from './../../models/user/user.model';
import { ResponseModel } from './../../models/common/response.model';
import { Observable } from 'rxjs';
import { UserFilterModel } from 'src/app/models/user/user.filter.model';
import { UserPaginatedItemModel } from 'src/app/models/user/user.paginated.item';
import { ClientModel } from 'src/app/models/user/client.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor( private httpClient: HttpClient) {  }

  getAllRoles():Observable<DropDownModel[]>
  {
    return this.httpClient.get<DropDownModel[]>
      (environment.apiUrl + 'User/getAllRoles');
  }

  saveUser(model:UserModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
      (environment.apiUrl + 'User', model);
    
  }

  deleteUser(id:number):Observable<ResponseModel>
  {
    return this.httpClient.delete<ResponseModel>
      (environment.apiUrl + "User" + "/" + id)
  }

  getUserById(id:number):Observable<UserModel>
  {
    return this.httpClient.get<UserModel>
     (environment.apiUrl + "User/getUserById" + "/" + id);
  }

  getUserList(filter:UserFilterModel):Observable<UserPaginatedItemModel>
  {
    return this.httpClient.post<UserPaginatedItemModel>
    (environment.apiUrl + "User/getUserList" , filter);
  }

  registerClient(model:ClientModel):Observable<ResponseModel>
  {
    return this.httpClient.post<ResponseModel>
      (environment.apiUrl + 'User/registerClient', model);
    
  }


}
