import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Producto } from '../Models/Producto';
import { Response } from '../Models/Response';

@Injectable({
  providedIn: 'root'
})

export class ProductoService {
  url: string = 'http://localhost:5000/Producto';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };
  
  constructor(private _http: HttpClient) {

  }
  

  getAllProductos(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  UpdateEstatusProducto(id: number): Observable<Response> {
    return this._http.put<Response>(`http://localhost:5000/Producto/UpdateEstatusProducto/${id}`,
    '', this.httpOptions);
  }

  getFilterProductos(search: string): Observable<Response> {
     return this._http.get<Response>(`http://localhost:5000/Producto/filter/${search}`);
  }

  addProducto(producto: Producto): Observable<Response> {
    return this._http.post<Response>(this.url, producto, this.httpOptions);
  }

  getProductoById(id: number): Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Producto/${id}`);
  }

  updateProducto(producto: any): Observable<Response> {
    return this._http.put<Response>(`http://localhost:5000/Producto`,
    producto, this.httpOptions);
  }

}
