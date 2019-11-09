import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'openr-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  constructor(private userService: UserService,
              private translate: TranslateService,
              private router: Router) {
    this.translate.setDefaultLang('en-us');
    this.translate.use('en-us');
  }

  user: User;

  ngOnInit() {
    if (this.userService.IsUserLogged()) {
      this.user = this.userService.LoggedUser();
    } else {
      this.router.navigateByUrl('/login');
    }
  }

}
