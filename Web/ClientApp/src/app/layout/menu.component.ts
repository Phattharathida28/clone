import { Component } from '@angular/core';
import { LayoutService } from './service/layout.service';
import { MenuItem } from 'primeng/api';
import { MenuService } from './menu.service';
import { AuthService } from '@app/core/authentication/auth.service';
import { User } from 'oidc-client-ts';

@Component({
    selector: 'x-menu',
    templateUrl: './menu.component.html'
})
export class MenuComponent {

    userLogo:string = "";
    userFullName : string = "";
    role:string = "";
    model: MenuItem[] = [];

    constructor(public layoutService: LayoutService, private menuService: MenuService , private auth : AuthService) {
        this.menuService.getMenuList().subscribe((item) => {
            this.model = item
        })
        this.auth.getUser().subscribe((res:User) => {
            if(res){
                this.userLogo = res.profile.name.toLocaleUpperCase()[0];
                this.userFullName = res.profile.given_name;
                this.role = res.profile.preferred_username.toLocaleUpperCase();
            }
        })
    }
}
