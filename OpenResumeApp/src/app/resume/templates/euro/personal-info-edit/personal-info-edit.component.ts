import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-personal-info-edit',
  templateUrl: './personal-info-edit.component.html',
  styleUrls: ['../euro.scss']
})
export class PersonalInfoEditComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
