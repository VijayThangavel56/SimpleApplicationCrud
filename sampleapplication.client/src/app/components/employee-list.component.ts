import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { Employee, EmployeeService } from '../services/employee.service';

@Component({
  selector: 'employee-list',
  standalone: false,
  templateUrl: './employee-list.component.html'
})
export class EmployeeListComponent implements OnInit {
  private readonly svc = inject(EmployeeService);
  private readonly router = inject(Router);

  employees: Employee[] = [];
  dataSource = new MatTableDataSource<Employee>([]);
  displayedColumns = ['id', 'name', 'email', 'position', 'actions'];

  ngOnInit(): void { this.refresh(); }

  refresh(): void {
    this.svc.getAll().subscribe(employees => {
      this.employees = employees;
      this.dataSource.data = employees;
    });
  }

  filterEmployees(value: string): void { this.dataSource.filter = value.trim().toLowerCase(); }
  create(): void { void this.router.navigate(['/employees/new']); }
  edit(employee: Employee): void { void this.router.navigate(['/employees/new'], { state: { employee } }); }
  remove(employee: Employee): void {
    if (employee.id !== undefined && confirm('Delete this employee?')) {
      this.svc.delete(employee.id).subscribe(() => this.refresh());
    }
  }
}
