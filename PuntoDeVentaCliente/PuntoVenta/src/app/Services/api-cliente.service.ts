import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response } from '../Models/Response'
import { Observable } from 'rxjs';
import { Cliente } from '../Models/Cliente';
import { HttpHeaders } from '@angular/common/http';
import { EditClienteModel } from '../Models/EditClienteModel';

@Injectable({
  providedIn: 'root'
})
export class ApiClienteService {
  url: string = 'http://localhost:5000/Cliente';


  constructor(
    private _http: HttpClient
  ) { 
     
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  getClienteById(id: number): Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Cliente/${id}`);
  }

  getCliente(): Observable<Response> {
      return this._http.get<Response>(this.url);
  }

  addCliente(cliente: Cliente) : Observable<Response> {
      return this._http.post<Response>(this.url, cliente, this.httpOptions);
  }

  filterCliente(filters: string): Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Cliente/filtro/${filters}`);
  }

  updateEstatusCliente(id: number): Observable<Response> {
    return this._http.put<Response>(`http://localhost:5000/Cliente/updateEstatus/${id}`,
     '', this.httpOptions);
  }

  updateCliente(cliente: EditClienteModel): Observable<Response> {
    return this._http.put<Response>(this.url, cliente , this.httpOptions);
  }


}
