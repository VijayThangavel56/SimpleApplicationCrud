import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  constructor(private snack: MatSnackBar) {}

  success(message: string): void {
    this.snack.open(message, 'Close', { duration: 3000, panelClass: ['snack-success'] });
  }

  error(message: string): void {
    this.snack.open(message, 'Close', { duration: 5000, panelClass: ['snack-error'] });
  }
}
