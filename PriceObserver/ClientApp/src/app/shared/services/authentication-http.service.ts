import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {AuthenticationResult} from "../models/authentication-result";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationHttpService {
  readonly authenticationEndpoint: string = environment.baseUrl + "api/authorize/";

  constructor(private http: HttpClient) { }

  public authenticate(token: string) : Observable<AuthenticationResult> {
    return this.http.post<AuthenticationResult>(this.authenticationEndpoint + token, null);
  }
}
