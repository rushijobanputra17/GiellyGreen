import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataParsingService {
 
  constructor(private http: HttpClient) { }

  loginUser(email: string,password: string): Observable<any>{
    let body ={
      'Username':email,
      'Password':password
    }
      return this.http.post<any>(`https://0354-106-201-236-89.ngrok.io/login`,body);
    }

    logintoken(email: string,password: string){
      let body =new URLSearchParams();
      body.set('Username',email);
      body.set('Password',password);
      body.set('grant_type',"password");

      const header=new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded');
      return this.http.post<any>(`https://0354-106-201-236-89.ngrok.io/Token`,body,{headers:header});

    }
 
}
