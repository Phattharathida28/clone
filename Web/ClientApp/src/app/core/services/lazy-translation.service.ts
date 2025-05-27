import { Injectable, Injector } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { mergeDeep } from './translate-http-loader';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})

export class LazyTranslationService {

    private lazyLoadedSubject = new BehaviorSubject<string[]>([]);
    lazyLoaded = this.lazyLoadedSubject.asObservable();
    private modules: string[] = [];
    private loadedTranslate: { [key: string]: {} } = {};
    private loaded = false;

    get translationLoaded() {
        return this.loaded;
    }

    set translationLoaded(value: boolean) {
        this.loaded = value;
    }

    constructor(private injector: Injector) { }

    public add(module: string) {
        if (this.modules.includes(module)) return;

        this.modules.push(module);

        if (this.loaded) this.load(module);
    }

    private load(module: string) {
        const translate = this.injector.get(TranslateService);
        const langs = translate.langs?.length == 0 ? [translate.currentLang] : translate.langs;
        langs.forEach(lang => {
            this.retrieve(module, lang).pipe(
                finalize(() => this.lazyLoadedSubject.next(this.modules))
            ).subscribe(result => {
                this.loadedTranslate[lang] = mergeDeep(this.loadedTranslate[lang] ?? {}, result);
                translate.setTranslation(lang, result, true);
            })
        })
    }

    loadByLang = (lang: string) => this.modules.map(module => this.retrieve(module, lang));

    getLoadedTranslate = (lang: string) => this.loadedTranslate[lang] ?? {};

    private retrieve = (module: string, lang: string): Observable<any> => this.injector.get(HttpClient).get(`localize/${module}/${lang}`).pipe(catchError(res => of({})))
}