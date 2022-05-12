import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {


  public userForm: FormGroup;
  Roles:DropDownModel[]=[];
  userId:number;

  constructor
  (
    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _userService : UserService,
    private spinner: NgxSpinnerService
  )
   {
    
   }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params=>{
      this.userId = +params.id;
  })

   this.getAllRoles();
   this.userForm = this.createUserForm()
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
     mobileNo: [null, Validators.required],
     username: [null, Validators.required],
     password: [null, Validators.required]
   })
 }
  
 onFileChange(event:any, type:number){

 }


 /*
 *Get DropDown MasterData
 */
 getAllRoles()
 {
   this.spinner.show();
    this._userService.getAllRoles()
      .subscribe(response=>{
      this.Roles = response;
    },(error)=>{
      this.spinner.hide();
    })   
 }

  public onSubmit(){
    console.log(this.userForm.value);
  }

 /*
 *Getters
 */
  get id()
  {
    return this.userForm.get('id').value;
  }
}