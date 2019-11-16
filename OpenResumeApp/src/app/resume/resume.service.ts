import { NgModule, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Resume } from './resume.model';
import { User } from '../user/user.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable()
@NgModule()
export class ResumeService {
    constructor(private http: HttpClient) { }
    List(user: User): Observable<Resume[]> {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type' : 'application/json; charset=utf-8',
                Accept: 'application/json',
                Authorization: `Bearer ${user.token}`
            })
        };
        return this.http.get<Resume[]>(`${environment.api}/Resume/${user.id}`, httpOptions);
    }
}
