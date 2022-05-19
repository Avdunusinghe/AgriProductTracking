import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { UserModel } from './user.model';

@Injectable()
export class UserPaginatedItemModel extends PaginatedItemsModel{
    data:UserModel[];
}