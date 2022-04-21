import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataParsingService {
 
  constructor(private http: HttpClient) { }

   APIURL="https://ed97-106-201-236-89.ngrok.io";

SupplierBody=
  {
    "SupplierName": "sample string 2",
    "SupplierReferenceNumber": "sample string 3",
    "BusinessAddress": "sample string 4",
    "EmailAddress": "sample string 5",
    "PhoneNumber": "sample string 6",
    "CompanyRegisteredNumber": "sample string 7",
    "VATNumber": "sample string 8",
    "TAXReference": "sample string 9",
    "CompanyRegisteredAddress": "sample string 10",
    "IsActive": true,
    "Logo": "sample string 12",
    "IsInvoicePresent": true
  }

  InvoiceBody={
    "invoiceDetails": [
      {
        "InvoiceDetailId": 0,
        "SupplierId": 0,
        "HairServices": 0,
        "BeautyServices": 0,
        "CustomService1": 0,
        "CustomService2": 0,
        "CustomService3": 0,
        "CustomService4": 0,
        "CustomService5": 0,
        "Net": 0,
        "VAT": 0,
        "Gross": 0,
        "AdvancePaid": 0,
        "BalanceDue": 0,
        "Approved": true
      }
    ],
    "CustomHeader1": "string",
    "CustomHeader2": "string",
    "CustomHeader3": "string",
    "CustomHeader4": "string",
    "CustomHeader5": "string",
    "InvoiceRef": "string",
    "InvoiceDate": "2022-04-20T09:44:00.790Z",
    "InvoiceId": 0
  }

  loginUser(email: string,password: string): Observable<any>{
    let body ={
      'Username':email,
      'Password':password
    }
      return this.http.post<any>(`${this.APIURL}//login`,body);
    }

    logintoken(email: string,password: string){
      let body =new URLSearchParams();
      body.set('Username',email);
      body.set('Password',password);
      body.set('grant_type',"password");

      const header=new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
      return this.http.post<any>(`${this.APIURL}//Token`,body,{headers:header});

    }

    getSupplierAPIData(): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.get<any>(`${this.APIURL}/api/Suppliers?isActive=False`,{headers:header});
    }
 

    delSupplier(ID:any): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.delete<any>(`${this.APIURL}/api/Suppliers/${ID}`,{headers:header});
    }
  
    supplierStatusUpdate(status:any,id:any): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.patch<any>(`${this.APIURL}/api/Suppliers?status=${status}&supplierId=${id}`,{headers:header});
    }
  
    AddSupplier(): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.post<any>(`${this.APIURL}//api/Suppliers`,this.SupplierBody,{headers:header});
    }

    
  editSupplier(ID:any): Observable<any>{
    let token=sessionStorage.getItem("logged_user");
    const header=new HttpHeaders().set("authorization", "bearer "+ token);
    return this.http.put<any>(`${this.APIURL}//api/Suppliers/${ID}`,this.SupplierBody,{headers:header});
  }


  getInvoiceData(month:any): Observable<any>{
    let token=sessionStorage.getItem("logged_user");
    const header=new HttpHeaders().set("authorization", "bearer "+ token);
    return this.http.get<any>(`${this.APIURL}/api/MonthlyInvoice?InvoiceMonth=${month}`,{headers:header});
  }

  
  saveInvoiceData(): Observable<any>{
    let token=sessionStorage.getItem("logged_user");
    const header=new HttpHeaders().set("authorization", "bearer "+ token);
    return this.http.post<any>(`${this.APIURL}/api/MonthlyInvoice`,this.InvoiceBody,{headers:header});
  }

  ApprovedSelectedSupplier(date:any,setofselected:any): Observable<any>{
    let selectedSupllier=Array.from(setofselected);
    let token=sessionStorage.getItem("logged_user");
    const header=new HttpHeaders().set("authorization", "bearer "+ token);
    return this.http.patch<any>(`${this.APIURL}/api/MonthlyInvoice?selectedDate=${date}`,selectedSupllier,{headers:header});
  }

  EmailSelectedSupplier(date:any,setofselected:any,invoiceReferenceNumber:any): Observable<any>{
    let selectedSupllier=Array.from(setofselected);
    let token=sessionStorage.getItem("logged_user");
    const header=new HttpHeaders().set("authorization", "bearer "+ token);
    return this.http.post<any>(`${this.APIURL}/api/Email?invoiceDate=${date}&InvoiceRef=${invoiceReferenceNumber}`,selectedSupllier,{headers:header});
  }
    
}
