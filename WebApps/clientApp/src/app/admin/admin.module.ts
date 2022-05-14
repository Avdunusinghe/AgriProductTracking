import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

import { InputFileConfig, InputFileModule } from 'ngx-input-file';
const config: InputFileConfig = {
  fileAccept: '*'
};

import { AdminComponent } from './admin.component';
import { MenuComponent } from './components/menu/menu.component';
import { FullScreenComponent } from './components/fullscreen/fullscreen.component'; 
import { MessagesComponent } from './components/messages/messages.component';


import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component'; 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
export const routes = [ 
  { 
    path: '', 
    component: AdminComponent, children: [
      { path: '', loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule) }, 
      { path: 'product', loadChildren: () => import('./product/product.module').then(m => m.ProductModule) },
      { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule) },
      { path: 'deliveryservice', loadChildren: () => import('./deliveryservice/deliveryservice.module').then(m => m.DeliveryserviceModule) },
     
    ]
  } 
];

@NgModule({
  declarations: [
    AdminComponent,
    MenuComponent,
   
    FullScreenComponent,
    MessagesComponent,
    BreadcrumbComponent,
   
  ],
  imports: [
   
    ToastrModule.forRoot(),
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    InputFileModule.forRoot(config),
  ]
})
export class AdminModule { }
