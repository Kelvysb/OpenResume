import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './user/login/login.component';
import { NewUserComponent } from './user/new-user/new-user.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { ForgetPasswordComponent } from './user/forget-password/forget-password.component';
import { ConfirmEmailComponent } from './user/confirm-email/confirm-email.component';
import { ResumeComponent } from './resume/resume/resume.component';
import { ResumeEditComponent } from './resume/resume-edit/resume-edit.component';
import { UserComponent } from './user/user/user.component';
import { NewUserConfirmationComponent } from './user/new-user/new-user-confirmation/new-user-confirmation.component';

export const ROUTES: Routes = [
    {path: '', component: HomeComponent},
    {path: 'login', component: LoginComponent},
    {path: 'user', component: UserComponent},
    {path: 'newuser', component: NewUserComponent},
    {path: 'newuser/confirmation', component: NewUserConfirmationComponent},
    {path: 'edit', component: EditUserComponent},
    {path: 'forgetpassword', component: ForgetPasswordComponent},
    {path: 'resetpassword', component: ResetPasswordComponent},
    {path: 'confirmemail', component: ConfirmEmailComponent},
    {path: 'about', component: AppComponent},
    {path: 'edit/:resume', component: ResumeEditComponent},
    {path: ':user/:resume', component: ResumeComponent}
];
