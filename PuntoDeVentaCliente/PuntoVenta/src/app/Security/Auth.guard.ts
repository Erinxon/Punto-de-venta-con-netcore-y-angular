import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ApiAuthService } from '../Services/apiAuth-service';

@Injectable( {providedIn: 'root'})

export class AuthGuard implements CanActivate {
    constructor(private router: Router,
        private apiAuthService: ApiAuthService){

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){
        const usuario = this.apiAuthService.usuarioData;
        if(usuario){
            return true;
        }else{
            this.router.navigate(['/login']);
            return false;     
        }
       
    }
    
}