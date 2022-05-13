import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { ProductModel } from './product.model';
@Injectable()
export class ProductPaginatedItemModel extends PaginatedItemsModel{
    data:ProductModel[];
}