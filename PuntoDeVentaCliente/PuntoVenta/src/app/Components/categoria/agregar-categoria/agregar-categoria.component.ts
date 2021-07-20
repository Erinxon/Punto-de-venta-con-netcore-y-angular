import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { CategoriaService } from 'src/app/Services/categoria-service';

@Component({
  selector: 'app-agregar-categoria',
  templateUrl: './agregar-categoria.component.html',
  styleUrls: ['./agregar-categoria.component.css'],
})
export class AgregarCategoriaComponent implements OnInit {
  public addCategoria = this.formBuilder.group({
    nombre: ['',[ Validators.required, Validators.pattern("[a-zA-Z ]*")]],
    estatus: [true],
  });

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  constructor(
    private serviceCategoria: CategoriaService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {}

  Guardar(): void {
    this.serviceCategoria
      .addCategoria(this.addCategoria.value)
      .subscribe((c) => {
        if (c.success) {
          this.result = {
            IsSend: true,
            success: true,
            message: 'La Categoria se agregó correctamente',
          };
          this.addCategoria.reset();
        } else {
          this.result = {
            IsSend: true,
            success: false,
            message: 'Ocurrio un error, vuelva a intentarlo!',
          };
        }
      });
  }

  CerrarMensaje(): void {
    this.result = {
      IsSend: false,
      success: false,
      message: '',
    };
  }

  get CamposValidos() {
    return {
       nombre: this.addCategoria.get('nombre'),
    }
 }

}
