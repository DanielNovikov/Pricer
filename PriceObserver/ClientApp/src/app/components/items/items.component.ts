import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ItemHttpService} from "../../shared/services/item-http.service";
import {Items} from "../../shared/models/items";

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.less']
})
export class ItemsComponent implements OnInit {

  modalImageVisible: boolean;
  modalImageSrc: string;

  @Input() itemsLists: Items[] | [];
  @Output() itemsListsChange = new EventEmitter<Items[]>();

  constructor(private itemHttpService: ItemHttpService) { }

  ngOnInit(): void {
  }

  remove(id: number): void {
    this.itemHttpService
      .delete(id)
      .subscribe(response => {
        let itemsList = this.itemsLists?.find(x => x.items.some(y => y.id === id))!;

        if (itemsList.items.length === 1) {

          this.itemsLists = this.itemsLists!.filter(x => x != itemsList) || [];
          this.itemsListsChange.emit(this.itemsLists);
          return;
        }

        itemsList.items = itemsList.items.filter(x => x.id !== id);
      });
  }

  showModalImage(src: string): void {
    this.modalImageVisible = true;
    this.modalImageSrc = src;
  }

  closeModalImage(): void {
    this.modalImageVisible = false;
  }
}
