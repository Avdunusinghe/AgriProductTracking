import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ResponseModel } from 'src/app/models/common/response.model';
import { OrderModel } from 'src/app/models/order/order.model';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {

  orderFilterForm:FormGroup;
  rowData:OrderModel;
  

  constructor(

    public _formBuilder: FormBuilder, 
    private _activatedRoute: ActivatedRoute, 
    private _spinner: NgxSpinnerService,
    private _orderService: OrderService,
    private _toastr: ToastrService,
    private _router:Router,
    public _dialog: MatDialog

  ) { }

  ngOnInit(): void {

    this.orderFilterForm= this.createOrderFilterForm();
    this.getAllOrders();
  }

   /*
 *Create order Filter Form
 */


 createOrderFilterForm():FormGroup{
  return this._formBuilder.group({
    searchText: new FormControl("")
    
  })
}

 /*
 *confirm order with delivery Route with param 
 */

 confirmOrder(id:number)
 {
    this._router.navigate(["admin/order/order-detail",id]);
 }

 /*
 *Page Number Changed Fire Event
 */

onPageChanged(pageInfo)
{
  this._spinner.show();
  console.log(pageInfo);
  this.getAllOrders();
}

/*
 *Get All order Details
 */


getAllOrders()
{
   this._spinner.show();
   this._orderService.getAllOrders()
     .subscribe(response=>{
      this.rowData = response;
      console.log(response);
     
   },(error)=>{
     this._spinner.hide();
   })    
}
}
    


