import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
    providedIn: 'root'
  })
  export class UsersService {
  
    private _baseUrl: string = `${environment.apiUrl}/users`;
  
    constructor(
      private http: HttpClient
    ) { }
  
    public get(id: number): Observable<User> {
      return this.http.get<User>(`${this._baseUrl}/${id}`);
    }
  
    public filter(value: any): Observable<User[]> {
      let params = new HttpParams()
      .append('name', value.name)
      .append('currentPage', value.currentPage)
      .append('pageLength', value.pageLength);
  
      return this.http.get<User[]>(`${this._baseUrl}/filter`, { params });
    }

    
    public post(user: User): Observable<any> {
      return this.http.post<any>(this._baseUrl, user);
    }
  
    public put(user: User): Observable<any> {
      return this.http.put<any>(`${this._baseUrl}/${user.id}`, user);
    }
    
  }
  