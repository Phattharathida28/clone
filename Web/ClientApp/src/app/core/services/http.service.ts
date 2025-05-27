import { Inject, Injectable, InjectionToken, Injector, Optional, Type } from '@angular/core';
import { HttpClient, HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpParams, HttpParamsOptions } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiPrefixInterceptor } from '../interceptors/api-prefix.interceptor';
import { LoadingInterceptor } from '../interceptors/loading.interceptor';
import { ErrorHandlerInterceptor } from '../interceptors/error-handler.interceptor';
import { HeaderInterceptor } from '../interceptors/header.interceptor';
import { AuthInterceptor } from '../interceptors/auth.interceptor';
import { Encoder } from './encoder';
import { MiddlewareInterceptor } from '../interceptors/middleware.interceptor';


declare module '@angular/common/http' {
  export interface HttpClient {
    skipErrorHandler(): HttpClient;
    disableApiPrefix(): HttpClient;
    disableLoading(): HttpClient;
    disableHeader(): HttpClient;
    disableAuthen(): HttpClient;
  }
}

class HttpInterceptorHandler implements HttpHandler {

  constructor(private next: HttpHandler, private interceptor: HttpInterceptor) { }

  handle(request: HttpRequest<any>): Observable<HttpEvent<any>> {
    return this.interceptor.intercept(request, this.next);
  }

}

export const HTTP_DYNAMIC_INTERCEPTORS = new InjectionToken<HttpInterceptor>('HTTP_DYNAMIC_INTERCEPTORS');

@Injectable({
  providedIn: 'root'
})
export class HttpService extends HttpClient {

  constructor(private httpHandler: HttpHandler,
    private injector: Injector,
    @Optional() @Inject(HTTP_DYNAMIC_INTERCEPTORS) private interceptors: HttpInterceptor[] = []) {
    super(httpHandler);

    if (!this.interceptors) {
      this.interceptors = [
        this.injector.get(ApiPrefixInterceptor),
        this.injector.get(LoadingInterceptor),
        this.injector.get(ErrorHandlerInterceptor),
        this.injector.get(HeaderInterceptor),
        this.injector.get(AuthInterceptor),
        this.injector.get(MiddlewareInterceptor),
      ];
    }
  }

  override request(method?: any, url?: any, options?: any): any {
    const handler = this.interceptors.reduceRight(
      (next, interceptor) => new HttpInterceptorHandler(next, interceptor),
      this.httpHandler
    );
    let params: HttpParams | undefined = undefined;
    if (!!options.params) {
      if (options.params instanceof HttpParams) {
        params = options.params;
      } else {
        params = new HttpParams({ fromObject: options.params, encoder: new Encoder() } as HttpParamsOptions);
      }
      options.params = params;
    }
    return new HttpClient(handler).request(method, url, options);
  }

  override skipErrorHandler(): HttpClient {
    return this.removeInterceptor(ErrorHandlerInterceptor);
  }

  override disableApiPrefix(): HttpClient {
    return this.removeInterceptor(ApiPrefixInterceptor);
  }

  override disableLoading(): HttpClient {
    return this.removeInterceptor(LoadingInterceptor);
  }

  override disableHeader(): HttpClient {
    return this.removeInterceptor(HeaderInterceptor);
  }

  override disableAuthen(): HttpClient {
    return this.removeInterceptor(AuthInterceptor);
  }

  private removeInterceptor(interceptorType: Type<HttpInterceptor>): HttpService {
    return new HttpService(
      this.httpHandler,
      this.injector,
      this.interceptors.filter(i => !(i instanceof interceptorType))
    );
  }

}