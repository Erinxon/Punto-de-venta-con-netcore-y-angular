import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ComprasService } from 'src/app/Services/compras.service';
import { ProductoService } from 'src/app/Services/producto.service';
import { VentasService } from 'src/app/Services/ventas.service';

@Component({
  selector: 'app-comprobante-compra',
  templateUrl: './comprobante-compra.component.html',
  styleUrls: ['./comprobante-compra.component.css']
})
export class ComprobanteCompraComponent implements OnInit {

  factura: any;
  productos: any[] = [];
  constructor(private compraServices: ComprasService,
    private activatedRoute: ActivatedRoute,
    private productoSerivices: ProductoService) { }

  ngOnInit(): void {
    this.getProductos();
    this.getFactura();
  }

  getFactura(): void {
    const idCompra = + this.activatedRoute.snapshot.paramMap.get('id')!;
    console.log(idCompra)
    this.compraServices.getCompraById(idCompra).subscribe(c => {
       this.factura = c.data;
    });
  }

  getProductos(): void {
    this.productoSerivices.getAllProductos().subscribe(p => {
      this.productos = p.data;
    })
  }


  public getNombreProducto(id: number): string {
    let producto = this.productos.find((x) => x.id == id);
    return producto.nombreProducto;
  }

  getTotal(): number {
    let total = 0;
    this.factura.detalleCompra.map(
      (x: { costo: number; cantidad: number }) => {
        total += x.costo * x.cantidad;
      }
    );
    return total;
  }

}
