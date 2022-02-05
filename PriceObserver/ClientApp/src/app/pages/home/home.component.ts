import {Component, Input, OnInit} from '@angular/core';
import {ItemHttpService} from "../../shared/services/item-http.service";
import {Items} from "../../shared/models/items";
import {Router} from "@angular/router";
import {Subject} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {

  dataLoaded: Subject<void> = new Subject<void>();

  showData: boolean = false;
  itemsLists: Items[] | [];

  constructor(
    private itemHttpService: ItemHttpService,
    private router: Router) { }

  ngOnInit(): void {
    if (!localStorage.hasOwnProperty('access_token'))
    {
      this.router.navigate(['login']);
      return;
    }

    this.itemHttpService
      .get()
      .subscribe(data => {
        this.dataLoaded.next();

        setTimeout(() => {
          this.itemsLists = data;
          this.showData = true;
        });
      });
  }

  logout(): void {
    localStorage.removeItem('access_token');

    window.location.href = 'https://t.me/pricer_official_bot';
  }
}
