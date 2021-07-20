import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import { Response } from '../Models/Response'
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Usuario } from '../Models/Usuario';
import { HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
};

@Injectable({
    providedIn: 'root'
})
export class ApiAuthService  {
    url: string = 'http://localhost:5000/Usuario/login';

    private usuarioSubject: BehaviorSubject<Usuario>;

    token$ = new EventEmitter<boolean>();

    public get usuarioData() : Usuario {
        return this.usuarioSubject.value;
    }

    constructor(private _http: HttpClient,
        ){
        this.usuarioSubject = new BehaviorSubject<Usuario>
        (JSON.parse(localStorage.getItem('usuario')!))
    }

    login(email: string, password: string) : Observable<Response> {
        return this._http.post<Response>(this.url, {email, password} , httpOptions).pipe(
            map(r => {
                if(r.success){
                    const user: Usuario = r.data;
                    localStorage.setItem('usuario', JSON.stringify(user))
                    this.token$.emit(true);     
                    this.usuarioSubject.next(user);                              
                }
                return r;
            })
        );
    }

    logout(): void{       
        localStorage.removeItem('usuario');
        this.token$.emit(false);
        this.usuarioSubject.next(null!);
    }
}