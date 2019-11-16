import { Component, OnInit, Inject, Injectable } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import { Router } from '@angular/router';
import { UserService } from '../user/user.service';

@Injectable()
@Component({
  selector: 'openr-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private translate: TranslateService,
              private router: Router,
              private userService: UserService) {
    this.translate.setDefaultLang('en-us');
    this.translate.use('en-us');
  }

  ngOnInit() {
  }

  Logout() {
    this.userService.Loggout();
    this.router.navigateByUrl('/');
  }

  IsHome(): boolean {
    return this.router.url === '/user';
  }

  IsUserLogged(): boolean {
    return this.userService.IsUserLogged();
  }

}
