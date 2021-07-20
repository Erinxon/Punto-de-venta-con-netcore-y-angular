import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ApiClienteService } from 'src/app/Services/api-cliente.service';
import { ProductoService } from 'src/app/Services/producto.service';
import { VentasService } from 'src/app/Services/ventas.service';

@Component({
  selector: 'app-nueva-venta',
  templateUrl: './nueva-venta.component.html',
  styleUrls: ['./nueva-venta.component.css']
})
export class NuevaVentaComponent implements OnInit {
  public formVenta = this.formBuilder.group({
    idCliente: [null, [Validators.required]],
    idProducto: [null, [Validators.required]],
    cantidad: [null, [Validators.required, Validators.min(1)]],
  });

  listaVenta: any = {
    emailUsuario: '',
    idCliente: 0,
    detalleVentas: [],
  };

  //Campos Selected
  productos: any[] = [];
  clientes: any[] = [];

  ischoicesOpenProducto: boolean = false;
  ischoicesOpenCliente: boolean = false;

  productoSeleccionado = {
    id: 0,
    nombre: '',
  };

  ClienteSeleccionado = {
    id: 0,
    nombre: '',
  };

  searchCliente: string = '';
  IsSelectCliente: boolean = false;

  searchProducto: string = '';
  IsSelectProducto: boolean = false;

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  //paginacion
  p: number = 1;
  p2: number = 1;

  isLoandingSaveVenta = false;

  constructor(
    private ventaService: VentasService,
    private clienteService: ApiClienteService,
    private productoService: ProductoService,
    private formBuilder: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.getClientes();
    this.getProductos();
  }

  filterCliente(e: any) {
    this.searchCliente = e;
    if (this.searchCliente) {
      this.clienteService
        .filterCliente(this.searchCliente)
        .subscribe((p) => {
          this.clientes = p.data;
        });
    } else {
      this.getClientes();
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

  getClientes(): void {
    this.clienteService.getCliente().subscribe((c) => {
      this.clientes = c.data;
    });
  }

  openShoiceProducto(): void {
    this.IsSelectProducto = true;
    this.ischoicesOpenProducto = !this.ischoicesOpenProducto;
  }

  openShoiceCliente(): void {
    this.IsSelectCliente = true;
    this.ischoicesOpenCliente = !this.ischoicesOpenCliente;
  }

  selectProducto(id: number, nombre: string): void {
    this.formVenta.get('idProducto')?.setValue(id);
    this.productoSeleccionado = {
      id: id,
      nombre: nombre,
    };
  }

  selectCliente(id: number, nombre: string): void {
    this.formVenta.get('idCliente')?.setValue(id);
    this.ClienteSeleccionado = {
      id: id,
      nombre: nombre,
    };
  }



  get CamposValidos() {
    return {
      idCliente: this.formVenta.get('idCliente'),
      idProducto: this.formVenta.get('idProducto'),
      cantidad: this.formVenta.get('cantidad'),
    };
  }

  addItem(): void {
    if(!this.verificarStockProducto()){
      let producto =  this.listaVenta.detalleVentas
      .filter((x: { idProducto: number;})=>x.idProducto ===this.CamposValidos.idProducto?.value);
      
      if(producto.length > 0){
        let cont = 0;
        this.listaVenta.detalleVentas.forEach((c: {cantidad: number, idProducto: number}) => {
          if (c.idProducto === this.CamposValidos.idProducto?.value) {
            this.listaVenta.detalleVentas[cont].cantidad += this.CamposValidos.cantidad?.value;
          }
          cont++;
        });
        cont = 0;
      }else{
        let precio =  this.productos.find((p: {id: number}) => p.id === this.CamposValidos.idProducto?.value)['precio'];
        this.listaVenta.emailUsuario = JSON.parse(localStorage.getItem('usuario')!).email;
        this.listaVenta.idCliente = this.CamposValidos.idCliente?.value;
        this.listaVenta.detalleVentas.push({
          precio: precio,
          cantidad: this.CamposValidos.cantidad?.value,
          idProducto: this.CamposValidos.idProducto?.value,
        });
      }
    }else{
      this.result.IsSend = true;
      this.result.success = false;
      this.result.message = 'No hay stock suficiente!';
    }
  
  }

  getTotal(): number {
    let total = 0;
    this.listaVenta.detalleVentas.map(
      (x: { precio: number; cantidad: number }) => {
        total += x.precio * x.cantidad;
      }
    );
    return total;
  }

  public getNombreProducto(id: number): string {
    return this.productos.find((x) => x.id == id).nombreProducto;
  }

  quitarProducto(id: number): void {
    this.listaVenta.detalleVentas.splice(id, 1);
  }

  getNombreCliente(): string {
    const client = this.clientes.find((x) => x.id == this.ClienteSeleccionado.id);
    return `${client['nombre']} ${client['apellido']}`;
  }

  confirmarVenta(): void {
    this.isLoandingSaveVenta = true;
    this.ventaService.addVenta(this.listaVenta).subscribe(c => {
       if(c.success){
        this.result.IsSend = true;
        this.result.success = true;
        this.result.message = 'Venta realizada correctamente';
        this.isLoandingSaveVenta = false;
        this.OnResetForm();
       }else{
        this.result.IsSend = true;
        this.result.success = false;
        this.result.message = 'Ocurrio un error, vualva a intentarlo!';
        this.isLoandingSaveVenta = false;
       }
    })
  }


  verificarStockProducto(): boolean {
    let producto =  this.listaVenta.detalleVentas
      .filter((x: { idProducto: number;})=>x.idProducto ===this.CamposValidos.idProducto?.value);
    let idProducto = this.CamposValidos.idProducto?.value;
    let stockProducto =  this.productos.find((x) => x.id == idProducto).stock;
    let cantidad = 0;
    if(producto.length > 0){
        cantidad = producto[0].cantidad + this.CamposValidos.cantidad?.value;
    }else{
      cantidad  = this.CamposValidos.cantidad?.value;
    }
    return cantidad > stockProducto ? true : false;
  }

  OnResetForm(): void {
    this.formVenta.reset();
    this.IsSelectProducto = false;
    this.IsSelectCliente = false;
    this.productoSeleccionado = {
      id: 0,
      nombre: '',
    };
    this.ClienteSeleccionado = {
      id: 0,
      nombre: '',
    };
    this.ischoicesOpenProducto = false;
    this.ischoicesOpenCliente = false;
    this.listaVenta = {
      idUsuario: 0,
      idCliente: 0,
      detalleVentas: [],
    };
    this.getClientes();
    this.getProductos();
  }

  CerrarMensaje(): void {
    this.result.IsSend = false;
    this.result.success = false;
    this.result.message = '';
  }


}

