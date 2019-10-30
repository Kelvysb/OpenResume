import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
