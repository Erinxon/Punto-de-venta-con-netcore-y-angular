import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriaService } from 'src/app/Services/categoria-service';

@Component({
  selector: 'app-editar-categoria',
  templateUrl: './editar-categoria.component.html',
  styleUrls: ['./editar-categoria.component.css']
})
export class EditarCategoriaComponent implements OnInit {
  public editCategoria = this.formBuilder.group({
    id: [0],
    nombre: ['',[ Validators.required, Validators.pattern("[a-zA-Z ]*")]],
    fechaCreado: [null],
    estatus: [true],
  });

  constructor(private route: ActivatedRoute, private serviceCategoria: CategoriaService,
    private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    const idCategoria = + this.route.snapshot.paramMap.get('id')!;
     this.serviceCategoria.getCategoriaById(idCategoria).subscribe(c => {
       this.editCategoria.setValue(c.data);
     })
  }

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  Guardar(): void {
    this.serviceCategoria
      .updateCategoria(this.editCategoria.value)
      .subscribe((c) => {
        if (c.success) {
          this.result = {
            IsSend: false,
            success: true,
            message: '',
          };
          this.router.navigate(['/categorias']);
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
       nombre: this.editCategoria.get('nombre'),
    }
 }



}
