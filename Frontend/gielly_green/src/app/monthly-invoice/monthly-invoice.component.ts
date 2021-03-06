import { DatePipe,DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import { NzMessageService } from 'ng-zorro-antd/message';
import Swal from 'sweetalert2';
import { DataParsingService } from '../data-parsing.service';
import { faPlus, faPen, faTrash,faArrowUpFromBracket,faFileInvoice,faUser} from '@fortawesome/free-solid-svg-icons';


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
  isButoonVisible=true;
  invoiceDate: any;
  customHeader1: any;
  customHeader2: any;
  customHeader3: any;
  customHeader4: any;
  customHeader5: any;
  invoiceReferenceNumber: any;
  isVisible = false;
  isMenuOpen = false;
  parserDollar = (value: string): string => value.replace('( ', '');
  formatterDollar = (value: number): string => `( ${value} )`;  
  plusIcon = faPlus;
  pen = faPen;
  del = faTrash;
  upload = faArrowUpFromBracket;
  invoice= faFileInvoice;
  supplier = faUser;

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
  Vat:any;
//#endregion

  constructor(private router: Router, private fb: FormBuilder, public datepipe: DatePipe,public decimalpipe: DecimalPipe, private httpService: DataParsingService, private message: NzMessageService) { }

  ngOnInit(): void {
    if (!sessionStorage.getItem('logged_user')) {
      this.router.navigate(['/login']);
    }
  }

  changeValue(data:any){
    this.isButoonVisible=false;
    debugger
    console.log(data.HairServices);
    if(data.HairServices<0 || data.HairServices==null){
      data.HairServices=0;
    }
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
  debugger
  this.isVisible = true;
  this.showLoader = true;
  let monthYearFilter = date.getFullYear() + "-" + (date.getMonth()+1);
  console.log(monthYearFilter);
  let lastdate=new Date(date.getFullYear(), date.getMonth() + 1, 0);
  this.invoiceDate = this.datepipe.transform(lastdate, 'yyyy-MM-dd');
  this.invoiceReferenceNumber = date.toLocaleString('en-us', { month: 'long' }) + String(date.getFullYear());
  this.counter++;
  this.httpService.getInvoiceData(monthYearFilter).subscribe(
    (response) => {
      console.log(response);
      if(response.Result.InvoiceId==0){
        this.isButoonVisible=false;     
      }
      else{
        this.isButoonVisible=true;
        this.invoiceReferenceNumber=response.Result.InvoiceRef;
        this.invoiceDate=response.Result.InvoiceDate;

      }
      response.Result.InvoiceDetails.forEach((item: any) => {
        if (item.AdvancePaid == null || item.AdvancePaid==" ") {
           item.AdvancePaid = 0;
        }
      }
      );
      this.MonthalyInvoiceData = response.Result.InvoiceDetails;
      console.log(this.MonthalyInvoiceData);
      this.showLoader = false;
      this.customHeader1 = response.Result.CustomHeader1
      this.customHeader2 = response.Result.CustomHeader2
      this.customHeader3 = response.Result.CustomHeader3
      this.customHeader4 = response.Result.CustomHeader4
      this.customHeader5 = response.Result.CustomHeader5
      this.showLoader = false;
      this.Vat=response.Result.VATPercent;
     
      this.onCurrentPageDataChange(response.Result);
    },
    (error: any) => {
      this.message.error("Server Error! Please Reload Your Page", {
        nzDuration: 5000
      });
      this.showLoader = false;
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
  this.showLoader = true;
  this.httpService.EmailSelectedSupplier(this.invoiceDate,this.setOfCheckedId,this.invoiceReferenceNumber).subscribe(
    (response) => {
      console.log(response);
      if(response.ResponseStatus==0){
        Swal.fire(
          'Error!',
          'Email cannot be sent because selected invoices have no data',
          'error'
        )
        this.showLoader = false;
      }
      else if(response.ResponseStatus==0){
        this.message.error(response.Message, {
          nzDuration: 5000
        });
        this.showLoader = false;
      }
    
      else{
        this.message.success("Email send Successfully", {
          nzDuration: 5000
        });
        this.showLoader = false;
    }
  },
    (error: any) => {
      this.message.error("Server Error! Please Reload Your Page", {
        nzDuration: 5000
      });
      this.showLoader = false;
    },
    () => console.log("done")
  );
 
}
//#endregion

//#region approved Supllier
Approved(){
  this.showLoader = true;
  console.log(this.setOfCheckedId);
  this.httpService.ApprovedSelectedSupplier(this.invoiceDate,this.setOfCheckedId).subscribe(
    (response) => {
      console.log(response);
  if(response.ResponseStatus==0){
    Swal.fire(
      'Error!',
      'selected invoices have no data',
      'error'
    )
    this.showLoader = false;
  }
  else if(response.ResponseStatus==0){
    this.message.error(response.Message, {
      nzDuration: 5000
    });
    this.showLoader = false;
  }

  else{
    Swal.fire(
      'Approved !',
      'you just approved invoices',
      'success'
    )
    this.showLoader = false;
    this.onChange(this.month);  
  }
      // this.message.success("Supplier Approved Successfully", {
      //   nzDuration: 5000
      // });
    },
    (error: any) => {
      this.message.error("Server Error! Please Reload Your Page", {
        nzDuration: 5000
      });
      this.showLoader = false;
    },
    () => console.log("done")
  );
}
//#endregion

//#region save invoice
saveInvoice(){
  this.showLoader = true;
  console.log(this.MonthalyInvoiceData);
  this.httpService.InvoiceBody.invoiceDetails = this.MonthalyInvoiceData;
  this.httpService.InvoiceBody.CustomHeader1 = this.customHeader1;
  this.httpService.InvoiceBody.CustomHeader2 =  this.customHeader2;
  this.httpService.InvoiceBody.CustomHeader3 =  this.customHeader3;
  this.httpService.InvoiceBody.CustomHeader4 =  this.customHeader4;
  this.httpService.InvoiceBody.CustomHeader5 =  this.customHeader5;
  this.httpService.InvoiceBody.InvoiceReference = this.invoiceReferenceNumber;
  this.httpService.InvoiceBody.InvoiceDate = this.invoiceDate;
  console.log(this.invoiceDate);
 
 
  this.httpService.saveInvoiceData().subscribe(
    (response) => {
      console.log(response);
      if (response.ResponseStatus==1){
        this.message.success(response.Message, {
          nzDuration: 3000
        });
        this.showLoader = false;
        this.isButoonVisible=true;
      }
      else if(response.ResponseStatus==0){
        this.message.error(response.Result[0], {
          nzDuration: 3000
        });
        this.showLoader = false;
      }
      else{
        if(response.Result.includes("UQ_InvoiceRef")){
          this.message.error("Invoice Reference already exists", {
            nzDuration: 3000
          });
          this.showLoader = false;
        }
        else{
          this.message.error(response.result, {
            nzDuration: 3000
          });
          this.showLoader = false;
        }
      }
    },
    (error: any) => {
      this.message.error("Server Error! Please Reload Your Page", {
        nzDuration: 5000
      });
      this.showLoader = false;
    },
    () => console.log("done")
  );
}
//#endregion

//#region download combine Pdf
downloadPdf(base64String:any,fileName:any){
  const source = `data:application/pdf;base64,${base64String}`;
  const link = document.createElement("a");
  link.href = source;
  link.download = `${fileName}.pdf`
  link.click();
}
//#endregion

//#region print report
printInvoice(){

  window.print();
  // let printContents: any = document.getElementById('InvoiceTable');
  // let getTableToPrint = printContents.outerHTML;
  // var a: any = window.open('', '', 'height=1000, width=1000');
  // setTimeout(function(){
  //   a.document.write('<html><head><link rel="stylesheet" type="text/css" href="styles.css" /></head>');
  //   a.document.write('<body>');
  //   a.document.write(getTableToPrint);
  //   a.document.write('</body></html>');
  //   // a.close();
  //   window.print();
  // }, 2000);
}
//#endregion

//#region combine pdf API Call
CombinePDF(){
  this.showLoader = true;
  this.httpService.CombinePDfOFSupplier(this.invoiceDate,this.setOfCheckedId,this.invoiceReferenceNumber).subscribe(
    (response) => {
      console.log(response);
    
      if(response.ResponseStatus==0){
        Swal.fire(
          'Error!',
          'Selected invoices have no data so they cannot be combined and downloaded',
          'error'
        )
        this.showLoader = false;
      }
      else if(response.ResponseStatus==2){
        this.message.error(response.Result, {
          nzDuration: 5000
        });
        this.showLoader = false;
      }
    
      else{
        this.message.success("Download Successfully", {
          nzDuration: 5000
        });
        this.downloadPdf(response.Result,response.Message);
        this.showLoader = false;
    }
    },
    (error: any) => {
      this.message.error("Server Error! Please Reload Your Page", {
        nzDuration: 5000
      });
      this.showLoader = false;
    },
    () => console.log("done")
  );
}
//#endregion

//#region Invoice Calculations
getVAT(data:any){
  if(data.VATNumber==" " || data.VATNumber==null){
    return 0.00
  }
  else{
    let netvalue=this.getNet(data)
    return data.VAT=netvalue*this.Vat/100;
  }
 
}

getNet(data:any){
  let netvalue=data.HairServices+data.BeautyServices+data.CustomService1+data.CustomService2+data.CustomService3+data.CustomService4+data.CustomService5;
  return data.Net=netvalue;
}

getGross(data:any){
  let netvalue=this.getNet(data);
  let VATvalue=this.getVAT(data);
  return data.Gross=netvalue+VATvalue;
}

getBalanceDue(data:any){
  let GrossValue=this.getGross(data);
  if(data.AdvancePaid!=null || data.AdvancePaid!=""){
    return data.BalanceDue=GrossValue-data.AdvancePaid;
  }
  else{
    return data.BalanceDue=GrossValue;
  }
}

getTotalNET(data:any){
  let total=0;
  data.forEach((item:any)=>
  total+=item.Net
  );
  return total as any;
}

getTotalVET(data:any){
  let total=0;
  data.forEach((item:any)=>
  total+=item.VAT
  );
  return total as any;
}

getTotalGross(data:any){
  let total=0;
  data.forEach((item:any)=>
  total+=item.Gross
  );
  return total as any;
}

getTotalAdvancePay(data:any){
  let total=0;
  data.forEach((item:any)=>
  total+=item.AdvancePaid
  );
  return total as any;
}

getTotalBalanceDue(data:any){
  let total=0;
  data.forEach((item:any)=>
  total+=item.BalanceDue
  );
  return total as any;
}

//#endregion 

//#region logout
logout(){
  sessionStorage.removeItem("logged_user");
  this.router.navigate(['/login']);
}
//#endregion
}

