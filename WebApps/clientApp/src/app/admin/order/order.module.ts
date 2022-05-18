import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { OrderListComponent } from './order-list/order-list.component';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { SwiperModule } from 'ngx-swiper-wrapper';
import { InputFileModule } from 'ngx-input-file';

export const routes = [ 
  { path: '', redirectTo: 'order-list', pathMatch: 'full'},
  { path: 'order-list', component: OrderListComponent, data: { breadcrumb: 'Order List' } },
  { path: 'order-detail', component: OrderDetailComponent, data: { breadcrumb: 'Order Detail' } },
  { path: 'order-detail/:id', component: OrderDetailComponent, data: { breadcrumb: 'Order Detail' } },


];

@NgModule({
  declarations: [
    OrderDetailComponent,
    OrderListComponent
  ],
  imports: [
    CommonModule,
    ToastrModule.forRoot(),
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    SharedModule,
    NgxPaginationModule,
    SwiperModule,
    InputFileModule
  ]
})
export class OrderModule { }
