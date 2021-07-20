import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../Models/Response'

@Injectable({
  providedIn: 'root'
})
export class ComprasService {
  url: string = 'http://localhost:5000/Compra';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  constructor(private _http: HttpClient) {

  }

  getAllCompra(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  addCompra(compra: any): Observable<Response> {
    return this._http.post<Response>(this.url, compra, this.httpOptions);
  }

  getCompraById(id: number): Observable<Response> {
    return this._http.get<Response>(`${this.url}/${id}`);
  }
}
