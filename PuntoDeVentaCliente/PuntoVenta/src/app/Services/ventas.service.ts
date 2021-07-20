import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../Models/Response';


@Injectable({
  providedIn: 'root'
})
export class VentasService {
  url: string = 'http://localhost:5000/Venta';
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  constructor(private _http: HttpClient) {

  }

  addVenta(venta: any): Observable<Response> {
    return this._http.post<Response>(this.url, venta, this.httpOptions);
  }

  getAllVentas(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  getVentaById(id: number): Observable<Response>  {
    return this._http.get<Response>(`${this.url}/${id}`);
  }
}
