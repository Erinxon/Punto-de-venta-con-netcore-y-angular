import { Component, OnInit } from '@angular/core';
import { ApiClienteService } from 'src/app/Services/api-cliente.service';
import { Response } from '../../Models/Response';
import {NgxPaginationModule} from 'ngx-pagination'; 
import { ProveedorService } from 'src/app/Services/proveedor.service';

@Component({
  selector: 'app-proveedores',
  templateUrl: './proveedores.component.html',
  styleUrls: ['./proveedores.component.css']
})
export class ProveedoresComponent implements OnInit {

  loanding: boolean = true;
  public proveedores: any;
  filters: string = '';

  p: number = 1;
  constructor(
    private proveedorServices: ProveedorService
  ) { 
    
  }

  ngOnInit(): void {
    this.getProveedores();
  }

  getProveedores(): void{
    this.proveedorServices.getAllProveedores().subscribe( r => {
      this.proveedores = r.data;
      this.loanding = false;
    });
  }

  filtrar(): void {
    if(this.filters){
      this.loanding = true;
      this.proveedorServices.getFilterProveedores(this.filters).subscribe(p => {
         this.proveedores = p.data;
         this.loanding = false;
      })         
    }
   
  }

  clearFilter(): void {
    this.filters = '';
    this.getProveedores();
  }

  updateEstatusCliente(id: number): void {
    this.proveedorServices.updateEstatusCliente(id).subscribe(p => {
      if(p.success){
        this.getProveedores();
      }
    })
  }

}
