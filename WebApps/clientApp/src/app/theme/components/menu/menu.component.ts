import { Component, OnInit, Input} from '@angular/core';
import { UserTokenModel } from 'src/app/models/auth/user.token.model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  
  constructor(private _authService : AuthService) { }

  currentUser?:UserTokenModel;
  role:string;
  isManagementLeveluser:boolean;
  isLoggedInUser:boolean = false;

  ngOnInit() {
    this.getCurrentUser();
    this.isManagementLeveluser = this.isUseRoleExsits("Admin") || 
                                 this.isUseRoleExsits("SuperAdmin") || 
                                 this.isUseRoleExsits("Farmer");

    
    
   }

  openMegaMenu(){
    let pane = document.getElementsByClassName('cdk-overlay-pane');
    [].forEach.call(pane, function (el) {
        if(el.children.length > 0){
          if(el.children[0].classList.contains('mega-menu')){
            el.classList.add('mega-menu-pane');
          }
        }        
    });
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
