import { Component, inject, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Employee, EmployeeService } from '../services/employee.service';

@Component({
  selector: 'employee-form',
  standalone: false,
  templateUrl: './employee-form.component.html'
})
export class EmployeeFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly svc = inject(EmployeeService);
  private readonly router = inject(Router);
  private readonly location = inject(Location);

  form!: FormGroup;
  private readonly editingId = (this.router.getCurrentNavigation()?.extras?.state as { employee?: Employee } | undefined)?.employee?.id;

  get isEditing(): boolean { return this.editingId !== undefined; }

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.maxLength(100)]],
      email: ['', [Validators.email, Validators.maxLength(200)]],
      position: ['', [Validators.maxLength(100)]],
      dateOfJoining: ['']
    });

    if (this.editingId !== undefined) {
      this.svc.get(this.editingId).subscribe(employee => {
        if (employee) {
          this.form.patchValue({
            firstName: employee.firstName,
            lastName: employee.lastName,
            email: employee.email,
            position: employee.position,
            dateOfJoining: employee.dateOfJoining ? employee.dateOfJoining.split('T')[0] : ''
          });
        }
      });
    }
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload: Employee = {
      id: this.editingId,
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      email: this.form.value.email,
      position: this.form.value.position,
      dateOfJoining: this.form.value.dateOfJoining
    };

    if (this.editingId !== undefined) {
      this.svc.update(this.editingId, payload).subscribe(success => {
        if (success) this.location.back();
      });
    } else {
      this.svc.create(payload).subscribe(employee => {
        if (employee) this.location.back();
      });
    }
  }

  cancel(): void { this.location.back(); }
}
