import { Injectable } from '@angular/core';
import { ProductImageViewModel } from './product.image.model';

@Injectable()
export class ProductModel
{
    id:number;
    name:string;
    description:string;
    categoryId:number;
    price:number;
    quantity:number;
    createdByName:string;
    updatedByName:string;
    productImages:ProductImageViewModel[];

    cartCount: number;
    ratingsCount: number;
    ratingsValue: number;
    availibilityCount: number;
}