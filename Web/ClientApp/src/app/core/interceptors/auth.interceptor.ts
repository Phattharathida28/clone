import { Injectable, Injector } from '@angular/core';

import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthService } from '../authentication/auth.service';


@Injectable({ providedIn: 'root' })
export class AuthInterceptor implements HttpInterceptor {
    private authService: AuthService;

    constructor(private injector: Injector) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let requestToForward = req;

        if (this.authService === undefined) this.authService = this.injector.get(AuthService);

        return this.authService.getAccessToken().pipe(
            switchMap(token => {
                requestToForward = req.clone({ setHeaders: { Authorization: token ?? '' } });

                return next.handle(requestToForward).pipe(catchError((err: HttpErrorResponse) => {
                    if (err.status == 401) this.authService.login()
                    return throwError(() => err);
                }))
            })
        )
    }
}