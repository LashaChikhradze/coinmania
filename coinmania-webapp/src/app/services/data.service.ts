import { Injectable } from "@angular/core";

@Injectable({providedIn: "root"})
export class DataService {
    private operation: "sign-up" | "sign-in" | undefined;
    private phone: any;

    public createData(phone: any, operation: any) {
        this.operation = operation;
        this.phone = phone;
    }

    public getData() {
        const args = {
            phone: this.phone,
            operation: this.operation
        }

        return args;
    }

    public clear() {
        this.phone = null;
    }
}