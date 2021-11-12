import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ShopWithItems} from "../models/shop-with-items";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ItemHttpService {

  readonly groupedEndpoint: string = "api/Item/grouped/";
  readonly deleteEndpoint: string = "api/Item/";

  constructor(private http: HttpClient) { }

  public getGrouped(userId: number) : Observable<ShopWithItems[]> {
    return this.http.get<ShopWithItems[]>(environment.pricerUrl + this.groupedEndpoint + userId);
  }

  public delete(id: number) : Observable<object> {
    return this.http.delete(environment.pricerUrl + this.deleteEndpoint + id);
  }
}
