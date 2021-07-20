import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/Services/usuario.service';
import {NgxPaginationModule} from 'ngx-pagination'; 

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  usuarios: any;
  filters: string = '';
  p: number = 1;
  loanding: boolean = true;

  constructor(
    private usuarioServices: UsuarioService
    ) { }

  ngOnInit(): void {
    this.getAllUsuario();
  }

  getAllUsuario(): void {
    this.usuarioServices.getAllUsuario().subscribe(u => {
      this.usuarios = u.data;
      this.loanding = false;
    });
  }

  filtrar(): void {
    if(this.filters){
      this.usuarioServices.filterUsuario(this.filters).subscribe(u => {
        this.usuarios = u.data;
      });
    }else{
      this.getAllUsuario();
    }
  }

  clearFilter(): void {
      this.filters = '';
      this.getAllUsuario();
  }

  updateEstatusUsuario(id: number): void {
     this.usuarioServices.updateStatusUsuario(id).subscribe(u => {
        if(u.success){
          this.getAllUsuario();
        }
     })
  }

}
