import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AuthInterceptorService } from './interceptors/auth-interceptor.service';
import { BootstrapComponent } from './boostrap/bootstrap.component';
import { GuestComponent } from './pages/guest/guest.component';
import { MemberComponent } from './pages/member/member.component';
import { SignInComponent } from './pages/guest/sign-in/sign-in.component';
import { SignUpComponent } from './pages/guest/sign-up/sign-up.component';
import { ConfirmationComponent } from './pages/guest/confirmation/confirmation.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field'

@NgModule({
  declarations: [
    BootstrapComponent,
    GuestComponent,
    SignInComponent,
    SignUpComponent,
    MemberComponent,
    ConfirmationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    MatFormFieldModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }
  ],
  bootstrap: [BootstrapComponent]
})
export class AppModule { }
