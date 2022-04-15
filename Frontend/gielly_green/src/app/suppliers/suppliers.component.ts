import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';


@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit {
  searchValue = '';
  isCollapsed = false;
  sortNamefn = (a: any , b: any) => a.name.localeCompare(b.name);
  sortRefFn = (a: any , b: any):number =>  a.reference - b.reference;
  
  productStatic:any;
  supplierList:any=[
    {
      name:'Vijay',
      reference: 1521313,
      status:true
    
    },
    {
      name:'prince patel',
      reference: 5484549,
      status:false
    },
    {
      name:'Rushi',
      reference: 48447454,
      status:true
    },
    {
      name:'Harsh Patel',
      reference: 496494,
      status:false
    }
  ];
  
 
  isVisible = false;
  validateSupplierForm!: FormGroup;

  constructor(private fb: FormBuilder,private message: NzMessageService) { }

  ngOnInit(): void {
    this.validateSupplierForm = this.fb.group({
      Name: [null, [Validators.required]],
      Address: [null,[Validators.required]],
      email: [null, [Validators.required,Validators.email]],
      Reference: [null],
      phone: [null],
      comapanyNumber: [null],
      VAT: [null],
      taxRef: [null],
      ComapanyAddress: [null,[Validators.required]],
      filename: [null],
    });
  }

  search(): void {
    this.productStatic=this.supplierList
    this.supplierList = this.productStatic.filter((item: any) => item.name.indexOf(this.searchValue) !== -1);
  }

  addSupplier(){
  }
  
  EditProduct(){
  }

  DeleteProduct(){
  }

  handleCancel(){
    this.isVisible = false;
  }

  handleOk(){
    if (this.validateSupplierForm.valid) {
      this.isVisible = false;
      this.validateSupplierForm.reset();

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
  
  showModal(): void {
   
    this.isVisible = true;
  }

}
