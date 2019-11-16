import { Injectable, NgModule } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { User } from './user.model';
import { environment } from 'src/environments/environment';
import { Observable, pipe } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
@NgModule()
export class UserService {

    user: User;

    constructor(private http: HttpClient,
                private cookieService: CookieService) {
                    if (this.cookieService.check('User')) {
                        this.user = JSON.parse(this.cookieService.get('User'));
                    }
                }

    Login(user: User): Observable<User> {

        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json',
              APIKey: environment.apikey
            })
        };
        return this.http.post<User>(`${environment.api}/user/login`, user, httpOptions);
    }

    Create(user: User): Observable<object> {

        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json',
              APIKey: environment.apikey
            })
        };
        return this.http.put<object>(`${environment.api}/user`, user, httpOptions);
    }

    Loggout() {
        this.cookieService.delete('User');
    }

    SetLoggegUser(user: User) {
        this.cookieService.set('User', JSON.stringify(user), 1);
        this.user = user;
    }

    IsUserLogged(): boolean {
        return this.cookieService.check('User');
    }

    LoggedUser(): User {
        return this.user;
    }
}
