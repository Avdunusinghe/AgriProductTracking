import { Component, OnInit } from '@angular/core';
import { UserTokenModel } from 'src/app/models/auth/user.token.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ProductModel } from './../../models/product/product.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public slides = [
    { title: 'The biggest sale', subtitle: 'Special for today', image: 'assets/images/carousel/banner1.jpg' },
    { title: 'Summer collection', subtitle: 'New Arrivals On Sale', image: 'assets/images/carousel/banner2.jpg' },
    { title: 'The biggest sale', subtitle: 'Special for today', image: 'assets/images/carousel/banner3.jpg' },
    { title: 'Summer collection', subtitle: 'New Arrivals On Sale', image: 'assets/images/carousel/banner4.jpg' },
    { title: 'The biggest sale', subtitle: 'Special for today', image: 'assets/images/carousel/banner5.jpg' },
    { title: 'The biggest sale', subtitle: 'Special for today', image: 'assets/images/carousel/banner6.jpg' },
    { title: 'The biggest sale', subtitle: 'Special for today', image: 'assets/images/carousel/banner7.jpg' }
  ];

  public brands = [];
  public banners = [];
  public featuredProducts: Array<ProductModel>;
  public onSaleProducts: Array<ProductModel>;
  public topRatedProducts: Array<ProductModel>;
  public newArrivalsProducts: Array<ProductModel>;

  currentUser?:UserTokenModel;
  role:string;
  isManagementLeveluser:boolean;
  isLoggedInUser:boolean = false;
  constructor(private _authService : AuthService) { }

  ngOnInit(): void {

    this.getCurrentUser();
    this.isManagementLeveluser = this.isUseRoleExsits("Admin") || 
                                 this.isUseRoleExsits("SuperAdmin") || 
                                 this.isUseRoleExsits("Farmer");

  }

  onLinkClick(event){

  }
  
  getCurrentUser(){
    this.currentUser = this._authService.currentUserValue;
    
    
  }

  isUseRoleExsits(role:string):boolean
  {
    for (let index = 0; index < this.currentUser.roles.length; index++) {
      if(this.currentUser.roles[index] === role)
      {
        return true;
      }
      
    } 

   /* for(var item of Object.keys(this.currentUser.roles)){
      if(item === role)
      {
        return true;
      }
    }*/

    return false;
  }
}