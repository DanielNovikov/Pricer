import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemsComponent } from "./components/items/items.component";
import { LoginComponent } from "./components/login/login.component";

const routes: Routes = [
  {path: '', component: ItemsComponent, pathMatch: 'full' },
  {path: 'login/:token', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
