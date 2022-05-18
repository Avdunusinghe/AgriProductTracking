import { Injectable } from '@angular/core';

@Injectable()
export class UserTokenModel
{
    isLoginSuccess:boolean;
    loginMessage:string;
    token:string;
    displayName:string;
    roles:[];
}