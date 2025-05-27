import { Injectable } from "@angular/core";
import { NotifyPosition } from "@app/shared/types/data.types";
import { TranslateService } from "@ngx-translate/core";
import { MessageService } from "primeng/api";
import { forkJoin, map, switchMap, zip } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class NotifyService {
    constructor(private ms: MessageService, private ts: TranslateService) { }

    private arrayToObject = (array: string[] = []) =>
        array.reduce((obj: any, item, index) => {
            obj[index] = item;
            return obj
        }, {})

    translatedParams(params: string[] = []) {
        return forkJoin(params.map(item => this.ts.get(item))).pipe(
            map((params) => this.arrayToObject(params))
        )
    }

    translatedMessage(message: string = "message.STD00004", params: string[] | null = null) {
        return params != null && params.length > 0 ? this.translatedParams(params).pipe(
            switchMap(translated => this.ts.get(message, translated))
        ) : this.ts.get(message);
    }

    info(message: string = "message.STD00004", params: string[] | null = null, position: NotifyPosition = "tr") {
        zip(this.translatedMessage(message, params), this.translatedMessage("message.STD00001"))
            .pipe(map((res) => ({ message: res[0], title: res[1] })))
            .subscribe(({ message, title }) => {
                this.ms.clear()
                this.ms.add({
                    severity: "info",
                    summary: title,
                    detail: message,
                    key: position
                })
            });
    }

    success(message: string = "message.STD00004", params: string[] | null = null, position: NotifyPosition = "tr") {
        zip(this.translatedMessage(message, params), this.translatedMessage("message.STD00000"))
            .pipe(map((res) => ({ message: res[0], title: res[1] })))
            .subscribe(({ message, title }) => {
                this.ms.clear()
                this.ms.add({
                    severity: "success",
                    summary: title,
                    detail: message,
                    key: position
                })
            });
    }

    error(message: string = "message.STD00004", params: string[] | null = null, position: NotifyPosition = "tr") {
        zip(this.translatedMessage(message, params), this.translatedMessage("message.STD00003"))
            .pipe(map((res) => ({ message: res[0], title: res[1] })))
            .subscribe(({ message, title }) => {
                this.ms.clear()
                this.ms.add({
                    severity: "error",
                    summary: title,
                    detail: message,
                    key: position,
                })
            });
    }

    warning(message: string = "message.STD00004", params: string[] | null = null, position: NotifyPosition = "tr") {
        zip(this.translatedMessage(message, params), this.translatedMessage("message.STD00002"))
            .pipe(map((res) => ({ message: res[0], title: res[1] })))
            .subscribe(({ message, title }) => {
                this.ms.clear()
                this.ms.add({
                    severity: "warn",
                    summary: title,
                    detail: message,
                    key: position,
                })
            });
    }
}