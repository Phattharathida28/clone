import { Observable } from 'rxjs';

export type ModalResolve<T> = {
  resolve(resolverParam?: any): Observable<T>
}