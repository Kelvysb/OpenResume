import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from 'src/app/shared/language.service';

@Component({
  selector: 'openr-new-user-confirmation',
  templateUrl: './new-user-confirmation.component.html'
})
export class NewUserConfirmationComponent implements OnInit {

  constructor(private translate: TranslateService,
              private languageService: LanguageService) { }

  ngOnInit() {
    this.translate.use(this.languageService.Current());
  }

}
