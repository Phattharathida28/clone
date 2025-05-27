import { Injectable, Inject } from "@angular/core";

export interface IServerConfiguration {
    IdentityUrl: string;
    ReportUrl: string;
    languages: Array<{ value: string, text: string }>;
}


@Injectable({ providedIn: 'root' })
export class ConfigurationService {
    private configuration!: IServerConfiguration;

    constructor(@Inject('BASE_URL') private baseUrl: string) { }

    loadConfig = () => fetch(`${this.baseUrl}/api/configuration`)
    .then(response => response.json())
    .then(json => {
        this.configuration = json.configuration as IServerConfiguration;
        this.configuration.languages = json.languages.map((lang: any) => ({ value: lang.key.toLowerCase(), text: lang.value }));
    })

    getConfig = () => this.configuration;
}