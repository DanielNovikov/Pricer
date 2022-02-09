import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Items} from "../models/items";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ItemHttpService {

  readonly getEndpoint: string = environment.baseUrl + "api/item/";
  readonly deleteEndpoint: string = environment.baseUrl + "api/item/";

  constructor(private http: HttpClient) { }

  public get() : Observable<Items[]> {
    return this.http.get<Items[]>(this.getEndpoint);
  }

  public delete(id: number) : Observable<object> {
    return this.http.delete(this.deleteEndpoint + id);
  }
}
