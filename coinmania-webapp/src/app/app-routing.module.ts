import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { GuestComponent } from './pages/guest/guest.component';
import { SignInComponent } from './pages/guest/sign-in/sign-in.component';
import { MemberComponent } from './pages/member/member.component';
import { SignUpComponent } from './pages/guest/sign-up/sign-up.component';
import { LoggedInAuthGuard } from './guards/loggedin.auth.guard';
import { ConfirmationComponent } from './pages/guest/confirmation/confirmation.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'member',
    pathMatch: 'full',
  },
  {
    path: 'guest',
    component: GuestComponent,
    canActivate: [LoggedInAuthGuard],
    children: [
        { path: '', redirectTo: 'sign-in', pathMatch: 'full' },
        { path: 'sign-in', component: SignInComponent },
        { path: 'sign-up', component: SignUpComponent },
        { path: 'confirmation', component: ConfirmationComponent }
    ],
  }, 
  {
    path: 'member',
    component: MemberComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
