import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FestivalsComponent } from './festivals/festivals.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'festivals',
    pathMatch: 'full',
  },
  {
    path: 'festivals',
    component: FestivalsComponent,
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
