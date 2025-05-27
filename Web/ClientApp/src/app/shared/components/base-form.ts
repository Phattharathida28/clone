import { ControlValueAccessor, NgControl, FormControl, AbstractControl } from '@angular/forms'
import { DoCheck, EventEmitter, Input, Output, Optional, Self, Directive, AfterViewInit } from '@angular/core'
import { Guid } from 'guid-typescript'

@Directive()
export abstract class BaseFormField implements ControlValueAccessor, AfterViewInit, DoCheck {
    @Input() value: any = null;
    @Input() disabled: boolean = false;
    @Input() placeholder: string = '';
    @Input() hasLabel: boolean = true;
    @Input() small: boolean = true;
    @Input() class: string = '';
    @Input() readonly: boolean = false
    @Output() change: EventEmitter<any> = new EventEmitter<any>()

    id = Guid.raw();
    required = false
    private _enabled: boolean;
    onChange = (value: any) => { }
    onTouched = () => { }

    constructor(@Optional() @Self() public controlDir: NgControl) {
        if (controlDir) controlDir.valueAccessor = this
    }

    ngAfterViewInit(): void {
        if (this.control) this._enabled = this.control.enabled;
    }

    get control() {
        return this.controlDir && this.controlDir.control instanceof FormControl ? this.controlDir.control : null
    }

    ngDoCheck() {
        if (this.control) {
            const validator = this.control.validator && this.control.validator(new FormControl(''))
            this.required = Boolean(validator && validator.hasOwnProperty('required')) || Boolean(validator && validator.hasOwnProperty('selectedCount'))

            if (this.control.enabled != this._enabled) {

                if (this.control.enabled && !this._enabled) this.control.enable();
                else if (!this.control.enabled && this._enabled) this.control.disable();

                this._enabled = this.control.enabled;
            }
        }
    }

    get hasErrors() {
        return (this.control && (this.control.touched || this.control.dirty) && this.control.errors)
    }

    writeValue(value: any): void { }

    registerOnChange(fn: any): void {
        this.onChange = fn
    }

    registerOnTouched(fn: () => void): void {
        this.onTouched = fn
    }

    setDisabledState(disabled: boolean): void {
        this.disabled = this.disabled ? this.disabled : disabled
    }

    removeErrors(keys: string[], control: AbstractControl) {
        if (!control || !keys || keys.length === 0) return;

        const remainingErrors = keys.reduce((errors, key) => {
            delete errors[key];
            return errors;
        }, { ...control.errors });

        control.setErrors(remainingErrors);

        if (Object.keys(control.errors || {}).length === 0) control.setErrors(null);
    }


    addErrors(errors: { [key: string]: any }, control: AbstractControl) {
        if (!control || !errors) return;

        control.setErrors({ ...control.errors, ...errors });
    }
}