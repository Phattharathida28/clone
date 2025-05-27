import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'stringToJson'
})
export class StringToJsonPipe implements PipeTransform {

  transform(value: unknown): object | unknown {
    if (typeof value == "string") return JSON.parse(value)

    return value;
  }

}
