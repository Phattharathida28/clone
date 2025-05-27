import { HttpClient } from '@angular/common/http';
import { Injectable, effect, signal } from '@angular/core';
import { Observable, Subject } from 'rxjs';

export interface Config {
    inputStyle: string;
    colorScheme: string;
    color: string;
    ripple: boolean;
    menuMode: string;
    scale: number;
}

interface LayoutState {
    staticMenuDesktopInactive: boolean;
    overlayMenuActive: boolean;
    profileSidebarVisible: boolean;
    configSidebarVisible: boolean;
    staticMenuMobileActive: boolean;
    menuHoverActive: boolean;
}

@Injectable({
    providedIn: 'root',
})
export class LayoutService {
    _config: Config = JSON.parse(localStorage.getItem("setting")) as unknown as Config

    config = signal<Config>(this._config);

    state: LayoutState = {
        staticMenuDesktopInactive: false,
        overlayMenuActive: false,
        profileSidebarVisible: false,
        configSidebarVisible: false,
        staticMenuMobileActive: false,
        menuHoverActive: false,
    };

    private configUpdate = new Subject<Config>();

    private overlayOpen = new Subject<any>();

    configUpdate$ = this.configUpdate.asObservable();

    overlayOpen$ = this.overlayOpen.asObservable();

    constructor(private http: HttpClient) {
        effect(() => {
            const config = this.config();
            if (this.updateStyle(config)) {
                this.changeTheme();
            }
            this.changeScale(config.scale);
            this.onConfigUpdate();

            this.updateSetting(config).subscribe((res: boolean) => {
                if (res) {
                    localStorage.setItem("setting", JSON.stringify(config))
                    this.changeVariableCss()
                }
            })
        });
    }

    updateStyle(config: Config) {
        return (config.colorScheme !== this._config.colorScheme);
    }

    onMenuToggle() {
        if (this.isOverlay()) {
            this.state.overlayMenuActive = !this.state.overlayMenuActive;
            if (this.state.overlayMenuActive) {
                this.overlayOpen.next(null);
            }
        }

        if (this.isDesktop()) {
            this.state.staticMenuDesktopInactive =
                !this.state.staticMenuDesktopInactive;
        } else {
            this.state.staticMenuMobileActive =
                !this.state.staticMenuMobileActive;

            if (this.state.staticMenuMobileActive) {
                this.overlayOpen.next(null);
            }
        }
    }

    showProfileSidebar() {
        this.state.profileSidebarVisible = !this.state.profileSidebarVisible;
        if (this.state.profileSidebarVisible) {
            this.overlayOpen.next(null);
        }
    }

    showConfigSidebar = () => this.state.configSidebarVisible = true

    isOverlay = () => this.config().menuMode === 'overlay'

    isDesktop = () => window.innerWidth > 991

    isMobile = () => !this.isDesktop()

    onConfigUpdate() {
        this._config = { ...this.config() };
        this.configUpdate.next(this.config());
    }

    changeTheme = () => this.replaceThemeLink(`assets/layout/styles/theme/${this.config().colorScheme}/theme.css`);

    replaceThemeLink(href: string) {
        const id = 'theme-css';
        let themeLink = <HTMLLinkElement>document.getElementById(id);
        const cloneLinkElement = <HTMLLinkElement>themeLink.cloneNode(true);

        cloneLinkElement.setAttribute('href', href);
        cloneLinkElement.setAttribute('id', id + '-clone');

        themeLink.parentNode!.insertBefore(
            cloneLinkElement,
            themeLink.nextSibling
        );
        cloneLinkElement.addEventListener('load', () => {
            themeLink.remove();
            cloneLinkElement.setAttribute('id', id);
        });
    }

    changeScale = (value: number) => document.documentElement.style.fontSize = `${value}px`;

    updateSetting = (config: Config): Observable<boolean> => this.http.disableLoading().post<boolean>(`setting`, config)

