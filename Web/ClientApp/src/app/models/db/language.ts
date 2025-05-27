import { EntityBase } from "../entityBase";
import { LanguageLang } from "./languageLang";

export class Language extends EntityBase {
    languageCode: string;
    description: string;
    pattern: string;
    active: boolean;
    languageLangs: LanguageLang[] = []
}