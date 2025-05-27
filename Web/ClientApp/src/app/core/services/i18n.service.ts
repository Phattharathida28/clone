import { Injectable } from '@angular/core';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { ConfigurationService } from './configuration.service';

const languageKey = 'language';

export enum DefaultLanguages {
    Eng = 'en',
    Thai = 'th'
}

@Injectable({
    providedIn: 'root'
})
export class I18nService {

    defaultLanguage: string = DefaultLanguages.Eng;

    private langChangeSubscription!: Subscription;

    onLangChanged = this.translateService.onLangChange;
    
    constructor(private translateService: TranslateService, private config: ConfigurationService) {

    }

    init(defaultLanguage?: string) {
        this.langChangeSubscription = this.translateService.onLangChange.subscribe((event: LangChangeEvent) => localStorage.setItem(languageKey, event.lang));
        this.language = defaultLanguage || localStorage.getItem(languageKey) || this.defaultLanguage;
    }

    destroy() {
        if (this.langChangeSubscription) this.langChangeSubscription.unsubscribe();
    }


    set language(language: string) {
        language = language ?? localStorage.getItem(languageKey) ?? this.translateService.getBrowserCultureLang();
        let isSupportedLanguage = this.config.getConfig().languages.map(l => l.value).includes(language);

        if (language && !isSupportedLanguage) {
            language = language.split('-')[0];
            language = this.config.getConfig().languages.map(l => l.value).find(supportedLanguage => supportedLanguage.startsWith(language)) || '';
            isSupportedLanguage = Boolean(language);
        }

        if (!isSupportedLanguage) {
            language = this.defaultLanguage;
        }

        this.translateService.use(language);
    }

    get language(): string {
        return this.translateService.currentLang;
    }

}