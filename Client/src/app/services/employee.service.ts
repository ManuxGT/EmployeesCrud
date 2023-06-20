import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/employee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
 })
export class EmployeeService {

  constructor(private readonly _http: HttpClient) { }

  private readonly _baseURL: string = `${environment.api}/Employee`;

  public getAll(currentPage: number): Observable<Array<Employee>> {
    return this._http.get<Array<Employee>>(this._baseURL, { params: { currentPage }});
  }

  public getById(employeeId: string): Observable<Employee> {
    return this._http.get<Employee>(`${this._baseURL}/${employeeId}`);
  }

  public create(employee: Employee): Observable<Employee> {
    return this._http.post<Employee>(`${this._baseURL}`, employee);
  }

  public update(employee: Employee): Observable<Employee> {
    return this._http.put<Employee>(`${this._baseURL}`, employee);
  }

  public delete(employeeId: string) {
    return this._http.delete(`${this._baseURL}/${employeeId}`);
  }
}
