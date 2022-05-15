import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { DeliveryServiceModel } from './deliveryservice.mode';


@Injectable()
export class DeliveryServicePaginatedItemModel extends PaginatedItemsModel{
    data:DeliveryServiceModel[];
}