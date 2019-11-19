import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../user/user.service';
import { LanguageService } from '../shared/language.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router,
              private userService: UserService,
              private languageService: LanguageService,
              private translate: TranslateService) { }

  ngOnInit() {
    this.translate.use(this.languageService.Current());
    if (this.userService.IsUserLogged()) {
      this.router.navigateByUrl('/user');
    }
  }

}
