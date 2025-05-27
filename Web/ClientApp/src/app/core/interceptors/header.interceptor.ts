import { Inject, Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuditService } from '../authentication/audit.service';
import { I18nService } from '../services/i18n.service';

@Injectable({
    providedIn: 'root'
})
export class HeaderInterceptor implements HttpInterceptor {
    constructor(@Inject('BASE_URL') private baseUrl: string,private audit: AuditService, private i18n: I18nService, private injector: Injector) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const reg = new RegExp(this.baseUrl);
        
        if (reg.test(request.url)) {
            let headers = Object.assign({}, {
                language: this.i18n.language || this.i18n.defaultLanguage,
            })

            if (this.audit.code) Object.assign(headers, { program: this.audit.code.toUpperCase() });
           
            return next.handle(request.clone({ setHeaders: headers }));
        }

        return next.handle(request);
    }
}   