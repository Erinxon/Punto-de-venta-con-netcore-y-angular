import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../Models/Response';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  url: string = 'http://localhost:5000/Usuario';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  constructor(private _http: HttpClient) { }

  getAllUsuario(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  getUsuarioById(id: number): Observable<Response> {
    return this._http.get<Response>(`${this.url}/${id}`);
  }

  addUsuario(usuario: any): Observable<Response> {
    return this._http.post<Response>(this.url, usuario, this.httpOptions);
  }

  updateUsuario(usuario: any): Observable<Response> {
    return this._http.put<Response>(this.url, usuario, this.httpOptions);
  }

  updateStatusUsuario(id: number): Observable<Response> {
    return this._http.put<Response>(`${this.url}/UpdateEstatusUsuario/${id}`, 
    '', this.httpOptions);
  }
  
  filterUsuario(search: string): Observable<Response> {
    return this._http.get<Response>(`${this.url}/FilterUsuario/${search}`);
  }


}
