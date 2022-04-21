import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DataParsingService } from '../data-parsing.service';

@Component({
  selector: 'app-monthly-invoice',
  templateUrl: './monthly-invoice.component.html',
  styleUrls: ['./monthly-invoice.component.css']
})
export class MonthlyInvoiceComponent implements OnInit {
//#region varaibles/Propertites
  showLoader = false;
  isCollapsed = false;
  month: any;
  invoiceDate: any;
  customHeader1: any;
  customHeader2: any;
  customHeader3: any;
  customHeader4: any;
  customHeader5: any;
  invoiceReferenceNumber: any;
  isVisible = false;

  dateAndInvoiceForm!: FormGroup;
  checked = false;
  indeterminate = false;
  listOfCurrentPageData: readonly [] = [];
  MonthalyInvoiceData: any;
  MonthlyInvoiceDataChanged:any[]=[];
  setOfCheckedId = new Set<number>();
  counter: number = 0;
  listOfSelection = [
    {
      text: 'Select All Row',
      onSelect: () => {
        this.onAllChecked(true);
      }
    },
  ];

  hairServices: any;
  beautyServices: any;
  customServices1: any;
  customServices2: any;
  customServices3: any;
  customServices4: any;
  customServices5: any;
  netTotal: any;
  advancePaid: any;
//#endregion

  constructor(private router: Router, private fb: FormBuilder, public datepipe: DatePipe, private httpService: DataParsingService, private message: NzMessageService) { }

  ngOnInit(): void {
    if (!sessionStorage.getItem('logged_user')) {
      this.router.navigate(['/login']);
    }
  }

  changeValue(data:any){
    debugger
    console.log(this.MonthlyInvoiceDataChanged);
    if(this.MonthlyInvoiceDataChanged.length != 0){
      var ExistingId:any=this.MonthlyInvoiceDataChanged.filter((key: { SupplierId: any; }) => key.SupplierId == data.SupplierId);
      console.log(ExistingId);
      if(ExistingId.length != 0){
        this.MonthlyInvoiceDataChanged.splice(ExistingId,1);
      }  
    }  
    this.MonthlyInvoiceDataChanged.push(data);
    console.log(this.MonthlyInvoiceDataChanged);
  }
  
//#region date onchange
onChange(date: any) {
  this.isVisible = true;
  this.showLoader = true;
  let monthYearFilter = date.getFullYear() + "-" + (date.getMonth()+1);
  console.log(monthYearFilter);
  this.invoiceDate = this.datepipe.transform(date, 'yyyy-MM-dd');
  this.invoiceReferenceNumber = String(date.getMonth() + 1) + String(date.getFullYear()) + String(this.counter);
  this.counter++;

  this.httpService.getInvoiceData(monthYearFilter).subscribe(
    (response) => {
      console.log(response);
      this.MonthalyInvoiceData = response.Result;
      console.log(this.MonthalyInvoiceData);
      this.showLoader = false;
      this.customHeader1 = this.MonthalyInvoiceData[0].CustomHeader1
      this.customHeader2 = this.MonthalyInvoiceData[0].CustomHeader2
      this.customHeader3 = this.MonthalyInvoiceData[0].CustomHeader3
      this.customHeader4 = this.MonthalyInvoiceData[0].CustomHeader4
      this.customHeader5 = this.MonthalyInvoiceData[0].CustomHeader5
      this.showLoader = false;
     
      this.onCurrentPageDataChange(response.Result);
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

//#region  supplier  selection
updateCheckedSet(id: number, checked: boolean): void {
  if (checked) {
    this.setOfCheckedId.add(id);
  } else {
    this.setOfCheckedId.delete(id);
  }
}

onItemChecked(id: number, checked: boolean): void {
  this.updateCheckedSet(id, checked);
  this.refreshCheckedStatus();
}

onAllChecked(value: boolean): void {
  this.MonthalyInvoiceData.forEach((item: any) => this.updateCheckedSet(item.SupplierId, value));
  this.refreshCheckedStatus();
}

onCurrentPageDataChange($event: any): void {
  this.listOfCurrentPageData = $event;
  this.refreshCheckedStatus();
}

refreshCheckedStatus(): void {
  this.checked = this.listOfCurrentPageData.every((item: any) => this.setOfCheckedId.has(item.SupplierId));
  this.indeterminate = this.listOfCurrentPageData.some((item: any) => this.setOfCheckedId.has(item.SupplierId)) && !this.checked;
}

//#endregion

//#region send email
sendEmail() {
  this.httpService.EmailSelectedSupplier(this.invoiceDate,this.setOfCheckedId,this.invoiceReferenceNumber).subscribe(
    (response) => {
      console.log(response);
      this.message.success("Email Send Successfully", {
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
//#endregion

//#region approved Supllier
Approved(){
  console.log(this.setOfCheckedId);
  this.httpService.ApprovedSelectedSupplier(this.invoiceDate,this.setOfCheckedId).subscribe(
    (response) => {
      console.log(response);
      this.message.success("Supplier Approved Successfully", {
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
//#endregion


//#region save invoice
saveInvoice(){
  console.log(this.MonthalyInvoiceData);
  this.httpService.InvoiceBody.invoiceDetails = this.MonthlyInvoiceDataChanged;
  this.httpService.InvoiceBody.CustomHeader1 = this.customHeader1;
  this.httpService.InvoiceBody.CustomHeader2 =  this.customHeader2;
  this.httpService.InvoiceBody.CustomHeader3 =  this.customHeader3;
  this.httpService.InvoiceBody.CustomHeader4 =  this.customHeader4;
  this.httpService.InvoiceBody.CustomHeader5 =  this.customHeader5;
  this.httpService.InvoiceBody.InvoiceRef = this.invoiceReferenceNumber;
  this.httpService.InvoiceBody.InvoiceDate = this.invoiceDate;
 
  this.httpService.saveInvoiceData().subscribe(
    (response) => {
      console.log(response);
      this.message.success("Data Saved Successfully", {
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
//#endregion
}
