import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../user.service';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../user.model';
import { Router } from '@angular/router';
import { Resume } from '../../resume/resume.model';
import { ResumeService } from 'src/app/resume/resume.service';
import { MessageComponent } from 'src/app/shared/message/message.component';

@Component({
  selector: 'openr-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  @ViewChild('Message', {static: false}) Message: MessageComponent;
  resumes: Resume[];
  user: User;

  constructor(private userService: UserService,
              private resumeService: ResumeService,
              private translate: TranslateService,
              private router: Router) {
    this.translate.setDefaultLang('en-us');
    this.translate.use('en-us');
  }


  ngOnInit() {
    if (this.userService.IsUserLogged()) {
      this.user = this.userService.LoggedUser();
    } else {
      this.router.navigateByUrl('/login');
    }
    this.resumeService.List(this.user).subscribe(result => {
      this.resumes = result;
    }, error => {
      this.translate.get('USER.ERRORS.' + error.error).subscribe((text: string) => {
        this.Message.Show(text, true, 6000);
      });
    });
  }

}
