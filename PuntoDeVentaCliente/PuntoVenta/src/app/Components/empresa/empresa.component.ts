import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { EmpresaService } from 'src/app/Services/empresa.service';

@Component({
  selector: 'app-empresa',
  templateUrl: './empresa.component.html',
  styleUrls: ['./empresa.component.css']
})
export class EmpresaComponent implements OnInit {
  formEmpresa = this.formBuilder.group({
      rnc: ['', [Validators.required]],
      ncf: ['', [Validators.required]],
      razonSocial: ['', [Validators.required]],
      nombre: ['', [Validators.required]],
      direccion: ['', [Validators.required]],
      telefono: ['', [Validators.required]],
  }); 
  empresa: any;

  result = {
    IsSend: false,
    success: false,
    message: '',
  };

  constructor(private empresaService: EmpresaService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.getEmpresa();
  }

  getEmpresa(): void {
    this.empresaService.GetEmpresa().subscribe(e => {
       this.empresa = e.data;
       this.setValueForm();
    })
  }

  setValueForm(): void {
    this.formEmpresa.setValue({
      rnc: this.empresa[0].rnc,
      ncf: this.empresa[0].ncf,
      razonSocial: this.empresa[0].razonSocial,
      nombre: this.empresa[0].nombre,
      direccion: this.empresa[0].direccion,
      telefono: this.empresa[0].telefono,
    }, {onlySelf: true});
  }

  save(): void {
      if(this.empresa.length > 0){
        this.updateEmpresa();
      }else{
        this.addEmpresa();
      }
  }

  addEmpresa(): void {
    this.empresaService.AddEmpresa(this.formEmpresa.value).subscribe(e => {
      if(e.success){
        this.result.success = true;
        this.result.message = 'Datos de la empresa agregados correctamente!'
      }else{
        this.result.success = false;
        this.result.message = 'Ocurrio un error, vuelva a intentarlo!'
      }
      this.result.IsSend = true;
      this.getEmpresa();
   })
  }

  updateEmpresa(): void {
    this.empresaService.updateEmpresa(this.getDatosEditar).subscribe(e => {
      if(e.success){
        this.result.success = true;
        this.result.message = 'Datos de la empresa editados correctamente!'
      }else{
        this.result.success = false;
        this.result.message = 'Ocurrio un error, vuelva a intentarlo!'
      }
      this.result.IsSend = true;
      this.getEmpresa();
   })
  }

  get getDatosEditar() {
    return {
      id: this.empresa[0].id,
      rnc: this.formEmpresa.get('rnc')?.value,
      ncf: this.formEmpresa.get('ncf')?.value,
      razonSocial: this.formEmpresa.get('razonSocial')?.value,
      nombre: this.formEmpresa.get('nombre')?.value,
      direccion: this.formEmpresa.get('direccion')?.value,
      telefono: this.formEmpresa.get('telefono')?.value,
      fechaCreado:  this.empresa[0].fechaCreado,
      estatus:  this.empresa[0].estatus,
    }
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
      rnc: this.formEmpresa.get('rnc'),
      ncf: this.formEmpresa.get('ncf'),
      razonSocial: this.formEmpresa.get('razonSocial'),
      nombre: this.formEmpresa.get('nombre'),
      direccion: this.formEmpresa.get('direccion'),
      telefono: this.formEmpresa.get('telefono'),
    }
 }

}
