import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideHttpClient(),
    provideToastr({
      timeOut: 2000,
      preventDuplicates: true,
      progressBar: true,
      enableHtml: true,
      progressAnimation: 'decreasing',
      toastClass: 'custom-toast'
    }),
    provideAnimations()
  ]
};