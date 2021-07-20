import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ProductoService } from 'src/app/Services/producto.service';
import { ProveedorService } from 'src/app/Services/proveedor.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { UsuarioService } from 'src/app/Services/usuario.service';
import { ComprasService } from 'src/app/Services/compras.service';

@Component({
  selector: 'app-nueva-compra',
  templateUrl: './nueva-compra.component.html',
  styleUrls: ['./nueva-compra.component.css'],
})
export class NuevaCompraComponent implements OnInit {
  public formCompra = this.formBuilder.group({
    idProveedor: [null, [Validators.required]],
    idProducto: [null, [Validators.required]],
    costo: [null, [Validators.required, Validators.min(1)]],
    cantidad: [null, [Validators.required, Validators.min(1)]],
  });
  listaCompra: any = {
    EmailUsuario: '',
    idProveedor: 0,
    detalleCompra: [],
  };

  //Campos Selected
  productos: any[] = [];
  proveedores: any;

  ischoicesOpenProducto: boolean = false;
  ischoicesOpenProveedor: boolean = false;

  productoSeleccionado = {
    id: 0,
    nombre: '',
  };

  proveedorSeleccionado = {
    id: 0,
    razonSocial: '',
  };

  searchProveedor: string = '';
  IsSelectProveedor: boolean = false;

  searchProducto: string = '';
  IsSelectProducto: boolean = false;

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  //paginacion
  p: number = 1;

  constructor(
    private proveedorService: ProveedorService,
    private productoService: ProductoService,
    private compraService: ComprasService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getProveedores();
    this.getProductos();
  }

  filterProveedor(e: any) {
    this.searchProveedor = e;
    if (this.searchProveedor) {
      this.proveedorService
        .getFilterProveedores(this.searchProveedor)
        .subscribe((p) => {
          this.proveedores = p.data;
        });
    } else {
      this.getProveedores();
    }
  }

  filterProductos(e: any) {
    this.searchProducto = e;
    if (this.searchProducto) {
      this.productoService
        .getFilterProductos(this.searchProducto)
        .subscribe((p) => {
          this.productos = p.data;
        });
    } else {
      this.getProductos();
    }
  }

  getProductos(): void {
    this.productoService.getAllProductos().subscribe((p) => {
      this.productos = p.data;
    });
  }

  getProveedores(): void {
    this.proveedorService.getAllProveedores().subscribe((p) => {
      this.proveedores = p.data;
    });
  }

  openShoiceProducto(): void {
    this.IsSelectProducto = true;
    this.ischoicesOpenProducto = !this.ischoicesOpenProducto;
  }

  openShoiceProveedor(): void {
    this.IsSelectProveedor = true;
    this.ischoicesOpenProveedor = !this.ischoicesOpenProveedor;
  }

  selectProducto(id: number, nombre: string): void {
    this.formCompra.get('idProducto')?.setValue(id);
    this.productoSeleccionado = {
      id: id,
      nombre: nombre,
    };
  }

  selectProveedor(id: number, nombre: string): void {
    this.formCompra.get('idProveedor')?.setValue(id);
    this.proveedorSeleccionado = {
      id: id,
      razonSocial: nombre,
    };
  }

  OnResetForm(): void {
    this.formCompra.reset();
    this.IsSelectProducto = false;
    this.IsSelectProveedor = false;
    this.productoSeleccionado = {
      id: 0,
      nombre: '',
    };
    this.proveedorSeleccionado = {
      id: 0,
      razonSocial: '',
    };
    this.ischoicesOpenProducto = false;
    this.ischoicesOpenProveedor = false;
    this.listaCompra = {
      idUsuario: 0,
      idProveedor: 0,
      detalleCompra: [],
    };
  }

  get CamposValidos() {
    return {
      idProveedor: this.formCompra.get('idProveedor'),
      idProducto: this.formCompra.get('idProducto'),
      costo: this.formCompra.get('costo'),
      cantidad: this.formCompra.get('cantidad'),
    };
  }

  addItem(): void {
    let producto =  this.listaCompra.detalleCompra
    .filter((x: { idProducto: number;})=>x.idProducto ===this.CamposValidos.idProducto?.value);
    
    if(producto.length > 0){
      let cont = 0;
      this.listaCompra.detalleCompra.forEach((c: {cantidad: number, idProducto: number}) => {
        if (c.idProducto === this.CamposValidos.idProducto?.value) {
          this.listaCompra.detalleCompra[cont].cantidad += this.CamposValidos.cantidad?.value;
        }
        cont++;
      });
      cont = 0;
    }else{
      this.listaCompra.EmailUsuario = JSON.parse(localStorage.getItem('usuario')!).email;
      this.listaCompra.idProveedor = this.CamposValidos.idProveedor?.value;
      this.listaCompra.detalleCompra.push({
         costo: this.CamposValidos.costo?.value,
         cantidad: this.CamposValidos.cantidad?.value,
         idProducto: this.CamposValidos.idProducto?.value,
      });
    }
    
    
   
  }

  getTotal(): number {
    let total = 0;
    this.listaCompra.detalleCompra.map(
      (x: { costo: number; cantidad: number }) => {
        total += x.costo * x.cantidad;
      }
    );
    return total;
  }

  public getNombreProducto(id: number): string {
    return this.productos.find((x) => x.id == id).nombreProducto;
  }

  quitarProducto(id: number): void {
    this.listaCompra.detalleCompra.splice(id, 1);
  }

  getRazonSocialProveedor(id: number): string {
    let razonSocial = '';
    this.proveedorService.getProveedorById(id).subscribe((p) => {
      razonSocial = p.data['razonSocial'];
    });
    return razonSocial;
  }

  confirmarCompra(): void {
    this.compraService.addCompra(this.listaCompra).subscribe(c => {
       if(c.success){
        this.result.IsSend = true;
        this.result.success = true;
        this.result.message = 'Compra realizada correctamente';
        this.OnResetForm();
       }else{
        this.result.IsSend = true;
        this.result.success = false;
        this.result.message = 'Ocurrio un error, vualva a intentarlo!';
       }
    })
  }

  CerrarMensaje(): void {
    this.result.IsSend = false;
    this.result.success = false;
    this.result.message = '';
  }
}
