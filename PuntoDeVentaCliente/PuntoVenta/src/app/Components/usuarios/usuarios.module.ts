import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsuariosComponent } from './usuarios.component';
import { EditarUsuarioComponent } from './editar-usuario/editar-usuario.component';
import { AgregarUsuarioComponent } from './agregar-usuario/agregar-usuario.component';

import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';


@NgModule({
  declarations: [
    UsuariosComponent,
    EditarUsuarioComponent,
    AgregarUsuarioComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
  ]
})
export class UsuariosModule { }
