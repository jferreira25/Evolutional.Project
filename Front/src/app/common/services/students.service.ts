import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Students } from '../models/students';

@Injectable({
    providedIn: 'root'
  })
  export class StudentsService {
  
    private _baseUrl: string = `${environment.apiUrl}/students`;
  
    constructor(
      private http: HttpClient
    ) { }
  
    public get(id: number): Observable<Students> {
      return this.http.get<Students>(`${this._baseUrl}/${id}`);
    }
  
    public filter(value: any): Observable<Students[]> {
      let params = new HttpParams()
      .append('name', value.name)
      .append('currentPage', value.currentPage)
      .append('pageLength', value.pageLength);
  
      return this.http.get<Students[]>(`${this._baseUrl}/filter`, { params });
    }
    
    public post(students: Students): Observable<any> {
      return this.http.post<any>(this._baseUrl, students);
    }
  
    public put(students: Students): Observable<any> {
      return this.http.put<any>(`${this._baseUrl}/${students.id}`, students);
    }

    public download(): Observable<any> {
      return this.http.get<any>(`${this._baseUrl}/download`);
    }

    public generate(): Observable<any> {
      return this.http.post<any>(`${this._baseUrl}/generate`,null);
    }
  }
  