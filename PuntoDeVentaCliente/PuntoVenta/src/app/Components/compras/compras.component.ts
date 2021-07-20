import { Component, OnInit } from '@angular/core';
import { ComprasService } from 'src/app/Services/compras.service';
import {NgxPaginationModule} from 'ngx-pagination'; 
import { Router } from '@angular/router';


@Component({
  selector: 'app-compras',
  templateUrl: './compras.component.html',
  styleUrls: ['./compras.component.css']
})
export class ComprasComponent implements OnInit {
  loanding: boolean = true;
  public compras: any[] = [];
  filters: string = '';

  p: number = 1;
  constructor(
    private comprasServices: ComprasService,
    private router: Router
  ) { 
    
  }

  ngOnInit(): void {
    this.getCompras();
  }

  getCompras(): void{
    this.comprasServices.getAllCompra().subscribe( r => {
      this.compras = r.data;
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
    this.getCompras();
  }

  getTotal(id:number): number {
    let compra = this.compras.find(x=> x.id == id);
    let total = 0;
    compra.detalleCompra.forEach((x:{costo:number, cantidad: number})=>{
       total += x.costo * x.cantidad;
    })
    return total;
 } 

 comprobante(id: number): void {
   this.router.navigate([`/compras/comprobante/${id}`])
 }

}
