import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataParsingService {
 
  constructor(private http: HttpClient) { }

   APIURL="https://23ea-106-201-236-89.ngrok.io";

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
    
  

}
