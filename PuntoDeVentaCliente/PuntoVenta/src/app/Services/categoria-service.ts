import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Response } from '../Models/Response'
import { Observable } from 'rxjs';
import { Categoria } from '../Models/Categoria';
import { HttpHeaders } from '@angular/common/http';
import { EditClienteModel } from '../Models/EditClienteModel';
import { UpdateCategoriaModel } from '../Models/UpdateCategoriaModel';

@Injectable({
  providedIn: 'root'
})

export class CategoriaService {
  url: string = 'http://localhost:5000/Categoria';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
  };

  constructor(private _http: HttpClient) { 

  }

  getCategoria(): Observable<Response> {
    return this._http.get<Response>(this.url);
  }

  getFilterCategoria(search: string): Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Categoria/filter/${search}`)
  }

  updateEstatusCategoria(id: number): Observable<Response> {
    return this._http.put<Response>(`http://localhost:5000/Categoria/updateEstatus/${id}`,
    '', this.httpOptions);
  }

  addCategoria(categoria: Categoria): Observable<Response> {
    return this._http.post<Response>(`http://localhost:5000/Categoria`,
                categoria, this.httpOptions);
  }

  updateCategoria(categoria: UpdateCategoriaModel): Observable<Response> {
    return this._http.put<Response>(`http://localhost:5000/Categoria`,
    categoria, this.httpOptions);
  }

  getCategoriaById(id: number): Observable<Response> {
    return this._http.get<Response>(`http://localhost:5000/Categoria/${id}`)
  }


}
