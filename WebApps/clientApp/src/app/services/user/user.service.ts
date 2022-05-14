import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { UserModel } from './../../models/user/user.model';
import { ResponseModel } from './../../models/common/response.model';
import { Observable } from 'rxjs';

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
}
