import { Component, OnInit } from '@angular/core';
import {ItemHttpService} from "../../shared/item-http.service";
import {ShopWithItems} from "../../models/shop-with-items";

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.less']
})
export class ItemsComponent implements OnInit {

  shops: ShopWithItems[] | undefined;

  constructor(private itemHttpService: ItemHttpService) { }

  ngOnInit(): void {
    this.itemHttpService
      .getGrouped(382190306)
      .subscribe(data => {
        this.shops = data;
        console.log(data);
      });
  }

  remove(id: number): void {
    this.itemHttpService
      .delete(id)
      .subscribe(response => {
        let shop = this.shops?.find(x => x.items.some(y => y.id === id));

        if (!shop)
          return;

        shop.items = shop.items.filter(x => x.id !== id);
      });
  }
}
