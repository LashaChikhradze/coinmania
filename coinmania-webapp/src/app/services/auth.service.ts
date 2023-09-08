import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, catchError, map, tap, throwError } from "rxjs";
import { User } from "../models/user.model";
import { SignupModel } from "../models/signup-user.model";
import { LoginModel } from "../models/login-user.model";
import { LoadUsersModel } from "../models/load-users.model";
import { FindUserModel } from "../models/find-user.model";
import { ResponseHandler } from "../commons/utils/response-handler.class";
import { ConfirmationModel } from "../models/confirmation.model";

@Injectable({providedIn: 'root'})
export class AuthService {
    public static user: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private http: HttpClient, private router: Router) {}

    autoLogin() {
        const userData = localStorage.getItem('user');
        const token = localStorage.getItem('auth-token');
        if(!userData || !token) {
            return;
        }
        const user: {
            phone: string;
            firstName: string;
            lastName: string;
            id: string;
        } = JSON.parse(userData);

        const loadedUser = new User(user.phone, user.firstName, user.lastName, user.id, token);
        AuthService.user.next(loadedUser);
    }

    logout() {
        AuthService.user.next(null);
        localStorage.clear();
        this.router.navigate(['/guest']);
    }

    signup(firstname: string, lastname: string, phone: string, password: string) {
        return this.http.post(
            "/api/User/RegisterUser", new SignupModel(phone, firstname, lastname, password)
        ).pipe();
    }

    login(phone: string, password: string) {
        return this.http.post("/api/User/LogIn", new LoginModel(phone, password)).pipe(map(ResponseHandler.success));
    }

    public loadUsers(phone: string, firstname: string, lastname: string) {
        return this.http.post("/api/User/users", new LoadUsersModel(phone, firstname, lastname)).pipe(map(ResponseHandler.success));
    }

    public findUser(phone: string) {
        return this.http.post("/api/User/find", new FindUserModel(phone)).pipe(map(ResponseHandler.success));
    }

    public sendSms(phone: string, token: string) {
        return this.http.get(`/api/sendsms/${phone}/${token}`);
    }

    public confirmPhone(phone: string, token: string, operation: "sign-up" | "sign-in") {
        if(operation == "sign-up") {
            return this.http.post("/api/User/sign-up/confirm", new ConfirmationModel(phone, token)).pipe(map(ResponseHandler.success));
        } else {
            return this.http.post("/api/User/sign-in/confirm", new ConfirmationModel(phone, token)).pipe(
                tap((resData: any) => {
                    const user = new User(resData.data.phone, resData.data.firstName, resData.data.lastName, resData.data.id, resData.token);
                    AuthService.user.next(user);
                    localStorage.setItem('user', JSON.stringify(resData.data));
                    localStorage.setItem('auth-token', resData.token);
                })
            );
        }
    }

}