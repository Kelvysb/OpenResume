import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-skills-additional',
  templateUrl: './skills-additional.component.html',
  styleUrls: ['../euro.scss']
})
export class SkillsAdditionalComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
