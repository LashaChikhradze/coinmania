import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { FormsErrorHandler } from 'src/app/commons/utils/forms-error-handler.class';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent {
    public isFormProcessing: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    public form = this.fb.group({
        phone: [null, [Validators.required, Validators.minLength(9), Validators.maxLength(9), Validators.pattern("^[0-9]*$")]],
        firstname: [null, [Validators.required]],
        lastname: [null, [Validators.required]],
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

    public signup() {
        if (!this.isFormProcessing.value && this.form.valid) {
            this.isFormProcessing.next(true);
            const value = this.form.value;
            this.dataService.createData(value.phone, "sign-up");
            this.authService.signup(value.firstname!, value.lastname!, value.phone!, value.password!).subscribe({
                next: () => {
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

    public toSignIn() {
        this._router.navigate(['../sign-in'], {relativeTo: this._route});
    }
}
