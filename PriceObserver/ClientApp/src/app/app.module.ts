import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ItemsComponent } from './components/items/items.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { LoginComponent } from './pages/login/login.component';
import { AuthInterceptor } from "./shared/services/interceptors/auth.interceptor";
import { HomeComponent } from './pages/home/home.component';
import { LoadSpinnerComponent } from './components/load-spinner/load-spinner.component';
import { NoItemsComponent } from './components/no-items/no-items.component';

@NgModule({
  declarations: [
    AppComponent,
    ItemsComponent,
    LoginComponent,
    HomeComponent,
    LoadSpinnerComponent,
    NoItemsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
