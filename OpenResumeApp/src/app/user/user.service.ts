import { Injectable, NgModule } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { User } from './user.model';
import { environment } from 'src/environments/environment';
import { Observable, pipe } from 'rxjs';

@Injectable()
@NgModule()
export class UserService {
    constructor(private http: HttpClient) {}

    login(user: User): Observable<User> {

        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json'
            })
        };
        return this.http.post<User>(`${environment.api}/user/login`, user, httpOptions)
            .pipe();
    }
}
