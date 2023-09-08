import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpParams, HttpRequest } from '@angular/common/http'
import { exhaustMap, Observable, take } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const authToken = localStorage.getItem("auth-token");
        if (authToken) {
            request = request.clone({
                setHeaders: { Authorization: `Bearer ${authToken}` },
            });
        }
        return AuthService.user.pipe(
            take(1),
            exhaustMap(event => {
                if(!event) {
                    return next.handle(request);
                }
                const modifiedReq = request.clone({ body: event.body })
                return next.handle(modifiedReq);
            })
        );
    }
}