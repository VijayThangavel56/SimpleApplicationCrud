import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { EmployeeApiClient } from '../clients/employee-api-client';
import { NotificationService } from './notification.service';

export interface Employee {
  id?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  dateOfJoining?: string;
  position?: string;
}

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  constructor(private client: EmployeeApiClient, private notify: NotificationService) {}

  getAll(): Observable<Employee[]> {
    return this.client.getAll().pipe(
      catchError(() => {
        this.notify.error('Failed to load employees');
        return of([] as Employee[]);
      })
    );
  }

  get(id: number): Observable<Employee | null> {
    return this.client.get(id).pipe(
      catchError(() => {
        this.notify.error('Failed to load employee');
        return of(null);
      })
    );
  }

  create(employee: Employee): Observable<Employee | null> {
    return this.client.create(employee).pipe(
      tap(() => this.notify.success('Employee created')),
      catchError(() => {
        this.notify.error('Failed to create employee');
        return of(null);
      })
    );
  }

  update(id: number, employee: Employee): Observable<boolean> {
    return this.client.update(id, employee).pipe(
      map(() => true),
      tap(() => this.notify.success('Employee updated')),
      catchError(() => {
        this.notify.error('Failed to update employee');
        return of(false);
      })
    );
  }

  delete(id: number): Observable<boolean> {
    return this.client.delete(id).pipe(
      map(() => true),
      tap(() => this.notify.success('Employee deleted')),
      catchError(() => {
        this.notify.error('Failed to delete employee');
        return of(false);
      })
    );
  }
}
