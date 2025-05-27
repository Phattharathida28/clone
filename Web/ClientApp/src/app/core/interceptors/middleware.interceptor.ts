import { Inject, Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MiddlewareInterceptor implements HttpInterceptor {

    constructor(@Inject('BASE_URL') private baseUrl: string) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const reg = new RegExp(this.baseUrl);

        if (reg.test(request.url)) return next.handle(request).pipe(
            map((res) => {
                if (res instanceof HttpResponse) {
                    if (Array.isArray(res.body)) res.body.map(m => this.convertStringToDate(m))
                    else this.convertStringToDate(res.body)
                }
                return res
            }),
        );
        else return next.handle(request);
    }

    private convertStringToDate(data: any) {
        for (const key in data) {
            if (data[key] && typeof data[key] != 'boolean' && typeof data[key] != 'number' && (data[key]?.toString().length > 13 && key.includes('date'))) {
                const date = new Date(data[key])

                if (!isNaN(date.getTime())) data[key] = date as Date
            }

            if (Array.isArray(data[key])) this.convertStringToDate(data[key])
        }
    }
}
