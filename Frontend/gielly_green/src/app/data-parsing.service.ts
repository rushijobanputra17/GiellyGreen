import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataParsingService {
 
  constructor(private http: HttpClient) { }

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
      return this.http.post<any>(`https://3e22-106-201-236-89.ngrok.io//login`,body);
    }

    logintoken(email: string,password: string){
      let body =new URLSearchParams();
      body.set('Username',email);
      body.set('Password',password);
      body.set('grant_type',"password");

      const header=new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
      return this.http.post<any>(`https://3e22-106-201-236-89.ngrok.io//Token`,body,{headers:header});

    }

    getProductData(): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.get<any>(`https://3e22-106-201-236-89.ngrok.io/api/Suppliers?isActive=False`,{headers:header});
    }
 

    delSupplier(ID:any): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.delete<any>(`https://3e22-106-201-236-89.ngrok.io/api/Suppliers/${ID}`,{headers:header});
    }
  
    supplierStatusUpdate(status:any,id:any): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.patch<any>(`https://3e22-106-201-236-89.ngrok.io/api/Suppliers?status=${status}&supplierId=${id}`,{headers:header});
    }
  
    AddSupplier(): Observable<any>{
      let token=sessionStorage.getItem("logged_user");
      const header=new HttpHeaders().set("authorization", "bearer "+ token);
      return this.http.patch<any>(`https://3e22-106-201-236-89.ngrok.io//api/Suppliers`,this.SupplierBody,{headers:header});
    }
  

}
