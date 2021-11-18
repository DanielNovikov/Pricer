import { Component, OnInit } from '@angular/core';
import {ItemHttpService} from "../../shared/services/item-http.service";
import {Shop} from "../../shared/models/shop";

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.less']
})
export class ItemsComponent implements OnInit {

  shops: Shop[] | undefined;

  constructor(private itemHttpService: ItemHttpService) { }

  ngOnInit(): void {
    this.itemHttpService
      .getGrouped()
      .subscribe(data => {
        this.shops = data;
        console.log(data);
      });
  }

  remove(id: number): void {
    this.itemHttpService
      .delete(id)
      .subscribe(response => {
        let shop = this.shops?.find(x => x.items.some(y => y.id === id))!;

        if (shop.items.length === 1)
        {
          this.shops = this.shops!.filter(x => x != shop);
          return;
        }

        shop.items = shop.items.filter(x => x.id !== id);
      });
  }
}
