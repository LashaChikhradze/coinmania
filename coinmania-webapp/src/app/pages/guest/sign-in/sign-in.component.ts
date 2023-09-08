import { Component } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, catchError, of, switchMap } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { FormsErrorHandler } from '../../../commons/utils/forms-error-handler.class';
import { DataService } from 'src/app/services/data.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent {
    public isFormProcessing: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    public form = this.fb.group({
        phone: [null, [Validators.required, Validators.minLength(9), Validators.maxLength(9), Validators.pattern("^[0-9]*$")]],
        password: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
    });
    public formErrorHandler: FormsErrorHandler | undefined;

    constructor(
        private authService: AuthService,
        private _router: Router,
        private _route: ActivatedRoute,
        private fb: FormBuilder,
        private dataService: DataService,
        private _snackbarService: SnackbarService
    ) {
        this.formErrorHandler = new FormsErrorHandler(this.form);
    }

    public login() {
        if (!this.isFormProcessing.value && this.form.valid) {
            this.isFormProcessing.next(true);
            const value = this.form.value;
            this.authService.login(value.phone!, value.password!).subscribe({
                next: () => {
                    this.dataService.createData(value.phone, "sign-in");
                    this._router.navigate(['../','confirmation'], {relativeTo: this._route});
                }, 
                error: (error: any) => {
                    this._snackbarService.fail('Something went wrong.');
                    this.isFormProcessing.next(false);
                }, 
                complete: () => {
                    this.isFormProcessing.next(false);
                }
            });
        }
    }

    public toSignUp() {
        this._router.navigate(['../sign-up'], {relativeTo: this._route});
    }
}
