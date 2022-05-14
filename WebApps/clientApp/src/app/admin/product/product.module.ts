import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { SwiperModule } from 'ngx-swiper-wrapper';
import { InputFileModule } from 'ngx-input-file';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
export const routes = [ 
  { 
    path: '',
    redirectTo: 'product-list', 
    pathMatch: 'full'
  },
  { 
    path: 'product-list', 
    component: ProductListComponent, 
    data: { breadcrumb: 'Product List' } 
  },
  {
     path: 'product-detail', 
     component: ProductDetailComponent, 
     data: { breadcrumb: 'Product Detail' } 
    },
  { 
    path: 'product-detail/:id', 
    component: ProductDetailComponent, 
    data: { breadcrumb: 'Product Detail' } 
  },

];

@NgModule({
  declarations: [
    ProductListComponent,
    ProductDetailComponent
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
export class ProductModule { }
