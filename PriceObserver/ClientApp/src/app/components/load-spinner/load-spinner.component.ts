import {Component, Input, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";

@Component({
  selector: 'app-load-spinner',
  templateUrl: './load-spinner.component.html',
  styleUrls: ['./load-spinner.component.less']
})
export class LoadSpinnerComponent implements OnInit {

  showSpinner: boolean = true;

  private loadedSubscription: Subscription;
  @Input() loaded: Observable<void>;

  constructor() { }

  ngOnInit(): void {
    this.loadedSubscription = this.loaded.subscribe(() => {
      this.showSpinner = false;
    });
  }



}
