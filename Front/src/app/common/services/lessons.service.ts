import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Lessons } from '../models/lessons';

@Injectable({
    providedIn: 'root'
  })
  export class LessonsService {
  
    private _baseUrl: string = `${environment.apiUrl}/lessons`;
  
    constructor(
      private http: HttpClient
    ) { }
  
    public get(id: number): Observable<Lessons> {
      return this.http.get<Lessons>(`${this._baseUrl}/${id}`);
    }
  
    public filter(value: any): Observable<Lessons[]> {
      let params = new HttpParams()
         .append('name', value.name)
         .append('currentPage', value.currentPage)
         .append('pageLength', value.pageLength);

      return this.http.get<Lessons[]>(`${this._baseUrl}/filter`, { params });
    }

    public post(lesson: Lessons): Observable<any> {
      return this.http.post<any>(this._baseUrl, lesson);
    }
  
    public put(lesson: Lessons): Observable<any> {
      return this.http.put<any>(`${this._baseUrl}/${lesson.id}`, lesson);
    }
    
  }
  