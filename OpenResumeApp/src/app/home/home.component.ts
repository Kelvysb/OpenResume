import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { UserService } from '../user/user.service';

@Component({
  selector: 'openr-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private translate: TranslateService,
              private router: Router,
              private userService: UserService) {
    this.translate.setDefaultLang('en-us');
    this.translate.use('en-us');
  }

  ngOnInit() {
    if (this.userService.IsUserLogged()) {
      this.router.navigateByUrl('/user');
    }
  }

}
