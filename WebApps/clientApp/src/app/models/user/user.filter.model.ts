import { Injectable } from '@angular/core';
@Injectable()

export class UserFilterModel
{
    roleId:number;
    searchText:string;
    currentPage:number;
    pageSize:number;
       
}