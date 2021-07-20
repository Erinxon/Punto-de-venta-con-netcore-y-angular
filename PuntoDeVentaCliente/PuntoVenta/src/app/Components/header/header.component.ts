import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiAuthService } from 'src/app/Services/apiAuth-service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  menuActive: boolean = true;
  isLogin: boolean = true;
  email: string = '';

  @Output() menu: EventEmitter<boolean> = new EventEmitter<boolean>(true);
  
  constructor(private apiAuthService: ApiAuthService, private router: Router) {
      this.isLogin = JSON.parse(localStorage.getItem('usuario')!) ? true : false;
  }

  logout(): void {  
    this.isLogin = false;
    this.apiAuthService.logout();
    this.router.navigate(['/login'])
  }

  ngOnInit(): void {
    this.email = this.apiAuthService.usuarioData.email;
  }

  desplegarMenu(): void {
    this.menuActive = !this.menuActive;
    this.menu.emit(this.menuActive);
  }

}
