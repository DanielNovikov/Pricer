import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ShopWithItems} from "../models/shop-with-items";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ItemHttpService {

  readonly groupedEndpoint: string = environment.baseUrl + "api/Item/grouped";
  readonly deleteEndpoint: string = environment.baseUrl + "api/Item/";

  constructor(private http: HttpClient) { }

  public getGrouped() : Observable<ShopWithItems[]> {
    return this.http.get<ShopWithItems[]>(this.groupedEndpoint);
  }

  public delete(id: number) : Observable<object> {
    return this.http.delete(this.deleteEndpoint + id);
  }
}
