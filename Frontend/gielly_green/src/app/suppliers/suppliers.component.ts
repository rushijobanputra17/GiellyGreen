import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit {
  searchValue = '';
  isCollapsed = false;
  supplierList:any;
  isVisible = false;
  validateSupplierForm!: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validateSupplierForm = this.fb.group({
      Name: [null, [Validators.required]],
      Description: [null, [Validators.required]],
      Quantity: [null, [Validators.required]],
      Status: [null, [Validators.required]],
      Price: [null, [Validators.required]],
    });
  }

  search(): void {
    // this.product = this.productStatic.filter((item: any) => item.Name.indexOf(this.searchValue) !== -1);
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
  }

  
  showModal(): void {
    this.isVisible = true;
  }

}
