import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UsuarioService } from 'src/app/Services/usuario.service';

@Component({
  selector: 'app-agregar-usuario',
  templateUrl: './agregar-usuario.component.html',
  styleUrls: ['./agregar-usuario.component.css']
})
export class AgregarUsuarioComponent implements OnInit {
  usuarios: any[] = [];
  public usuarioForm = this.formBuilder.group({
    email: ['', Validators.required],
    nombreUsuario: ['', Validators.required],
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    pass: ['', Validators.required],
    RepetirPass: ['', Validators.required],
    roll: ['', Validators.required],
  })

  result = {
    IsSend: false,
    success: false,
    message: ''
  }
  
  constructor(
    private usuarioServices: UsuarioService,
    private formBuilder: FormBuilder) {

   }

  ngOnInit(): void {
    this.usuarioServices.getAllUsuario().subscribe(u => {
      this.usuarios = u.data;
    })

  }

   
  get CamposValidos() {
    return {
       email: this.usuarioForm.get('email'),
       nombreUsuario: this.usuarioForm.get('nombreUsuario'),
       nombre: this.usuarioForm.get('nombre'),
       apellido: this.usuarioForm.get('apellido'),
       pass: this.usuarioForm.get('pass'),
       RepetirPass: this.usuarioForm.get('RepetirPass'),
       roll: this.usuarioForm.get('roll')
    }
 }

  Guardar() : void {
    let usuario = this.GetDateUser;
    if(this.CamposValidos.pass?.value !== this.CamposValidos.RepetirPass?.value){
      this.result.IsSend = true;
      this.result.success = false;
      this.result.message =  `Las contraseñas deben ser iguales`;     
    }
    else if(this.validateUserExists()){
      this.result.IsSend = true;
      this.result.success = false;
      this.result.message =  `El usuario '${usuario.nombreUsuario}' ya existe, por favor intente con otro!`;     
    }else if(this.validateEmailExists()){
      this.result.IsSend = true;
      this.result.success = false;
      this.result.message =  `El email '${usuario.email}' ya existe, por favor intente con otro!`;
    }
    else{
      this.AddUserDb();
    }  
  }


  AddUserDb(): void{
    this.usuarioServices.addUsuario(this.GetDateUser).subscribe(r => {
      if(r.success === true){
        this.result.success = true;
        this.result.message = 'Usuario agregado correctamente';
        this.usuarioForm.reset();
      }else{       
       this.result.success = false;
       this.result.message =  'Ocurrio un error, vuelva a intentarlo';
      }
      this.result.IsSend = true;
   });   
  }

  get GetDateUser() {
    return {
      email: this.usuarioForm.get('email')?.value,
      nombreUsuario: this.usuarioForm.get('nombreUsuario')?.value,
      nombre: this.usuarioForm.get('nombre')?.value,
      apellido: this.usuarioForm.get('apellido')?.value,
      pass: this.usuarioForm.get('pass')?.value,
      roll: this.usuarioForm.get('roll')?.value
    }
  }

  validateUserExists(): boolean {
    let user = this.usuarios.
              find(u => u.nombreUsuario === this.usuarioForm.get('nombreUsuario')?.value);
    return user ? true : false;
  }

  validateEmailExists(): boolean {
    let user = this.usuarios.
    find(u => u.email === this.usuarioForm.get('email')?.value);
    return user ? true : false;
  }

  CerrarMensaje(): void {
    this.result.IsSend = false;
    this.result.success = false;
    this.result.message = '';
  }
  


}
