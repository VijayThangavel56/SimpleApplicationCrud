import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import type { Employee } from '../services/employee.service';
import { API_BASE_URL } from '../tokens';

@Injectable({ providedIn: 'root' })
export class EmployeeApiClient {
  private readonly base: string;

  constructor(private http: HttpClient, @Inject(API_BASE_URL) baseUrl: string) {
    this.base = `${baseUrl.replace(/\/$/, '')}/api/employees`;
  }

  getAll(): Observable<Employee[]> { return this.http.get<Employee[]>(this.base); }
  get(id: number): Observable<Employee> { return this.http.get<Employee>(`${this.base}/${id}`); }
  create(employee: Employee): Observable<Employee> { return this.http.post<Employee>(this.base, employee); }
  update(id: number, employee: Employee): Observable<void> { return this.http.put<void>(`${this.base}/${id}`, employee); }
  delete(id: number): Observable<void> { return this.http.delete<void>(`${this.base}/${id}`); }
}
