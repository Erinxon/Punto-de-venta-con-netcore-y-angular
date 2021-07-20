import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ApiClienteService } from 'src/app/Services/api-cliente.service';
import { ValidarCedula } from 'src/app/Validations/ValidarCedula';

@Component({
  selector: 'app-agregar-cliente',
  templateUrl: './agregar-cliente.component.html',
  styleUrls: ['./agregar-cliente.component.css']
})
export class AgregarClienteComponent implements OnInit {
  public addCliente = this.formBuilder.group({
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    cedula: ['', [Validators.required]],  
    telefono: ['', [
      Validators.required,
      Validators.pattern('^\\s*(?:\\+?(\\d{1,3}))?[-. (]*(\\d{3})[-. )]*(\\d{3})[-. ]*(\\d{4})(?: *x(\\d+))?\\s*$')
    ]],
    direccion: this.formBuilder.group({
      provincia: ['', Validators.required],
      ciudad: ['', Validators.required],
      calle:['', Validators.required],
      numeroCasa: ['', Validators.required],
   })
  })

  result = {
    IsSend: false,
    success: false,
    message: ''
  }
  
  constructor(private apicliente: ApiClienteService
    , private formBuilder: FormBuilder) {

   }

  ngOnInit(): void {

  }

   
  get CamposValidos() {
    return {
       nombre: this.addCliente.get('nombre'),
       apellido: this.addCliente.get('apellido'),
       cedula: this.addCliente.get('cedula'),
       email: this.addCliente.get('email'),
       telefono: this.addCliente.get('telefono'),
       provincia: this.addCliente.get('direccion.provincia'),
       ciudad: this.addCliente.get('direccion.ciudad'),
       calle: this.addCliente.get('direccion.calle'),
       numeroCasa: this.addCliente.get('direccion.numeroCasa')
    }
 }

  Guardar() : void {
    const validar = new ValidarCedula();
    if(validar.isValida(this.addCliente.get('cedula')?.value)){
      this.apicliente.addCliente(this.addCliente.value).subscribe(r => {
        if(r.success === true){
          this.result.success = true;
          this.result.message = 'Cliente agregado correctamente';
          this.addCliente.reset();
        }else{       
         this.result.success = false;
         this.result.message =  'Ocurrio un error, vuelva a intentarlo';
        }
        this.result.IsSend = true;
     });
    }else{
      this.addCliente.controls['cedula'].setErrors({'incorrect': true});
    }
    
  }

  CerrarMensaje(): void {
    this.result.IsSend = false;
    this.result.success = false;
    this.result.message = '';
  }

 

}
