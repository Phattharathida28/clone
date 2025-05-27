import { Pipe, PipeTransform } from '@angular/core';
import { I18nService } from '@app/core/services/i18n.service';

@Pipe({
  name: 'dateToLocalDateString'
})
export class DateToLocalDateStringPipe implements PipeTransform {

  constructor(private i18n: I18nService) { }

  transform(value: Date): string {
    return value.toLocaleDateString(this.i18n.language);
  }

}
