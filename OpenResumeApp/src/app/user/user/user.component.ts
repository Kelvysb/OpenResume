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
              private cookieService: CookieService,
              private router: Router) { }

  user: User;

  ngOnInit() {
    if (this.cookieService.check('User')) {
      this.user = JSON.parse(this.cookieService.get('User'));
    } else {
      this.router.navigateByUrl('/login');
    }
  }

}
