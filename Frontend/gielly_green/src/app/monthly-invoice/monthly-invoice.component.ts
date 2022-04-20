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
  
//#region date onchange
onChange(date: any) {
  this.isVisible = true;
  this.showLoader = true;
  let monthYearFilter = date.getFullYear() + "-" + date.getMonth()
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
  this.MonthalyInvoiceData.forEach((item: any) => this.updateCheckedSet(item.SupId, value));
  this.refreshCheckedStatus();
}

onCurrentPageDataChange($event: any): void {
  this.listOfCurrentPageData = $event;
  this.refreshCheckedStatus();
}

refreshCheckedStatus(): void {
  this.checked = this.listOfCurrentPageData.every((item: any) => this.setOfCheckedId.has(item.SupId));
  this.indeterminate = this.listOfCurrentPageData.some((item: any) => this.setOfCheckedId.has(item.id)) && !this.checked;
}

//#endregion

//#region send email
sendEmail() {
  this.message.success("Email Send Successfully", {
    nzDuration: 5000
  });
}
//#endregion

}
