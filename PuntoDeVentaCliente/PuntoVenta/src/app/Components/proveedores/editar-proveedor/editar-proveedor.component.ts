import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProveedorService } from 'src/app/Services/proveedor.service';

@Component({
  selector: 'app-editar-proveedor',
  templateUrl: './editar-proveedor.component.html',
  styleUrls: ['./editar-proveedor.component.css']
})
export class EditarProveedorComponent implements OnInit {

  public editProveedor = this.formBuilder.group({
    id: [0, Validators.required],
    rnc: ['', Validators.required],
    razonSocial: ['', Validators.required],
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
   fechaCreado: [null, Validators.required],
   estatus: [null, Validators.required]
  })

  result = {
    IsSend: false,
    success: false,
    message: ''
  }
  
  constructor(
    private proveedorServices: ProveedorService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router) {

   }

  ngOnInit(): void {
    const idProveedor = + this.activatedRoute.snapshot.paramMap.get('id')!;
    this.proveedorServices.getProveedorById(idProveedor).subscribe(p => {
       this.editProveedor.setValue(p.data, {onlySelf: true});
    })

  }

  get CamposValidos() {
    return {
       rnc: this.editProveedor.get('rnc'),
       razonSocial: this.editProveedor.get('razonSocial'),
       telefono: this.editProveedor.get('telefono'),
       provincia: this.editProveedor.get('direccion.provincia'),
       ciudad: this.editProveedor.get('direccion.ciudad'),
       calle: this.editProveedor.get('direccion.calle'),
       numeroCasa: this.editProveedor.get('direccion.numeroCasa')
    }
 }

  Guardar() : void {
    this.proveedorServices.updateProveedor(this.editProveedor.value).subscribe(r => {
      console.log(r.data);
      if(r.success){
        this.result.success = true;
        this.router.navigate(['/proveedores'])
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
