import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { SwiperModule } from 'ngx-swiper-wrapper';
import { InputFileModule } from 'ngx-input-file';

export const routes = [ 
  { path: '', redirectTo: 'user-list', pathMatch: 'full'},
  { path: 'user-list', component: UserListComponent, data: { breadcrumb: 'User List' } },
  { path: 'user-detail', component: UserDetailComponent, data: { breadcrumb: 'User Detail' } },

];



@NgModule({
  declarations: [
    UserListComponent,
    UserDetailComponent
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
export class UserModule { }
