import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Shop} from "../models/shop";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ItemHttpService {

  readonly getEndpoint: string = environment.baseUrl + "api/Item/";
  readonly deleteEndpoint: string = environment.baseUrl + "api/Item/";

  constructor(private http: HttpClient) { }

  public get() : Observable<Shop[]> {
    return this.http.get<Shop[]>(this.getEndpoint);
  }

  public delete(id: number) : Observable<object> {
    return this.http.delete(this.deleteEndpoint + id);
  }
}
