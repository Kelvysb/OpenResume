import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-skills-languages',
  templateUrl: './skills-languages.component.html',
  styleUrls: ['../euro.scss']
})
export class SkillsLanguagesComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
