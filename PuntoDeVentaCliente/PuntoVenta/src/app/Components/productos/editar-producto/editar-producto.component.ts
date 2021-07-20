import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriaService } from 'src/app/Services/categoria-service';
import { ProductoService } from 'src/app/Services/producto.service';

@Component({
  selector: 'app-editar-producto',
  templateUrl: './editar-producto.component.html',
  styleUrls: ['./editar-producto.component.css'],
})
export class EditarProductoComponent implements OnInit {
  public productForm = this.formBuilder.group({
    id: [0, [Validators.required]],
    codigo: [
      null,
      [Validators.required, Validators.maxLength(10), Validators.minLength(5)],
    ],
    nombreProducto: [null, [Validators.required, Validators.maxLength(100)]],
    descripcion: [null, [Validators.required, Validators.maxLength(250)]],
    imagen: [null, [Validators.required]],
    costo: [
      null,
      [Validators.required, Validators.maxLength(14), Validators.minLength(0)],
    ],
    precio: [
      null,
      [Validators.required, Validators.maxLength(14), Validators.minLength(0)],
    ],
    stock: [null, [Validators.required, Validators.minLength(0)]],
    idCategoria: [0, [Validators.required]],
    fechaCreado: [null, [Validators.required]],
    estatus: [null, Validators.required],
  });

  ischoicesOpen: boolean = false;
  ListCategoria: any;
  categoriasSelected = {
    id: 0,
    nombre: '',
  };
  searchCategoria: string = '';
  IsSelectCategoria: boolean = false;

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private serviceProducto: ProductoService,
    private categoriaServices: CategoriaService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    const idProducto = +this.activatedRoute.snapshot.paramMap.get('id')!;
    this.serviceProducto.getProductoById(idProducto).subscribe((p) => {
      let producto: any = p.data;
      this.categoriasSelected = {
        id: producto['categoria']['id'],
        nombre: producto['categoria']['nombre'],
      };
      this.productForm.setValue({
        id: producto['id'],
        codigo: producto['codigo'],
        nombreProducto: producto['nombreProducto'],
        descripcion: producto['descripcion'],
        imagen: producto['imagen'],
        costo: producto['costo'],
        precio: producto['precio'],
        stock: producto['stock'],
        idCategoria: producto['categoria']['id'],
        fechaCreado: producto['fechaCreado'],
        estatus: producto['estatus'],
      }, {onlySelf: true});
    });
    this.getCategorias();
  }

  getCategorias(): void {
    this.categoriaServices.getCategoria().subscribe((c) => {
      this.ListCategoria = c.data;
    });
  }

  filterCategoria(e: any) {
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
    this.serviceProducto.updateProducto(this.productForm.value).subscribe((p) => {
      if (p.success) {
         this.router.navigate(['productos']);
      } else {
        this.result.IsSend = true;
        this.result.success = false;
        this.result.message = p.message;
      }
    });
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

}
