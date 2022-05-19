import { Injectable } from '@angular/core';
import { ProductImageModel } from './product.image.model';

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
    productImages:ProductImageModel[];

    cartCount: number;
    ratingsCount: number;
    ratingsValue: number;
    availibilityCount: number;
}