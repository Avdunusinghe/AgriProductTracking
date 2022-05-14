import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeliveryserviceListComponent } from './deliveryservice-list/deliveryservice-list.component';
import { DeliveryserviceDetailComponent } from './deliveryservice-detail/deliveryservice-detail.component';
import { ToastrComponentlessModule, ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { SwiperModule } from 'ngx-swiper-wrapper';
import { InputFileModule } from 'ngx-input-file';



export const routes = [ 
  { path: '', redirectTo: 'deliveryservice-list', pathMatch: 'full'},
  { path: 'deliveryservice-list', component: DeliveryserviceListComponent, data: { breadcrumb: 'Delivery-Service List' } },
  { path: 'deliveryservice-detail', component: DeliveryserviceDetailComponent, data: { breadcrumb: 'Delivery-Service Detail' } },

];


@NgModule({
  declarations: [
    DeliveryserviceListComponent,
    DeliveryserviceDetailComponent
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
export class DeliveryserviceModule { }
