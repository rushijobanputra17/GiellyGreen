import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DataParsingService } from '../data-parsing.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  validateForm!: FormGroup;

  submitForm(): void {
    if (this.validateForm.valid) {
      const UserLoginDetail=this.validateForm.controls
      
  

      this.httpService.loginUser(UserLoginDetail["userName"].value,UserLoginDetail["password"].value).subscribe(
        (response) => {
          
          if(response.ResponseStatus==1){

            this.httpService.logintoken(UserLoginDetail["userName"].value,UserLoginDetail["password"].value).subscribe(
              (response) => {
                this.message.success('Successfully Login', {
                  nzDuration: 2000
                }),
                sessionStorage.setItem("logged_user",response.access_token);
                this.router.navigate(['/suppliers']);
              },
              (error: any) => {
                this.message.error("Server Error! Please Reload Your Page", {
                  nzDuration: 5000
                });
              },
              () => console.log("success")
            );
            
          }
          else{

            this.message.error('Invalid Email or Password', {
              nzDuration: 2000
            });
          }
           
        },
        (error: any) => {
         
          if(error.error.error_description==null){
            this.message.error("sometghin went wrong! Plaese Reload Your Page", {
              nzDuration: 1000
            });
          }
          else{
            this.message.error(error.error.error_description, {
              nzDuration: 1000
            });
          }
         
        },
        () => console.log("successfully Login")
      );
    
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }


  constructor(private fb: FormBuilder,private router: Router,private httpService: DataParsingService,private message: NzMessageService) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }

}
