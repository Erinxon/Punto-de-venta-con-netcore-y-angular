import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CategoriaService } from 'src/app/Services/categoria-service';
import { ProductoService } from 'src/app/Services/producto.service';

@Component({
  selector: 'app-agregar-producto',
  templateUrl: './agregar-producto.component.html',
  styleUrls: ['./agregar-producto.component.css'],
})
export class AgregarProductoComponent implements OnInit {
  ischoicesOpen: boolean = false;
  ListCategoria: any;
  categoriasSelected = {
    id: 0,
    nombre: '',
  };
  searchCategoria: string = '';
  IsSelectCategoria: boolean = false;

  public productForm = this.formBuilder.group({
    codigo: [
      '',
      [Validators.required, Validators.maxLength(10), Validators.minLength(5)],
    ],
    nombreProducto: ['', [Validators.required, Validators.maxLength(100)]],
    descripcion: ['', [Validators.required, Validators.maxLength(250)]],
    imagen: ['', [Validators.required]],
    costo: [
      null,
      [Validators.required, Validators.maxLength(14), Validators.minLength(0)],
    ],
    precio:  [
      null,
      [Validators.required, Validators.maxLength(14), Validators.minLength(0)],
    ],
    stock: [null, [Validators.required, Validators.minLength(0)]],
    idCategoria: [null, [Validators.required]],
    estatus: [true, Validators.required],
  });

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  constructor(
    private serviceProducto: ProductoService,
    private categoriaServices: CategoriaService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getCategorias();
  }

  getCategorias(): void {
    this.categoriaServices.getCategoria().subscribe((c) => {
      this.ListCategoria = c.data;
    });
  }

  filterCategoria(e:any) {
    this.searchCategoria = e; 
    if (this.searchCategoria) {
      this.categoriaServices
        .getFilterCategoria(this.searchCategoria)
        .subscribe((c) => {
           this.ListCategoria = c.data;
        });
    } else {
      this.getCategorias();
    }
  }

  get CamposValidos() {
    return {
      codigo: this.productForm.get('codigo'),
      nombreProducto: this.productForm.get('nombreProducto'),
      descripcion: this.productForm.get('descripcion'),
      imagen: this.productForm.get('imagen'),
      costo: this.productForm.get('costo'),
      precio: this.productForm.get('precio'),
      stock: this.productForm.get('stock'),
      idCategoria: this.productForm.get('idCategoria'),
    };
  }

  Guardar(): void {
     this.serviceProducto.addProducto(this.productForm.value).subscribe( p =>  {
        if(p.success){
            this.result.success = true;
            this.result.message = 'Producto agregado correctamente';
            this.limpiarFormulario();
        }else{
            this.result.success = false;
            this.result.message = 'Ocurrio un error, vuelva a intentarlo';
        }
        this.result.IsSend = true;
     })
  }

  CerrarMensaje(): void {
    this.result.IsSend = false;
    this.result.success = false;
    this.result.message = '';
  }

  openShoice(): void {
    this.IsSelectCategoria = true;
    this.ischoicesOpen = !this.ischoicesOpen;
  }

  selectCategoria(id: number, nombre: string): void {
    this.productForm.get('idCategoria')?.setValue(id);
    this.categoriasSelected = {
      id: id,
      nombre: nombre,
    };
  }

  limpiarFormulario(): void {
    this.productForm.reset();
    this.IsSelectCategoria = false;
    this.categoriasSelected = {
      id: 0,
      nombre: '',
    };
    this.ischoicesOpen = false;

    this.CerrarMensaje();
  }
}
