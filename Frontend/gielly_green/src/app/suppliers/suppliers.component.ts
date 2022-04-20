import { toBase64String } from '@angular/compiler/src/output/source_map';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';
import { DataParsingService } from '../data-parsing.service';
import { faPlus, faPen, faTrash } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit {

  //#region Varaibles/Propertites 
  searchValue = '';
  isCollapsed = false;
  sortNamefn = (a: any, b: any) => a.SupplierName.localeCompare(b.SupplierName);
  sortRefFn = (a: any, b: any): number => a.SupplierReferenceNumber - b.SupplierReferenceNumber;

  //icons
  plusIcon = faPlus;
  pen = faPen;
  del = faTrash;

  productStatic: any;
  supplierList: any;
  file: File;
  baseURL: any;
  supplierLogo: any;
  isEdit = false;
  EditedSupplier: any;
  uploadedlogo: any;



  isVisible = false;
  validateSupplierForm!: FormGroup;
  showLoader = false;
  //#endregion

  constructor(private fb: FormBuilder, private message: NzMessageService, private router: Router, private httpService: DataParsingService) { }

  ngOnInit(): void {

    //validate form
    this.validateSupplierForm = this.fb.group({
      Name: [null, [Validators.required, Validators.pattern("^[a-zA-Z]+[ ]?[a-zA-Z]+$")]],
      Address: [null, [Validators.required, Validators.pattern("^[A-Za-z0-9 .,*&%$#@!-_]{3,150}$")]],
      email: [null, [Validators.required, Validators.email]],
      phone: [null, [Validators.pattern("^[0-9]{10,15}$")]],
      Reference: [null, [Validators.required, Validators.pattern("^[A-Za-z0-9]{1,15}$")]],
      comapanyNumber: [null, [Validators.pattern("^[0-9]{10,15}$")]],
      VAT: [null, [Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      taxRef: [null, [Validators.pattern("^[a-zA-Z0-9]{1,15}$")]],
      ComapanyAddress: [null, [Validators.pattern("[A-Za-z0-9 .,*&%$#@!-_]{3,150}$")]],
      filename: [null],
      status: [false],
    });
    this.getSupplierData();
  }

//#region getsupplier data from database
  getSupplierData(): void {
    this.showLoader = true;
    const token = sessionStorage.getItem("logged_user")
    if (token == null) {
      this.message.error("Login Required", {
        nzDuration: 1000
      });
      this.router.navigate(['/login']);
    }

    else {
      this.httpService.getSupplierAPIData().subscribe(
        (response) => {
          this.supplierList = response.Result;
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
//#endregion

//#region uploadedlogo convert into base64 String
  uploadLogo(event: any) {
    this.showLoader = true;
    this.file = event.target.files[0];
    const reader: any = new FileReader();
    reader.readAsDataURL(this.file);
    reader.onload = () => {
      this.baseURL = reader.result.split(",")
      this.supplierLogo = this.baseURL[1];
      this.uploadedlogo = "data:image/png;base64," + this.supplierLogo;
    };
    this.showLoader = false;

  }
//#endregion

//#region search functionality by supplier name
  search(): void {
    this.supplierList = this.productStatic.filter((item: any) => item.SupplierName.toLowerCase().indexOf(this.searchValue.toLowerCase()) !== -1);
  }
//#endregion

//#region edit supplier's values fill into the modal.
  EditSupplier(event: any) {
    this.isEdit = true;
    console.log(event);
    debugger
    this.isVisible = true;
    console.log(event.Logo);
    this.uploadedlogo = "data:image/png;base64," + event.Logo;
    this.validateSupplierForm.patchValue({
      Name: event.SupplierName,
      Reference: event.SupplierReferenceNumber,
      Address: event.BusinessAddress,
      email: event.EmailAddress,
      phone: event.PhoneNumber,
      comapanyNumber: event.CompanyRegisteredNumber,
      VAT: event.VATNumber,
      taxRef: event.TAXReference,
      ComapanyAddress: event.CompanyRegisteredAddress,
      status: event.IsActive,
      filename: (event.Logo),
    });
    this.EditedSupplier = event;
  }

//#endregion

//#region delete supplier from Database.
  DeleteSupplier(data: any) {
    this.showLoader = true
    this.httpService.delSupplier(data.SupplierId).subscribe(
      (response) => {
        if (response.ResponseStatus == 3) {
          this.message.error(response.Message, {
            nzDuration: 5000
          });
          this.showLoader = false;
        }
        else if (response.ResponseStatus == 1) {
          this.message.success(response.Message, {
            nzDuration: 5000
          });
          this.showLoader = false;
          this.supplierList = this.supplierList.filter((key: { SupplierId: any; }) => key.SupplierId != data.SupplierId);
        }
        else {
          this.message.success(response.Message, {
            nzDuration: 5000
          });
          this.showLoader = false;
        }
      },
      (error: any) => console.log(error),
      () => console.log("successfully delete")
    );
  }
//#endregion

//#region update supplier Status
  supplierStatus(data: any) {
    this.showLoader = true;
    this.httpService.supplierStatusUpdate(data.IsActive, data.SupplierId).subscribe(
      (response) => {
        this.message.success(response.Message, {
          nzDuration: 5000
        });
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
//#endregion

//#region show modal.
  showModal(): void {
    this.isEdit = false;
    this.isVisible = true;
    this.uploadedlogo = null;
  }
//#endregion

//#region modal cancel button
  handleCancel() {
    this.isVisible = false;
    this.validateSupplierForm.reset();
  }
//#endregion

//#region when modal submit ?(add or edit)
  handleOk() {
    debugger
    if (this.isEdit != true) {
      if (this.validateSupplierForm.valid) {
        this.showLoader = true;

        const SupplierDetail = this.validateSupplierForm.controls
        this.httpService.SupplierBody.SupplierName = SupplierDetail["Name"].value;
        this.httpService.SupplierBody.SupplierReferenceNumber = SupplierDetail["Reference"].value;
        this.httpService.SupplierBody.BusinessAddress = SupplierDetail["Address"].value;
        this.httpService.SupplierBody.EmailAddress = SupplierDetail["email"].value;
        this.httpService.SupplierBody.PhoneNumber = SupplierDetail["phone"].value;
        this.httpService.SupplierBody.CompanyRegisteredNumber = SupplierDetail["comapanyNumber"].value;
        this.httpService.SupplierBody.VATNumber = SupplierDetail["VAT"].value;
        this.httpService.SupplierBody.TAXReference = SupplierDetail["taxRef"].value;
        this.httpService.SupplierBody.CompanyRegisteredAddress = SupplierDetail["ComapanyAddress"].value;
        this.httpService.SupplierBody.IsActive = SupplierDetail["status"].value;
        this.httpService.SupplierBody.Logo = this.supplierLogo;
        this.httpService.AddSupplier().subscribe(

          (response) => {
            console.log(response);

            try {
              if (response.Result.includes("UQ_Supplier_ReferenceNumer")) {
                this.message.error("Supplier reference number already exists", {
                  nzDuration: 5000
                });
              }
              else if (response.Result.includes("idx_VATNumber")) {
                this.message.error("VAT number already exists", {
                  nzDuration: 5000
                });
              }
              else if (response.Result.includes("idx_TAXReference")) {
                this.message.error("TAX reference number already exists", {
                  nzDuration: 5000
                });
              }
              else if (response.Result.includes("idx_Email")) {
                this.message.error("Supplier Email  already exists", {
                  nzDuration: 5000
                });
              }
            }

            catch {
              this.message.success('Supplier Added Successfully ', {
                nzDuration: 1000
              });
              this.isVisible = false;
              this.validateSupplierForm.reset();
              this.getSupplierData();
              this.showLoader = false;
            }
          },
          (error: any) => {
            this.message.error("Server Error! Please Reload Your Page", {
              nzDuration: 5000
            });
          },
          () => console.log("done")
        );
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

    else {
      this.showLoader = true;
      if (this.validateSupplierForm.valid) {
        const SupplierDetail = this.validateSupplierForm.controls
        const editBody = this.httpService.SupplierBody
        console.log(this.EditedSupplier);
        console.log(editBody);
        this.isVisible = false;
        this.EditedSupplier.SupplierName = editBody.SupplierName = SupplierDetail["Name"].value;
        this.EditedSupplier.SupplierReferenceNumber = editBody.SupplierReferenceNumber = SupplierDetail["Reference"].value;
        this.EditedSupplier.BusinessAddress = editBody.BusinessAddress = SupplierDetail["Address"].value;
        this.EditedSupplier.EmailAddress = editBody.EmailAddress = SupplierDetail["email"].value;
        this.EditedSupplier.PhoneNumber = editBody.PhoneNumber = SupplierDetail["phone"].value;
        this.EditedSupplier.CompanyRegisteredNumber = editBody.CompanyRegisteredNumber = SupplierDetail["comapanyNumber"].value;
        this.EditedSupplier.VATNumber = editBody.VATNumber = SupplierDetail["VAT"].value;
        this.EditedSupplier.TAXReference = editBody.TAXReference = SupplierDetail["taxRef"].value;
        this.EditedSupplier.CompanyRegisteredAddress = editBody.CompanyRegisteredAddress = SupplierDetail["ComapanyAddress"].value;
        this.EditedSupplier.IsActive = editBody.IsActive = SupplierDetail["status"].value;
        this.EditedSupplier.Logo = editBody.Logo = this.supplierLogo;
        this.httpService.editSupplier(this.EditedSupplier.SupplierId).subscribe(

          (response) => {
            console.log(response);

            try {
              if (response.Result.includes("UQ_Supplier_ReferenceNumer")) {
                this.isVisible = true;
                this.message.error("Supplier reference number already exists", {
                  nzDuration: 5000
                });
              }
              else if (response.Result.includes("idx_VATNumber")) {
                this.isVisible = true;
                this.message.error("VAT number already exists", {
                  nzDuration: 5000
                });
              }
              else if (response.Result.includes("idx_TAXReference")) {
                this.isVisible = true;
                this.message.error("TAX reference number already exists", {
                  nzDuration: 5000
                });
              }
              else if (response.Result.includes("idx_Email")) {
                this.isVisible = true;
                this.message.error("Supplier Email  already exists", {
                  nzDuration: 5000
                });
              }
            }
            catch {
              this.message.success('Supplier Edited Successfully ', {
                nzDuration: 5000
              });
              this.isVisible = false;
              this.validateSupplierForm.reset();
              this.showLoader = false;
            }

          },
          (error: any) => console.log(error),
          () => console.log("successfully Edited")
        );

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
//#endregion

}
