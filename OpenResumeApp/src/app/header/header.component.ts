import { Component, OnInit, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { UserService } from '../user/user.service';
import { LanguageService } from '../shared/language.service';

@Injectable()
@Component({
  selector: 'openr-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  selectedLanguage: string;

  constructor(private router: Router,
              private userService: UserService,
              public languageService: LanguageService,
              private translate: TranslateService) { }

  ngOnInit() {
    this.selectedLanguage = this.languageService.Current();
    this.translate.use(this.selectedLanguage);
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

  LanguageChange(language) {
    this.languageService.Set(language);
  }

}
