import { DOCUMENT } from '@angular/common';
import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ApiAuthService } from './Services/apiAuth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  isLogin: boolean = false;
  menuActive: boolean = true;
  constructor(private apiAuthService: ApiAuthService, private router: Router,
    ) {
    this.isLogin = JSON.parse(localStorage.getItem('usuario')!) ? true : false;
    
    
  }
  ngOnInit(): void {
    this.apiAuthService.token$.subscribe( token => {
      this.isLogin = token;
   });
   
  }

  onMenuActive(even: boolean){
    this.menuActive = even;
  }

  
}
