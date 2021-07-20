import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductoService } from 'src/app/Services/producto.service';
import { VentasService } from 'src/app/Services/ventas.service';

@Component({
  selector: 'app-factura-venta',
  templateUrl: './factura-venta.component.html',
  styleUrls: ['./factura-venta.component.css']
})
export class FacturaVentaComponent implements OnInit {
  factura: any;
  productos: any[] = [];
  constructor(private ventaServices: VentasService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private productoSerivices: ProductoService) { }

  ngOnInit(): void {
    this.getProductos();
    this.getFactura();
  }

  getFactura(): void {
    const idVenta = +this.activatedRoute.snapshot.paramMap.get('id')!;
    this.ventaServices.getVentaById(idVenta).subscribe(v => {
       this.factura = v.data;
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
    this.factura.detalleVentas.map(
      (x: { precio: number; cantidad: number }) => {
        total += x.precio * x.cantidad;
      }
    );
    return total;
  }

}
