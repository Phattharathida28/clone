import { Inject, Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { forkJoin, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoadingService } from '@app/pages/loading/loading.service';
import { NotifyService } from '../services/notify.service';
import { Router } from '@angular/router';

export type HandledError = {
  status: number;
  message: string;
}
export interface ErrorModel {
  code: string,
  parameters: string[]
}
@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerInterceptor implements HttpInterceptor {

  constructor(private loading: LoadingService, @Inject('BASE_URL') private baseUrl: string, private ns: NotifyService, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const reg = new RegExp(this.baseUrl);

    if (reg.test(request.url)) return next.handle(request).pipe(catchError(error => this.handleError(error)));
    else return next.handle(request);
  }

  private handleError(errorResponse: HttpErrorResponse): Observable<HttpEvent<any>> {
    this.loading.forceHide();

    if (errorResponse.status === 0) this.ns.error(errorResponse.message)
    else this.handleBackendError(errorResponse);

    const errer: HandledError = { status: errorResponse.status, message: errorResponse.message }

    return throwError(() => errer);
  }

  private handleBackendError(errorResponse: HttpErrorResponse) {
    switch (errorResponse.status) {
      case 504: this.ns.error("message.STD00005")
        break;
      case 403: this.router.navigate(["/access-denied"], { replaceUrl: true });
        break;
      case 401: this.router.navigate(["/"], { replaceUrl: true });
        break;
      case 500: this.ns.error(errorResponse.error.code)
        break;
      case 400:
      case 404:
        const backendError = errorResponse.error;
        if (!backendError.code && errorResponse.status == 404) {
          this.ns.error("message.STD00006", [errorResponse.url])
        }
        else if (backendError.errors) {
          if (backendError.single) {
            forkJoin((backendError.errors as ErrorModel[]).map((item: ErrorModel) => this.ns.translatedMessage(item.code, item.parameters))).subscribe((translates: string[]) => {
              this.ns.error(translates.join("<br>"));
            })
          }
          else (backendError.errors as ErrorModel[]).forEach((item: ErrorModel | string) => {
            if (typeof item === 'object') this.ns.error(item.code, item.parameters)
            else {
              let errorMessage = item as string;
              this.ns.error(errorMessage)
            }
          });
        }
        else if (backendError.code) {
          this.ns.error(backendError.code, backendError.parameters)
        }
        else if ((backendError as string).includes('code')) {
          const message = JSON.parse(backendError);
          this.ns.error(message.code, message.parameters)
        }
        else this.ns.error(backendError)
        break;
    }
  }

}