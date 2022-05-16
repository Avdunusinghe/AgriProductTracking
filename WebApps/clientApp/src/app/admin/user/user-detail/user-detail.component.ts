import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { UserModel } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {


  public userForm: FormGroup;
  userRoles:DropDownModel[]=[];
  userId:number;
  user:UserModel;

  /*
* Constructor Dependency Injection
*/
  constructor
  (
    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _userService : UserService,
    private _spinner: NgxSpinnerService,
    private _toastr: ToastrService,
    private _router:Router,
  )
   {
      this.getAllRoles();
   }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params=>{
      this.userId = +params.id;

      
      if(this.userId > 0)
      {
        this.getUserById();
      }
      
      this.userForm = this.createUserForm();   

  }) 
}


/*
 *Creating Initial User Form
 */
 createUserForm():FormGroup
 {
   return this._formBuilder.group({
     id:[0],
     fullName: [null, Validators.required],
     email:[null, Validators.required],
     address:[[null], Validators.required],
     mobileNumber: [null, Validators.required],
     username: [null, Validators.required],
     password: [null, Validators.required],
     roleId: [[null], Validators.required]
   })
 }

 createExistingUserForm():FormGroup
 {
   return this._formBuilder.group({
     id:[this.user.id],
     fullName: [{value:this.user.fullName},, Validators.required],
     email:[{value:this.user.email},, Validators.required],
     address:[{value:this.user.address},, Validators.required],
     mobileNumber: [{value:this.user.mobileNumber},, Validators.required],
     username: [{value:this.user.username},, Validators.required],
     password: [{value:this.user.password},, Validators.required],
     roleId: [{value:this.user.roles},, Validators.required]
   })
 }
  
 onFileChange(event:any, type:number){

 }


 /*
 *Get DropDown user roles
 */
 getAllRoles()
 {
   this._spinner.show();
    this._userService.getAllRoles()
      .subscribe(response=>{
      this.userRoles = response;
    },(error)=>{
      this._spinner.hide();
    })   
 }


 saveUser()
  {
    this._spinner.show();
    if(this.userForm.valid){
      this._userService.saveUser(this.userForm.getRawValue()).subscribe((response)=>{
        if(response.isSuccess){
            this._toastr.success(response.message, "success");
            this._router.navigate(["admin/user/user-list"])
        }
        else{
          this._toastr.success(response.message, "error");
        }
      },(error)=>{
        this._spinner.hide();
      })
    }  
  }

   /*
 *Get Product By Id
 */

 getUserById()
 {
   this._spinner.show();
   this._userService.getUserById(this.userId).subscribe((response)=>{
      this.user = response;

      this.userForm.get("id").setValue(response.id);
      this.userForm.get("fullName").setValue(response.fullName);
      this.userForm.get("email").setValue(response.email);
      this.userForm.get("address").setValue(response.address);
      this.userForm.get("mobileNumber").setValue(response.mobileNumber);
      this.userForm.get("username").setValue(response.username);
      this.userForm.get("password").setValue(response.password);
      this.userForm.get("roleId").setValue(response.roles);
      
   },(error)=>{
     this._spinner.hide();
   })
 }

 /*
 *Getters

  get id()
  {
    return this.userForm.get('id').value;
  }
   */
}