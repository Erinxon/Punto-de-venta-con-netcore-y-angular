import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ProveedorService } from 'src/app/Services/proveedor.service';

@Component({
  selector: 'app-agregar-proveedor',
  templateUrl: './agregar-proveedor.component.html',
  styleUrls: ['./agregar-proveedor.component.css']
})
export class AgregarProveedorComponent implements OnInit {
  public addProveedor = this.formBuilder.group({
    rnc: ['', Validators.required],
    razonSocial: ['', Validators.required],
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
  
  constructor(
    private proveedorServices: ProveedorService,
    private formBuilder: FormBuilder) {

   }

  ngOnInit(): void {

  }

   
  get CamposValidos() {
    return {
       rnc: this.addProveedor.get('rnc'),
       razonSocial: this.addProveedor.get('razonSocial'),
       telefono: this.addProveedor.get('telefono'),
       provincia: this.addProveedor.get('direccion.provincia'),
       ciudad: this.addProveedor.get('direccion.ciudad'),
       calle: this.addProveedor.get('direccion.calle'),
       numeroCasa: this.addProveedor.get('direccion.numeroCasa')
    }
 }

  Guardar() : void {
    this.proveedorServices.addProvedor(this.addProveedor.value).subscribe(r => {
      if(r.success === true){
        this.result.success = true;
        this.result.message = 'Proveedor agregado correctamente';
        this.addProveedor.reset();
      }else{       
       this.result.success = false;
       this.result.message =  'Ocurrio un error, vuelva a intentarlo';
      }
      this.result.IsSend = true;
   });   
  }

  CerrarMensaje(): void {
    this.result.IsSend = false;
    this.result.success = false;
    this.result.message = '';
  }


}
