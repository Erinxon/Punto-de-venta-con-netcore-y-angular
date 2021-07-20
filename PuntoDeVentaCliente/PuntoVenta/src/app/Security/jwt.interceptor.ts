import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { throwError } from "rxjs";
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";
import { ApiAuthService } from "../Services/apiAuth-service";

@Injectable({providedIn: 'root'})
export class JwtInterceptor implements HttpInterceptor {

    constructor(private apiAuthService: ApiAuthService, private router: Router){

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const usuario = this.apiAuthService.usuarioData;
        let request = req;
        if(usuario){
            request = request.clone({
                setHeaders: {
                    authorization: `Bearer ${ usuario.token }`
                }
            });
        }
        return next.handle(request).pipe(
            catchError((response: HttpErrorResponse) => {
              if (response.status === 401) {
                this.apiAuthService.logout();
                this.router.navigate(['/login'])
              }
              return throwError(response);
            }
        ));
    }   
}