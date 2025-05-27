import { EntityBase } from "../entityBase";

export class LanguageLang extends EntityBase {
    languageCode: string;
    languageCodeForname: string;
    languageName: string;

    constructor(languageCode?: string, languageCodeForname?: string) {
        super()
        if (languageCode) this.languageCode = languageCode
        if (languageCodeForname) this.languageCodeForname = languageCodeForname
    }
}