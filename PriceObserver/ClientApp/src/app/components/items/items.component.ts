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

}
