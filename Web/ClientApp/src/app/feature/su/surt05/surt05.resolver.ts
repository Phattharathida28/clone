import { ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Surt05Service } from './surt05.service';
import { Message } from '@app/models/su/message';
import { inject } from '@angular/core';

export const messages: ResolveFn<Message[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => inject(Surt05Service).list()

export const message: ResolveFn<Message> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const { messageCode } = inject(Router).getCurrentNavigation()?.extras.state as { messageCode: string } || { messageCode: null }

    return inject(Surt05Service).detail(messageCode)
}