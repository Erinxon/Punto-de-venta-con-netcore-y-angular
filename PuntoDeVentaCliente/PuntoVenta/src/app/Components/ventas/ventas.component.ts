import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { ComprasService } from 'src/app/Services/compras.service';
import { VentasService } from 'src/app/Services/ventas.service';
import { MatDatepicker, MatDatepickerToggle,
} from '@angular/material/datepicker';

import { MatLabel, MatFormField } from '@angular/material/form-field';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ventas',
  templateUrl: './ventas.component.html',
  styleUrls: ['./ventas.component.css']
})
export class VentasComponent implements OnInit {
  loanding: boolean = true;
  public ventas: any[] = [];
  filters: string = '';

  p: number = 1;
  constructor(
    @Inject(DOCUMENT) private _document: Document,
    private ventasServices: VentasService,
    private router: Router
  ) { 
    this._document.title = "Ventas";
  }

  ngOnInit(): void {
    this.getVentas();
    
  }

  getVentas(): void{
    this.ventasServices.getAllVentas().subscribe( v => {
      this.ventas = v.data;
      this.loanding = false;
    });
  }

  filtrar(): void {
    if(this.filters){
       console.log(this.filters);
    }
   
  }

  clearFilter(): void {
    this.filters = '';
    this.getVentas(); 
  }

  getTotal(id:number): number {
     let venta = this.ventas.find(x=> x.id == id);
     let total = 0;
     venta.detalleVentas.forEach((x:{precio:number, cantidad: number})=>{
        total += x.precio * x.cantidad;
     })
     return total;
  } 

  factura(id: number): void {
    this.router.navigate([`ventas/factura/${id}`])
  }

}
