import { NgModule } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@NgModule()
export class LanguageService {
    private current: string;
    private defaultlanguage = 'en-US';
    public Languages = [{code: 'en-US', name: 'ENGLISH'},
                        {code: 'pt-BR', name: 'PORTUGUESE'}];
    constructor(private translate: TranslateService) {
        this.translate.setDefaultLang(this.defaultlanguage);
        this.current = this.GetUsersLocale();
        this.translate.use(this.current);
    }
    Current(): string {
        if (this.current === undefined) {
            this.current = this.GetUsersLocale();
        }
        return this.current;
    }

    Set(language: string) {
        this.current = language;
        this.translate.use(this.current);
    }

    private GetUsersLocale(): string {
        if (typeof window === 'undefined' || typeof window.navigator === 'undefined') {
          return this.defaultlanguage;
        }
        const wn = window.navigator as any;
        let lang = wn.languages ? wn.languages[0] : this.defaultlanguage;
        lang = lang || wn.language || wn.browserLanguage || wn.userLanguage;
        return lang;
      }
}
