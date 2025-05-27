import { Component, OnDestroy, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { Router, ActivatedRoute } from '@angular/router';
import { I18nService, DefaultLanguages } from '@app/core/services/i18n.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'x-empty',
    template: ``
})
export class EmptyComponent implements OnInit, OnDestroy {

    private langChangedSubscription!: Subscription;

    constructor(
        private router: Router,
        private location: Location,
        private route: ActivatedRoute,
        public i18n: I18nService) { }

    ngOnInit() {
        if (this.router.url.includes('/lang')) {
            this.langChangedSubscription = this.i18n.onLangChanged.subscribe(() => this.location.back());
            const lang = this.route.snapshot.paramMap.get('code');
            this.i18n.language = lang ?? DefaultLanguages.Eng;
        }
        else this.location.back();
    }

    ngOnDestroy() {
        if (this.langChangedSubscription) {
            this.langChangedSubscription.unsubscribe();
        }
    }
}