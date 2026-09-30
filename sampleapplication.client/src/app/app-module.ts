import { HttpClientModule } from '@angular/common/http';
import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { AppRoutingModule } from './routes/app-routing-module';
import { App } from './app';
import { API_BASE_URL } from './tokens';
import { environment } from '../environments/environment';
import { EmployeeListComponent } from './components/employee-list.component';
import { EmployeeFormComponent } from './components/employee-form.component';

@NgModule({
  declarations: [
    App,
    EmployeeListComponent,
    EmployeeFormComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, ReactiveFormsModule, BrowserAnimationsModule, MatSnackBarModule,
    MatFormFieldModule, MatInputModule, MatTableModule, MatButtonModule, MatIconModule, MatToolbarModule, MatCardModule,
    AppRoutingModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    { provide: API_BASE_URL, useValue: environment.apiUrl }
  ],
  bootstrap: [App]
})
export class AppModule { }
