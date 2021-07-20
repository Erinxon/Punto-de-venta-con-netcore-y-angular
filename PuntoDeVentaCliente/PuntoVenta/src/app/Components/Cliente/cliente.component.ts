import { Component, OnInit } from '@angular/core';
import { ApiClienteService } from 'src/app/Services/api-cliente.service';
import { Response } from '../../Models/Response';
import {NgxPaginationModule} from 'ngx-pagination'; 

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  loanding: boolean = true;
  public clientes: any;
  filters: string = '';

  p: number = 1;
  constructor(
    private apicliente: ApiClienteService
  ) { 
    
  }
  
  ngOnInit(): void {
    this.getClientes();
  }

  getClientes(): void{
    this.apicliente.getCliente().subscribe( r => {
      this.clientes = r.data;
      this.loanding = false;
    });
  }

  filtrar(): void {
    if(this.filters){
      this.loanding = true;
      this.apicliente.filterCliente(this.filters).subscribe( r => {
        this.clientes = r.data;
        this.loanding = false;
      });
    }
   
  }

  clearFilter(): void {
    this.filters = '';
    this.getClientes();
  }

  updateEstatusCliente(id: number): void {
    this.apicliente.updateEstatusCliente(id).subscribe( r => {
      if(r.success){
        this.getClientes();
      }     
    });
  }

}
