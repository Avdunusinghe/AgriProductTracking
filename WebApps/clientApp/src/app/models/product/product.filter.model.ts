import { Injectable } from '@angular/core';
@Injectable()
export class ProductFilterModel
{
    categoryId:number;
    searchText:string;
    currentPage:number;
    pageSize:number;
       
}