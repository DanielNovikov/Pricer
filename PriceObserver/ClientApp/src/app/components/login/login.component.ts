import { Component, OnInit } from '@angular/core';
import {AuthenticationHttpService} from "../../shared/services/authentication-http.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent implements OnInit {

  invalidToken: boolean = false;
  tokenExpired: boolean = false;

  constructor(
    private authenticationHttpService: AuthenticationHttpService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    let token = this.route.snapshot.paramMap.get('token');

    if (!token) {
      alert('Token is not present');
      return;
    }

    this.authenticationHttpService
      .authenticate(token)
      .subscribe({
        next: async result => {

          localStorage.setItem('access_token', result.accessToken!);

          console.log(localStorage.getItem('access_token'));

          await this.router.navigate(['']);

          console.log(result.accessToken!);
        },
        error: error => {
          if (error.status === 401)
            this.tokenExpired = true;
          else
            this.invalidToken = true;
        }
      });
  }

}
