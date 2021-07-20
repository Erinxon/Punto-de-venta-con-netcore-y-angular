import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EditClienteModel } from 'src/app/Models/EditClienteModel';
import { ApiClienteService } from 'src/app/Services/api-cliente.service';
import { ApiAuthService } from 'src/app/Services/apiAuth-service';
//import { CedulaValida } from '../../../Validations/ValidarCedula';

@Component({
  selector: 'app-editar-cliente',
  templateUrl: './editar-cliente.component.html',
  styleUrls: ['./editar-cliente.component.css']
})
export class EditarClienteComponent implements OnInit {
  public editCliente = this.formBuilder.group({
    id: [0, Validators.required],
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    cedula: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    telefono: ['', [
      Validators.required,
      Validators.pattern('^\\s*(?:\\+?(\\d{1,3}))?[-. (]*(\\d{3})[-. )]*(\\d{3})[-. ]*(\\d{4})(?: *x(\\d+))?\\s*$')
    ]],
    direccion: this.formBuilder.group({
      id: [0, Validators.required],
      provincia: ['', Validators.required],
      ciudad: ['', Validators.required],
      calle:['', Validators.required],
      numeroCasa: ['', Validators.required],
   }),
   fechaCreado: ['', Validators.required],
   estatus: [false, Validators.required]
  })

  result = {
    IsSend: false,
    success: false,
    message: ''
  }

  constructor(private route: ActivatedRoute, private apiClienteSerice: ApiClienteService,
    private formBuilder: FormBuilder, private router: Router) {
        
   }

  ngOnInit(): void {
    const idCliente = + this.route.snapshot.paramMap.get('id')!;
    this.apiClienteSerice.getClienteById(idCliente).subscribe(c => { 
      this.editCliente.setValue(c.data, {onlySelf: true});
    });    
  }


  get CamposValidos() {
    return {
       nombre: this.editCliente.get('nombre'),
       apellido: this.editCliente.get('apellido'),
       cedula: this.editCliente.get('cedula'),
       email: this.editCliente.get('email'),
       telefono: this.editCliente.get('telefono'),
       provincia: this.editCliente.get('direccion.provincia'),
       ciudad: this.editCliente.get('direccion.ciudad'),
       calle: this.editCliente.get('direccion.calle'),
       numeroCasa: this.editCliente.get('direccion.numeroCasa')
    }
 }

 Guardar(): void {
   this.apiClienteSerice.updateCliente(this.editCliente.value).subscribe(c=>{
      if(c.success){
         this.router.navigate(['cliente']);
      }else{
        this.result.IsSend = true;
        this.result.success = false;
        this.result.message = c.message;
      }
   })
 }

 CerrarMensaje(): void {
  this.result.IsSend = false;
  this.result.success = false;
  this.result.message = '';
}

}