    changeVariableCss() {
        const getNewColors = (color) => {
            return {
                primaryColor: color,
                focusRing: `0 0 0 0.2rem rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.2)`,
                highlightBg: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.16)`,
                highlightBg004: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.04)`,
                highlightBg016: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.16)`,
                highlightBg020: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.2)`,
                highlightBg024: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.24)`,
                highlightBg03616: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.3616)`,
                highlightBg07: `rgba(${parseInt(color.slice(1, 3), 16)}, ${parseInt(color.slice(3, 5), 16)}, ${parseInt(color.slice(5), 16)}, 0.7)`,
                primaryHover: this.config().colorScheme == "dark" ? setLightColor(color, 56.8) : setDarkColor(color, 10),
                primaryHover2: setLightColor(color, 32.2),
                primary400: setLightColor(color, 19),
                primaryColorText: isBgDark(color) ? "#FFFFFF" : "#030712",
                highlightTextColor: setDarkColor(color, 15)
            };
        }

        const setLightColor = (color, light) => {
            const originalRGB = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(color);
            const r = parseInt(originalRGB[1], 16);
            const g = parseInt(originalRGB[2], 16);
            const b = parseInt(originalRGB[3], 16);

            const brightnessChange = light;
            const rNew = Math.min(255, r + ((255 - r) * brightnessChange) / 100);
            const gNew = Math.min(255, g + ((255 - g) * brightnessChange) / 100);
            const bNew = Math.min(255, b + ((255 - b) * brightnessChange) / 100);

            return `#${Math.round(rNew).toString(16).padStart(2, '0')}${Math.round(gNew).toString(16).padStart(2, '0')}${Math.round(bNew).toString(16).padStart(2, '0')}`;
        }

        const setDarkColor = (hex, percent) => {
            let r = parseInt(hex.substring(1, 3), 16);
            let g = parseInt(hex.substring(3, 5), 16);
            let b = parseInt(hex.substring(5, 7), 16);

            r = Math.floor(r * (1 - percent / 100));
            g = Math.floor(g * (1 - percent / 100));
            b = Math.floor(b * (1 - percent / 100));

            const result = '#' + ((1 << 24) + (r << 16) + (g << 8) + b).toString(16).slice(1);

            return result;
        }

        const isBgDark = (hex) => {
            hex = hex.replace(/^#/, '');
            if (hex.length === 3) {
                hex = hex
                    .split('')
                    .map(function (s) {
                        return s + s;
                    })
                    .join('');
            }

            const bigint = parseInt(hex, 16);
            const r = (bigint >> 16) & 255;
            const g = (bigint >> 8) & 255;
            const b = bigint & 255;

            return ((r + g + b) / 3) <= 127;
        }

        const color = getNewColors(this.config().color);
        const root = document.documentElement;

        root.style.setProperty('--primary-color', color["primaryColor"]);
        root.style.setProperty('--primary-500', color["primaryColor"])
        root.style.setProperty('--focus-ring', color["focusRing"]);
        root.style.setProperty('--highlight-bg', color["highlightBg"]);
        root.style.setProperty('--highlight-bg-004', color["highlightBg004"]);
        root.style.setProperty('--highlight-bg-020', color["highlightBg020"]);
        root.style.setProperty('--highlight-bg-024', color["highlightBg024"]);
        root.style.setProperty('--highlight-bg-03616', color["highlightBg03616"]);
        root.style.setProperty('--highlight-bg-07', color["highlightBg07"]);
        root.style.setProperty('--primary-hover', color["primaryHover"]);
        root.style.setProperty('--primary-hover-2', color["primaryHover2"]);
        root.style.setProperty('--primary-400', color["primary400"]);
        root.style.setProperty('--primary-color-text', color["primaryColorText"]);
        root.style.setProperty('--highlight-text-color', color["highlightTextColor"]);
        root.style.setProperty('--highlight-bg-016', color["highlightBg016"]);
    }
}
