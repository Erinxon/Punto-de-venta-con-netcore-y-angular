import { Component, OnInit } from '@angular/core';
import { ProductoService } from 'src/app/Services/producto.service';
import { Response } from '../../Models/Response';
import {NgxPaginationModule} from 'ngx-pagination'; 

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {
  filters: string = '';
  productos: any;
  p: number = 1;
  loanding: boolean = true;

  constructor(private productoService: ProductoService) { }

  ngOnInit(): void {
      this.getAllProductos();
  }

  getAllProductos(): void {
    this.productoService.getAllProductos().subscribe(p => {
      this.productos = p.data;
      this.loanding = false;
   })
  }

  filtrar(): void {
    if(this.filters){
      this.loanding = true;
      this.productoService.getFilterProductos(this.filters).subscribe(p => {
        this.productos = p.data;
        this.loanding = false;
     })
    }
   
  }

  clearFilter(): void {
    this.filters = '';
    this.getAllProductos();
  }

  updateEstatusCliente(id: number): void {
    this.productoService.UpdateEstatusProducto(id).subscribe(p => {
      if(p.success){
        this.getAllProductos();
      }    
    });
  }

}
