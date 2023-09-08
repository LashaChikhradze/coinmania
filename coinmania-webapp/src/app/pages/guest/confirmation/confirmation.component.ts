import { Component, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { FormsErrorHandler } from 'src/app/commons/utils/forms-error-handler.class';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
    selector: 'app-confirmaiton',
    templateUrl: './confirmation.component.html',
    styleUrls: ['./confirmation.component.scss'],
})
export class ConfirmationComponent { 
    private _phone: any;
    private _operation: "sign-up" | "sign-in" | undefined;

    constructor(private _dataService: DataService, private _snackbarService: SnackbarService, private _authService: AuthService, private _router: Router, private _route: ActivatedRoute, private fb: FormBuilder,) {
        const data = this._dataService.getData();
        this._phone = data.phone;
        this._operation = data.operation;
    }

    public isFormProcessing: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    public form = this.fb.group({
        token: [null, [Validators.required]]
    });
    public formErrorHandler: FormsErrorHandler | undefined;

    public confirm() {
        if (!this.isFormProcessing.value && this.form.valid) {
            this.isFormProcessing.next(true);
            const value = this.form.value;
            this._authService.confirmPhone(this._phone, value.token!, this._operation!).subscribe({
               next: () => {
                    this._snackbarService.success(this._operation + " was successful");
                    if(this._operation == "sign-up") {
                        this._router.navigate(['../sign-in'], { relativeTo: this._route });
                    } else if(this._operation == "sign-in"){
                        this._router.navigate(['/', 'member']);
                    }
               },
               error: () => {
                    this._snackbarService.fail("Something went wrong.");
                    this._dataService.clear();
                    this.isFormProcessing.next(false);
                    this._router.navigate(['../', this._operation]);
               },
               complete: () => {
                    this._dataService.clear();
                    this.isFormProcessing.next(false);
               }
            });
        }
    }

}