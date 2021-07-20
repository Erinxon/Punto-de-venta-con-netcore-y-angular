import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UsuarioService } from 'src/app/Services/usuario.service';

@Component({
  selector: 'app-editar-usuario',
  templateUrl: './editar-usuario.component.html',
  styleUrls: ['./editar-usuario.component.css']
})
export class EditarUsuarioComponent implements OnInit {

  usuarios: any[] = [];
  usuarioEdit: any;

  public usuarioForm = this.formBuilder.group({
    id: ['', Validators.required],
    email: ['', Validators.required],
    nombreUsuario: ['', Validators.required],
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    roll: ['', Validators.required],
  })

  result = {
    IsSend: false,
    success: false,
    message: ''
  }
  
  constructor(
    private usuarioServices: UsuarioService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router) {

   }

  ngOnInit(): void {
    this.getUsuarioEdit();
    this.getUsuarios();
  }

  getUsuarioEdit(): void {
    let idUsuario = + this.activatedRoute.snapshot.paramMap.get('id')!;
    this.usuarioServices.getUsuarioById(idUsuario).subscribe(u => {
       this.usuarioEdit =  u.data
       this.usuarioForm.setValue({
        id: this.usuarioEdit.id,
        email: this.usuarioEdit.email,
        nombreUsuario: this.usuarioEdit.nombreUsuario,
        nombre: this.usuarioEdit.nombre,
        apellido: this.usuarioEdit.apellido,
        roll: this.usuarioEdit.roll,
       }, {onlySelf: true});
    })
  }

  getUsuarios(): void {
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
       roll: this.usuarioForm.get('roll')
    }
 }

  Guardar() : void {
    let usuario = this.GetDateUser;
    if(this.validateUserExists() && 
      this.CamposValidos.nombreUsuario?.value !== this.usuarioEdit.nombreUsuario){
      this.result.IsSend = true;
      this.result.success = false;
      this.result.message =  `El usuario '${usuario.nombreUsuario}' ya existe, por favor intente con otro!`;     
    }else if(this.validateEmailExists() && 
      this.CamposValidos.email?.value !== this.usuarioEdit.email){
      this.result.IsSend = true;
      this.result.success = false;
      this.result.message =  `El email '${usuario.email}' ya existe, por favor intente con otro!`;
    }
    else{
      this.AddUserDb();
    }  
  }


  AddUserDb(): void{
    this.usuarioServices.updateUsuario(this.GetDateUser).subscribe(r => {
      if(r.success === true){
        this.result.success = true;
        this.router.navigate(['/usuarios'])
      }else{       
       this.result.success = false;
       this.result.message =  'Ocurrio un error, vuelva a intentarlo';
      }
      this.result.IsSend = true;
   });   
  }

  get GetDateUser() {
    return {
      id: this.usuarioEdit.id,
      email: this.CamposValidos.email?.value,
      nombreUsuario: this.CamposValidos.nombreUsuario?.value,
      nombre: this.CamposValidos.nombre?.value,
      apellido: this.CamposValidos.apellido?.value,
      pass: this.usuarioEdit.pass,
      roll: this.CamposValidos.roll?.value,
      fechaCreado: this.usuarioEdit.fechaCreado,
      estatus: this.usuarioEdit.estatus,
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
