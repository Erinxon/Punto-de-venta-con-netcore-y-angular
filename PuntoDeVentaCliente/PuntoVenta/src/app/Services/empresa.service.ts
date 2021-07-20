import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../Models/Response';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {
  url: string = 'http://localhost:5000/Empresa';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };
  
  constructor(private _http: HttpClient) { }

  GetEmpresa(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  AddEmpresa(empresa: any): Observable<Response> {
    return this._http.post<Response>(this.url, empresa, this.httpOptions);
  }

  updateEmpresa(empresa: any): Observable<Response> {
    return this._http.put<Response>(this.url, empresa, this.httpOptions);
  }
}
