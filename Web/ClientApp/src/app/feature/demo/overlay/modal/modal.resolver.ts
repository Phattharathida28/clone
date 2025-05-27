import { ModalResolve } from '@app/shared/components/modal/modal.resolver';
import { of } from 'rxjs';

export const modalResolver: ModalResolve<any> = {
  resolve(resolverParam) {
    return of(resolverParam)
  },
}
