import { Injectable } from '@angular/core';
import { ProductImageModel } from './../product/product.image.model';
@Injectable()
export class OrderItemModel
{
    Id:number;
    productId:number;
    orderId:number;
    numberOfItems:number;

    productImage:ProductImageModel;
}