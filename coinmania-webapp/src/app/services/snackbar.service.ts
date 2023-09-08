import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

enum Type {
    success = 'toast-success',
    fail = 'toast-fail',
}

@Injectable({
    providedIn: 'root',
})
export class SnackbarService {
    public static Type: Type;

    constructor(private _snackBar: MatSnackBar) {}

    public success(message: string, duration: number = 5000): void {
        this.show(Type.success, message, duration);
    }

    public fail(message: string, duration: number = 5000): void {
        this.show(Type.fail, message, duration);
    }

    public show(type: Type, message: string, duration: number = 5000): void {
        this._snackBar.open(message, '', { duration, panelClass: [type] });
    }
}
