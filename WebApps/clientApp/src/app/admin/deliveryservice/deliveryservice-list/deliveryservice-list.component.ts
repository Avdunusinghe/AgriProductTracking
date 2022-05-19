import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DeliveryServiceFilterModel } from 'src/app/models/deliveryservice/deliveryservice.filter.model';
import { DeliveryServiceModel } from 'src/app/models/deliveryservice/deliveryservice.model';
import { DeliveryserviceService } from 'src/app/services/deliveryservice/deliveryservice.service';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-deliveryservice-list',
  templateUrl: './deliveryservice-list.component.html',
  styleUrls: ['./deliveryservice-list.component.scss']
})
export class DeliveryserviceListComponent implements OnInit {

  deliveryserviceFilterForm:FormGroup;
  rowData = new Array<DeliveryServiceModel>();

  public searchText: string;
  constructor(

    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _spinner: NgxSpinnerService,
    private _deliveryService: DeliveryserviceService,
    private _toastr: ToastrService,
    private _router:Router,
    public _dialog: MatDialog

  ) { }

  currentPage: number = 0;
  pageSize: number = 5;
  totalRecord: number = 0;


  ngOnInit(): void {
    
    this.deliveryserviceFilterForm= this.createdeliveryserviceFilterForm();
    this.getDeliveryServiceList();
  }

  
 /*
 *Create delievry service Filter Form
 */


 createdeliveryserviceFilterForm():FormGroup{
  return this._formBuilder.group({
    searchText: new FormControl("")
    
  })
}

/*
 *Add Newdelivery service  Route 
 */

 addNewDeliveryService()
 {
    this._router.navigate(["admin/deliveryservice/deliveryservice-detail"]);
 }

 /*
 *Update delivery Route with param 
 */

 updateDeliveryService(id:number)
 {
    this._router.navigate(["admin/deliveryservice/deliveryservice-detail",id]);
 }

  /*
 *Delete delivery service
 */

 deleteDeliveryService(id:number)
 {
    const dialogRef = this._dialog.open(ConfirmDialogComponent,{
      maxWidth: "400px",
      data: {
        title: "Confirm Delivery Service",
        message: "Are you sure you want delete this DeliveryService?"
      }
    });
    dialogRef.afterClosed().subscribe(dialogResult => { 
      if(dialogResult){
       this._deliveryService.deliveryServiceDelete(id).subscribe((response)=>{
          if(response.isSuccess)
          {
             this._toastr.success(response.message,"success");
             this. getDeliveryServiceList();
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
*Search Filter Changed Fire Event
 */

filterDataTable(event)
{
  this.currentPage = 0;
  this.pageSize = 10;
  this._spinner.show();
  this. getDeliveryServiceList();

}

/*
 *Page Number Changed Fire Event
 */

onPageChanged(pageInfo)
{
  this._spinner.show();
  console.log(pageInfo);
  
  this.currentPage = pageInfo;
  this. getDeliveryServiceList();
}

/*
 *Get All deliveryservice Details
 */

 getDeliveryServiceList()
  {
    this._spinner.show();

    let filter = new DeliveryServiceFilterModel();

  
    filter.searchText = this.searchFilter;
    filter.currentPage = this.currentPage + 1;
    filter.pageSize = this.pageSize;

    this._deliveryService.getDeliveryServiceList(filter).subscribe((response)=>{
      this.rowData = response.data;
      console.log(this.rowData);
      this.totalRecord = response.totalRecordCount;
     
    },(error)=>{
      this._spinner.hide();
      this._toastr.error("Network error has been occured. Please try again.","error");
    });
    
  }



  

  get searchFilter()
  {
    return this.deliveryserviceFilterForm.get("searchText").value;
  }

}



