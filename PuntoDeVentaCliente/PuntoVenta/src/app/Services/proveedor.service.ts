import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../Models/Response';

@Injectable({
  providedIn: 'root'
})
export class ProveedorService {
  url: string = 'http://localhost:5000/Proveedor';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  constructor(
    private _http: HttpClient
  ) { }

  getAllProveedores(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  updateEstatusCliente(id:number): Observable<Response> {
    return this._http.put<Response>(`http://localhost:5000/Proveedor/UpdateEstatus/${id}`,
    '',this.httpOptions)
  }

  getFilterProveedores(search: string) : Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Proveedor/filterProveedor/${search}`);
  }

  addProvedor(proveedor: any): Observable<Response> {
    return this._http.post<Response>(this.url, proveedor, this.httpOptions);
  }

  updateProveedor(proveedor: any): Observable<Response> {
    return this._http.put<Response>(this.url, proveedor, this.httpOptions);
  }

  getProveedorById(id: number): Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Proveedor/${id}`);
  }
}
