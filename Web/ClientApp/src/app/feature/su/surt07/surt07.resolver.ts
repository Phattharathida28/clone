import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { ContentDTO, Surt07Service } from './surt07.service';
import { ContentUploadService } from '@app/shared/services/content-upload.service';
import { map, zip } from 'rxjs';

export const contents: ResolveFn<ContentDTO[]> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const sv = inject(Surt07Service)
  const cs = inject(ContentUploadService)

  return zip([cs.getContentUrl(), sv.list()]).pipe(
    map((res) => ({ contentUrl: res[0], content: res[1] })),
    map(({ content, contentUrl }) => {
      content.map(m => m.url = `${contentUrl.DisplayUrl}/${m.container}/${m.path}`)

      return content
    })
  )
};