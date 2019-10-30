import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-skills-languages-edit',
  templateUrl: './skills-languages-edit.component.html',
  styleUrls: ['../euro.scss']
})
export class SkillsLanguagesEditComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
