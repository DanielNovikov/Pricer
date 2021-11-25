import {Component, Input, OnInit} from '@angular/core';
import {ItemHttpService} from "../../shared/services/item-http.service";
import {Shop} from "../../shared/models/shop";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {

  shops: Shop[] | [];

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
      .getGrouped()
      .subscribe(data => {
        this.shops = data;
      });
  }

}