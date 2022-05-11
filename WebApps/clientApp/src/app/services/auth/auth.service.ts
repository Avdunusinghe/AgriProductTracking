import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { LoginModel } from 'src/app/models/auth/login.model';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { UserModel } from './../../models/user/user.model';
import { UserTokenModel } from 'src/app/models/auth/user.token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<UserTokenModel>;
  public currentUser: Observable<UserTokenModel>;

  constructor
  (
    private httpClient: HttpClient
  ) 
  { 
    this.currentUserSubject = new BehaviorSubject<UserTokenModel>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserTokenModel {
    return this.currentUserSubject.value;
  }
  
  login(loginModel : LoginModel):Observable<any>{
    return this.httpClient.post<any>(environment.apiUrl + "Auth/login", loginModel).pipe(map((UserModel)=>{
      localStorage.setItem("currentUser",JSON.stringify(UserModel));
      this.currentUserSubject.next(UserModel);
      return UserModel;
    }))
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    return of({ success: false });
  }
}
