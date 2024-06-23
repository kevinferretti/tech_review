import { Routes } from '@angular/router';

import { FullComponent } from './layouts/full/full.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: '',
        redirectTo: '/astronauts',
        pathMatch: 'full'
      },
      {
        path: '',
        loadChildren:
          () => import('./main/material.module').then(m => m.MaterialComponentsModule)
      }
    ]
  }
];
