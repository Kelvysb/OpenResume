import {Routes} from '@angular/router';
import { AppComponent } from './app.component';

export const ROUTES: Routes = [
    {path: '', component: AppComponent},
    {path: 'login', component: AppComponent},
    {path: 'newuser', component: AppComponent},
    {path: 'edit', component: AppComponent},
    {path: 'recoverpassword', component: AppComponent},
    {path: 'resetpassword', component: AppComponent},
    {path: 'confirmemail', component: AppComponent},
    {path: 'about', component: AppComponent},
    {path: ':user/:resume', component: AppComponent},
    {path: 'view/:resume', component: AppComponent}
];
