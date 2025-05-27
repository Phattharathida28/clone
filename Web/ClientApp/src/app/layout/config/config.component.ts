import { Component, Input } from '@angular/core';
import { Config, LayoutService } from '../service/layout.service';
import { MenuService } from '../menu.service';

export type SelectButtonOption = {
    label: string
    value: string | number
}
@Component({
    selector: 'x-config',
    templateUrl: './config.component.html',
})
export class ConfigComponent {
    @Input() minimal: boolean = false;
    ingredient!: string;
    scales: number[] = [12, 13, 14, 15, 16, 17, 18, 19, 20];
    menuModeOptions: SelectButtonOption[] = [{ label: 'Static', value: 'static' }, { label: 'Overlay', value: 'overlay' }]
    inputOptions: SelectButtonOption[] = [{ label: 'Outlined', value: 'outlined' }, { label: 'Filled', value: 'filled' }]
    colorHex: string

    constructor(
        public layoutService: LayoutService,
        public menuService: MenuService,
    ) {
        this.color = this.layoutService.config().color
    }

    get visible(): boolean {
        return this.layoutService.state.configSidebarVisible;
    }

    set visible(_val: boolean) {
        this.layoutService.state.configSidebarVisible = _val;
    }

    get scale(): number {
        return this.layoutService.config().scale;
    }

    set scale(_val: number) {
        this.layoutService.config.update((config) => ({
            ...config,
            scale: _val,
        }));
    }

    get menuMode(): string {
        return this.layoutService.config().menuMode;
    }

    set menuMode(_val: string) {
        this.layoutService.config.update((config) => ({
            ...config,
            menuMode: _val,
        }));
    }

    get inputStyle(): string {
        return this.layoutService.config().inputStyle;
    }

    set inputStyle(_val: string) {
        this.layoutService.config.update((config: Config) => ({
            ...config,
            inputStyle: _val
        }));
    }

    get ripple(): boolean {
        return this.layoutService.config().ripple;
    }

    set ripple(_val: boolean) {
        this.layoutService.config.update((config) => ({
            ...config,
            ripple: _val,
        }));
    }

    set colorScheme(val: boolean) {
        const color = val ? "dark" : "light"
        this.layoutService.config.update((config) => ({
            ...config,
            colorScheme: color,
            color: this.color
        }));
    }

    get colorScheme(): boolean {
        return this.layoutService.config().colorScheme == "dark" ? true : false;
    }

    set color(color: string) {
        this.colorHex = color
    }

    get color() {
        return this.colorHex
    }

    onConfigButtonClick = () => this.layoutService.showConfigSidebar()

    changeTheme() {
        const regexPattern: RegExp = /^#([A-Fa-f0-9]{6})$/g;
        const isValid: boolean = regexPattern.test(this.color);

        if (!isValid) this.color = this.layoutService.config().color

        this.layoutService.config.update((config: Config) => ({
            ...config,
            color: this.color
        }))
    }

    decrementScale = () => this.scale--

    incrementScale = () => this.scale++
}
