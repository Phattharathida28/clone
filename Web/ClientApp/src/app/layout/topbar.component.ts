import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Config, LayoutService } from "./service/layout.service";
import { I18nService } from '@app/core/services/i18n.service';
import { LoadingService } from '@app/pages/loading/loading.service';
import { AuthService } from '@app/core/authentication/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'x-topbar',
    templateUrl: './topbar.component.html'
})
export class TopBarComponent {

    items!: MenuItem[];
    languages: { code: string, icon: string, label: string }[] = [
        { icon: '../../assets/layout/images/th.png', label: 'ไทย', code: 'en' },
        { icon: '../../assets/layout/images/en.png', label: 'EN', code: 'th' }
    ]
    scroll: number = 0

    @ViewChild('menubutton') menuButton!: ElementRef;
    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;
    @ViewChild('topbarmenu') menu!: ElementRef;

    constructor(
        public layoutService: LayoutService,
        public i18n: I18nService,
        private ls: LoadingService,
        private auth: AuthService,
        private router: Router) { }

    changLanguage(lang: string) {
        this.router.navigate(['/empty/lang', lang]);
    }

    changeTheme(mode: string) {
        this.layoutService.config.update((config: Config) => ({
            ...config,
            colorScheme: mode
        }))
    }

    async logout() {
        this.ls.show()
        await this.auth.logout()
        this.ls.hide()
    }

    @HostListener('window:scroll', [])
    onWindowScroll() {
        this.scroll = window.scrollY;
    }
}
