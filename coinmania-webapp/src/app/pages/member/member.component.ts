import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { FormsErrorHandler } from 'src/app/commons/utils/forms-error-handler.class';
import { AuthService } from 'src/app/services/auth.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
    selector: 'app-member',
    templateUrl: './member.component.html',
    styleUrls: ['./member.component.scss'],
})
export class MemberComponent implements OnInit {
    public user: any;
    public data: any;

    constructor(
        private _authService: AuthService,
        private _router: Router,
        private _route: ActivatedRoute,
        private fb: FormBuilder,
        private _snackbarService: SnackbarService
    ) {
        this.user = JSON.parse(localStorage.getItem("user")!);
        this.formErrorHandler = new FormsErrorHandler(this.form);
    }

    ngOnInit(): void {
        this._authService.loadUsers("", "", "").subscribe({
            next: (result) => {
                this.data = result;
            }
        });
    }

    public onLogout() {
        this._authService.logout();
    }

    public isFormProcessing: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    public form = this.fb.group({
        phone: ["", [Validators.maxLength(9)]],
        firstname: ["", []],
        lastname: ["", []]
    });
    public formErrorHandler: FormsErrorHandler | undefined;

    public onSearch() {
        if (!this.isFormProcessing.value && this.form.valid) {
            this.isFormProcessing.next(true);
            const value = this.form.value;
            this._authService.loadUsers(value.phone!, value.firstname!, value.lastname!).subscribe({
                next: (result) => {
                    this.data = result;
                    this._snackbarService.success('Users loaded.');
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
}
