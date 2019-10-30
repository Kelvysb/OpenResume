import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ROUTES } from './app.routes';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from './user/login/login.component';
import { ChangePasswordComponent } from './user/change-password/change-password.component';
import { ForgetPasswordComponent } from './user/forget-password/forget-password.component';
import { NewUserComponent } from './user/new-user/new-user.component';
import { ConfirmEmailComponent } from './user/confirm-email/confirm-email.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { ResumeComponent } from './resume/resume/resume.component';
import { ResumeEditComponent } from './resume/resume-edit/resume-edit.component';
import { PersonalInfoComponent } from './resume/templates/euro/personal-info/personal-info.component';
import { PersonalInfoEditComponent } from './resume/templates/euro/personal-info-edit/personal-info-edit.component';
import { ProfessionalInfoComponent } from './resume/templates/euro/professional-info/professional-info.component';
import { ProfessionalInfoEditComponent } from './resume/templates/euro/professional-info-edit/professional-info-edit.component';
import { EducationalInfoComponent } from './resume/templates/euro/educational-info/educational-info.component';
import { EducationalInfoEditComponent } from './resume/templates/euro/educational-info-edit/educational-info-edit.component';
import { SkillsComponent } from './resume/templates/euro/skills/skills.component';
import { SkillsEditComponent } from './resume/templates/euro/skills-edit/skills-edit.component';
import { SkillsLanguagesComponent } from './resume/templates/euro/skills-languages/skills-languages.component';
import { SkillsLanguagesEditComponent } from './resume/templates/euro/skills-languages-edit/skills-languages-edit.component';
import { SkillsAdditionalComponent } from './resume/templates/euro/skills-additional/skills-additional.component';
import { SkillsAdditionalEditComponent } from './resume/templates/euro/skills-additional-edit/skills-additional-edit.component';
import { ResumeHeaderComponent } from './resume/templates/euro/resume-header/resume-header.component';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user/user.component';
import { InputContainerComponent } from './shared/input-container/input-container.component';
import { UserService } from './user/user.service';


export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    ChangePasswordComponent,
    ForgetPasswordComponent,
    NewUserComponent,
    ConfirmEmailComponent,
    ResetPasswordComponent,
    EditUserComponent,
    ResumeComponent,
    ResumeEditComponent,
    PersonalInfoComponent,
    PersonalInfoEditComponent,
    ProfessionalInfoComponent,
    ProfessionalInfoEditComponent,
    EducationalInfoComponent,
    EducationalInfoEditComponent,
    SkillsComponent,
    SkillsEditComponent,
    SkillsLanguagesComponent,
    SkillsLanguagesEditComponent,
    SkillsAdditionalComponent,
    SkillsAdditionalEditComponent,
    ResumeHeaderComponent,
    HomeComponent,
    UserComponent,
    InputContainerComponent
  ],
  imports: [
    UserService,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot(ROUTES, {preloadingStrategy: PreloadAllModules}),
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
      }
  })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
