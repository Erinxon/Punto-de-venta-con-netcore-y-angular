import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ApiAuthService } from 'src/app/Services/apiAuth-service';
import { DOCUMENT } from '@angular/common';
import { Inject } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  pathname: string | undefined = '';
  @Input() menuActive: boolean = true;
  subMenu: boolean = false;
  constructor(private router: Router, @Inject(DOCUMENT) private _document: Document){
    this.pathname = _document.defaultView?.location.pathname;
  }

  ngOnInit(): void {
   
  }

  getCurrentUrl(url: string): boolean | undefined {
    return this._document.defaultView?.location.pathname.match(url) ? true : false;
  }

  desplegarSubMenu(): void{
    this.subMenu =  !this.subMenu;
  }

  isRutaInicio(): boolean {
    let isInicio = !this.getCurrentUrl('/productos') 
                && !this.getCurrentUrl('/categorias') && !this.getCurrentUrl('/cliente')
                && !this.getCurrentUrl('/proveedores') && !this.getCurrentUrl('/compras')
                && !this.getCurrentUrl('/ventas') && !this.getCurrentUrl('/usuario')
                && !this.getCurrentUrl('/perfil') && !this.getCurrentUrl('empresa')
                ? true : false;
    return isInicio;
  }



 

}
