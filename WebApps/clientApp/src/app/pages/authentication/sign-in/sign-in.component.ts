import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { emailValidator, matchingPasswords } from 'src/app/theme/utils/app-validators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  authForm:FormGroup;
  clientRegisterForm:FormGroup;
/*
* Contructor Dependency Injection
*/
  constructor
  (
    public _formBuilder: FormBuilder, 
    public _router:Router, 
    public _snackBar: MatSnackBar,
    private _authService:AuthService,
    private _toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.authForm = this.createAuthForm();
    this.clientRegisterForm = this.createClientRegisterForm();
  }

  /*
  * Create Login Form
  */
   createAuthForm():FormGroup
   {
     return this._formBuilder.group({
       userName:["", Validators.compose([Validators.required, emailValidator])],
       password:["",Validators.compose([Validators.required, Validators.minLength(6)])]
     });
   }

  /*
  *Create Register Form
  */
   createClientRegisterForm():FormGroup
   {
     return this._formBuilder.group({
       id:[0],
       fullName:["",Validators.required],
       mobileNumber:["",Validators.required],
       email:["",Validators.compose([Validators.required, emailValidator])],
       password:["",Validators.required],
       confirmPassword:["",Validators.required],
     },{validator: matchingPasswords('password', 'confirmPassword')});
   }

   login(){
    if (this.authForm.valid) {  
      this._authService.login(this.authForm.getRawValue()).subscribe((response)=>{
        if(response.isLoginSuccess)
        {
          const token = this._authService.currentUserValue.token;
          if(token)
          {
            this._router.navigate(['/']);
          }
         
        }else{
         this._toastr.error(response.loginMessage, 'error');
        }
      })
    }
    
      
    
   }

   registerCustomer(){

   }
  
}
