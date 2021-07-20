import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditarClienteComponent } from './editar-cliente/editar-cliente.component';
import { AgregarClienteComponent } from './agregar-cliente/agregar-cliente.component';
import { ClienteComponent } from './cliente.component';

import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    ClienteComponent,
    EditarClienteComponent,
    AgregarClienteComponent
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
export class ClienteModule { }
