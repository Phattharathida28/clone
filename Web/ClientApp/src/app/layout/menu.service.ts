import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Subject, map } from 'rxjs';


export interface MenuChangeEvent {
    key: string;
    routeEvent?: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class MenuService {

    private menuSource = new Subject<MenuChangeEvent>();
    private resetSource = new Subject();

    menuSource$ = this.menuSource.asObservable();
    resetSource$ = this.resetSource.asObservable();

    constructor(private http: HttpClient) { }

    onMenuStateChange(event: MenuChangeEvent) {
        this.menuSource.next(event);
    }

    reset() {
        this.resetSource.next(true);
    }

    getMenuList = () => {
        const demo = [
            {
                label: 'Demo',
                icon: "pi pi-desktop",
                items: [
                    {
                        label: "Trainning",
                        icon: "pi pi-folder",
                        items: [
                            { label: 'Life Cycle', icon: 'pi pi-fw pi-circle', routerLink: ["/demo/lifecycle"] },
                            { label: 'Binding', icon: 'pi pi-fw pi-circle', routerLink: ["/demo/binding"] }
                        ]
                    },
                    { label: 'Form Layout', icon: 'pi pi-fw pi-id-card', routerLink: ['/demo/formlayout'] },
                    { label: 'Input', icon: 'pi pi-fw pi-check-square', routerLink: ['/demo/input'] },
                    { label: 'Float Label', icon: 'pi pi-fw pi-bookmark', routerLink: ['/demo/floatlabel'] },
                    { label: 'Invalid State', icon: 'pi pi-fw pi-exclamation-circle', routerLink: ['/demo/invalidstate'] },
                    { label: 'Button', icon: 'pi pi-fw pi-box', routerLink: ['/demo/button'] },
                    { label: 'Table', icon: 'pi pi-fw pi-table', routerLink: ['/demo/table'] },
                    { label: 'Panel', icon: 'pi pi-fw pi-tablet', routerLink: ['/demo/panel'] },
                    { label: 'Overlay', icon: 'pi pi-fw pi-clone', routerLink: ['/demo/overlay'] },
                    { label: 'Message', icon: 'pi pi-fw pi-comment', routerLink: ['/demo/message'] },
                    { label: 'Attachment', icon: 'pi pi-fw pi-upload', routerLink: ['/demo/attachment'] },
                    { label: 'Chart', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/demo/charts'] },
                    { label: 'Icons', icon: 'pi pi-fw pi-prime', routerLink: ['/demo/icons'] },
                    { label: 'PrimeFlex', icon: 'pi pi-fw pi-desktop', url: ['https://primeflex.org/'], target: '_blank' },
                    { label: 'PrimeNG', icon: 'pi pi-fw pi-prime', url: ['https://primeng.org/'], target: '_blank' },
                    {
                        label: "Pages",
                        icon: "pi pi-file",
                        items: [
                            { label: 'Access Denied', icon: 'pi pi-fw pi-lock', routerLink: ["/access-denied"] },
                            { label: 'Not Found', icon: 'pi pi-fw pi-exclamation-circle', routerLink: ["/notfound"] }
                        ]
                    }
                ]
            }
        ]

        return this.http.get("menu").pipe(map((res: any) => ([{
            items: !environment.production ? [...demo, ...(res ?? [])] : res
        }])))
    }
}
