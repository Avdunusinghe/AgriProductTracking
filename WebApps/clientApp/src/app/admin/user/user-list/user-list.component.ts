import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/models/common/drop.down.model';
import { UserFilterModel } from 'src/app/models/user/user.filter.model';
import { UserModel } from 'src/app/models/user/user.model';
import { UserService } from 'src/app/services/user/user.service';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  userFilterForm:FormGroup;
  userRoles:DropDownModel[]=[];
  rowData = new Array<UserModel>();

  public searchText: string;
  constructor(

    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _spinner: NgxSpinnerService,
    private _userService:UserService,
    private _toastr: ToastrService,
    private _router:Router,
    public _dialog: MatDialog

  ) { }

  currentPage: number = 0;
  pageSize: number = 5;
  totalRecord: number = 0;

  ngOnInit(): void {

    this.getAllRoles();
    this.userFilterForm = this.createUserFilterForm();
  }


   /*
 *Get DropDown MasterData (roles)
 */

 getAllRoles()
 {
   this._spinner.show();
    this._userService.getAllRoles()
      .subscribe(response=>{
      this.userRoles = response;
      this.getUserList();
    },(error)=>{
      this._spinner.hide();
    })   
 }

 /*
 *Create user Filter Form
 */


 createUserFilterForm():FormGroup{
  return this._formBuilder.group({
    searchText: new FormControl(""),
    roleId:new FormControl(0),
  })
}

/*
 *Add New user Route userDetail Compenent
 */

 addNewUser()
 {
    this._router.navigate(["admin/user/user-detail"]);
 }

 /*
 *Update user Route with param userDetail Compenent
 */

 updateUser(id:number)
 {
    this._router.navigate(["admin/user/user-detail",id]);
 }

  /*
 *Delete user
 */

 deleteUser(id:number)
 {
    const dialogRef = this._dialog.open(ConfirmDialogComponent,{
      maxWidth: "400px",
      data: {
        title: "Confirm Delete User",
        message: "Are you sure you want delete this user?"
      }
    });
    dialogRef.afterClosed().subscribe(dialogResult => { 
      if(dialogResult){
       this._userService.deleteUser(id).subscribe((response)=>{
          if(response.isSuccess)
          {
             this._toastr.success(response.message,"success");
             this.getUserList();
          }else
          {
            this._toastr.error(response.message,"error")
          }
       })
      } 
    },(error)=>{
      this._toastr.error("Network error has been occured. Please try again.","error")
    }); 
 }

 /*
 *Role Filter Changed Fire Event
 */

onRoleIdChanged(item:any)
{
  this.currentPage = 0;
  this.pageSize = 0;
  this.totalRecord = 0;
  this._spinner.show();
  this.getUserList();

}

/*
*Search Filter Changed Fire Event
 */

filterDataTable(event)
{
  this.currentPage = 0;
  this.pageSize = 10;
  this._spinner.show();
  this.getUserList();

}

/*
 *Page Number Changed Fire Event
 */

onPageChanged(pageInfo)
{
  this._spinner.show();
  console.log(pageInfo);
  
  this.currentPage = pageInfo;
  this.getUserList();
}

/*
 *Get All user Details
 */

 getUserList()
  {
    this._spinner.show();

    let filter = new UserFilterModel();

    filter.roleId = this.roleFilterId;
    filter.searchText = this.searchFilter;
    filter.currentPage = this.currentPage + 1;
    filter.pageSize = this.pageSize;

    this._userService.getUserList(filter).subscribe((response)=>{
      this.rowData = response.data;
      this.totalRecord = response.totalRecordCount;
    },(error)=>{
      this._spinner.hide();
      this._toastr.error("Network error has been occured. Please try again.","error");
    });

  }


  /*
  *Getters
  */
  get roleFilterId()
  {
    return this.userFilterForm.get("roleId").value;
  }

  get searchFilter()
  {
    return this.userFilterForm.get("searchText").value;
  }

}
