import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';
import { DataParsingService } from '../data-parsing.service';


@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit {
  searchValue = '';
  isCollapsed = false;
  sortNamefn = (a: any , b: any) => a.SupplierName.localeCompare(b.SupplierName);
  sortRefFn = (a: any , b: any):number =>  a.SupplierReferenceNumber - b.SupplierReferenceNumber;
  
  productStatic:any;
  supplierList:any;
  
 
  isVisible = false;
  validateSupplierForm!: FormGroup;
  showLoader = false;

  constructor(private fb: FormBuilder,private message: NzMessageService,private router: Router,private httpService: DataParsingService) { }

  ngOnInit(): void {
    this.validateSupplierForm = this.fb.group({
      Name: [null, [Validators.required,Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      Address: [null,[Validators.required]],
      email: [null, [Validators.required,Validators.email,Validators.pattern("^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]],
      Reference: [null],
      phone: [null],
      comapanyNumber: [null],
      VAT: [null],
      taxRef: [null],
      ComapanyAddress: [null,[Validators.required]],
      filename: [null],
    });

    this.getUserData();
  }


  getUserData(): void {
    this.showLoader = true;
    const token =sessionStorage.getItem("logged_user")
    if(token==null){
      this.message.error("Login Required", {
        nzDuration: 1000
      });
      this.router.navigate(['/login']);
    }

    else{
      this.httpService.getProductData().subscribe(
        (response) => {
          this.supplierList=response.Result;
          this.productStatic = response.Result;
          console.log(this.supplierList);
          this.showLoader = false;
        },
        (error: any) => {
          this.message.error("Server Error! Please Reload Your Page", {
            nzDuration: 5000
          });
        },
        () => console.log("done")
      );
    }
  
  }



  search(): void {
    this.supplierList = this.productStatic.filter((item: any) => item.SupplierName.indexOf(this.searchValue) !== -1);
  }

  addSupplier(){
  }
  
  EditProduct(){
  }

  DeleteSupplier(data:any){
    this.httpService.delSupplier(data.SupplierId).subscribe(
      (response) =>{
        if(response.ResponseStatus==3){
          this.message.error(response.Message, {
            nzDuration: 1000
          });
        }
        else if(response.ResponseStatus==1){
          this.message.success(response.Message, {
            nzDuration: 1000
          });
          this.supplierList=this.supplierList.filter((key: {SupplierId:any;})=> key.SupplierId != data.SupplierId);
        }
        else{
          this.message.success(response.Message, {
            nzDuration: 1000
          });
        }
            },
      (error: any) => console.log(error),
      () => console.log("successfully delete")
    );
  }


supplierStatus(data:any){
  this.httpService.supplierStatusUpdate(data.IsActive,data.SupplierId).subscribe(
    (response) => {
      this.message.success(response.Message, {
        nzDuration: 5000
      });
    },
    (error: any) => {
      this.message.error("Server Error! Please Reload Your Page", {
        nzDuration: 5000
      });
    },
    () => console.log("done")
  );
}

  showModal(): void {
    this.isVisible = true;
  }

  handleCancel(){
    this.isVisible = false;
  }

  handleOk(){
    if (this.validateSupplierForm.valid) {
      this.showLoader = true;
      this.isVisible = false;
      this.validateSupplierForm.reset();

      this.httpService.AddSupplier().subscribe(
        (response) => {
          this.supplierList=response.Result;
          this.productStatic = response.Result;
          console.log(this.supplierList);
          this.showLoader = false;
        },
        (error: any) => {
          this.message.error("Server Error! Please Reload Your Page", {
            nzDuration: 5000
          });
        },
        () => console.log("done")
      );
      
      this.message.success('Supplier Added Successfully ', {
        nzDuration: 1000
      });
    }
    else {
      Object.values(this.validateSupplierForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      }
    }
  


}
