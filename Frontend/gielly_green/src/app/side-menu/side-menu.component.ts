import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faPlus, faPen, faTrash,faArrowUpFromBracket,faFileInvoice,faUser} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit {

  constructor(private router: Router) { }

  plusIcon = faPlus;
  pen = faPen;
  del = faTrash;
  upload = faArrowUpFromBracket;
  invoice= faFileInvoice;
  supplier = faUser;

  ngOnInit(): void {
  }

  isMenuOpen = false;

//#region logout
logout(){
  sessionStorage.removeItem("logged_user");
  this.router.navigate(['/login']);
}
//#endregion

}
