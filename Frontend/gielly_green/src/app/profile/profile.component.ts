import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faPlus, faPen, faTrash,faArrowUpFromBracket,faFileInvoice,faUser} from '@fortawesome/free-solid-svg-icons';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DataParsingService } from '../data-parsing.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  isCollapsed = false;
  invoice= faFileInvoice;
  supplier = faUser;
  validateProfileForm!: FormGroup;
  profileID:any;
  showLoader = false;


  constructor(private fb: FormBuilder, private message: NzMessageService, private router: Router, private httpService: DataParsingService) { }

  ngOnInit(): void {
    const token = sessionStorage.getItem("logged_user")
    if (token == null) {
      this.message.error("Login Required", {
        nzDuration: 1000
      });
      this.router.navigate(['/login']);
    }
    this.validateProfileForm = this.fb.group({
      Name: [null],
      Address: [null],
      city: [null],
      Zipcode: [null],
      Country: [null],
      VAT: [null],
    });

    this.getProfile();
  }

  //#region getProfile Data
getProfile(){
  this.showLoader = true;
  this.httpService.getProfile().subscribe(
    (response) => {
      console.log(response);
      this.validateProfileForm.patchValue({
        Name: response.Result.CompanyName,
        Address: response.Result.AddressLine,
        city: response.Result.City,
        Zipcode: response.Result.ZipCode,
        Country: response.Result.Country,
        VAT: response.Result.DefaultVAT,
      });
      this.profileID= response.Result.ProfileId;
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


//#region submit profile
submitProfile(){
  this.showLoader = true;
  const ProfileDetail = this.validateProfileForm.controls
  const profile = this.httpService.ProfileBody
  profile.CompanyName = ProfileDetail["Name"].value;
  profile.AddressLine = ProfileDetail["Address"].value;
  profile.City = ProfileDetail["city"].value;
  profile.ZipCode = ProfileDetail["Zipcode"].value;
  profile.Country = ProfileDetail["Country"].value;
  profile.DefaultVAT = ProfileDetail["VAT"].value;
  profile.ProfileId = this.profileID;


  this.httpService.setProfile().subscribe(
    (response) => {
      this.message.success("Data Saved Successfully!", {
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

//#region logout
logout(){
  sessionStorage.removeItem("logged_user");
  this.router.navigate(['/login']);
}
//#endregion

}
