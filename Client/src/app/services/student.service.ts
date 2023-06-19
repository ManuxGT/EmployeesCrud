import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Student } from '../models/student.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
 })
export class StudentService {

  constructor(private readonly _http: HttpClient) { }

  private readonly _baseURL: string = `${environment.api}/Student`;

  public getAll(): Observable<Array<Student>> {
    return this._http.get<Array<Student>>(this._baseURL);
  }

  public getById(studentId: string): Observable<Student> {
    return this._http.get<Student>(`${this._baseURL}/${studentId}`);
  }

  public create(student: Student): Observable<Student> {
    return this._http.post<Student>(`${this._baseURL}`, student);
  }

  public update(student: Student): Observable<Student> {
    return this._http.put<Student>(`${this._baseURL}`, student);
  }

  public delete(studentId: string) {
    return this._http.delete(`${this._baseURL}/${studentId}`);
  }
}
