import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComprasComponent } from './compras.component';
import { NuevaCompraComponent } from './nueva-compra/nueva-compra.component';
import { ComprobanteCompraComponent } from './comprobante-compra/comprobante-compra.component';


import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    ComprasComponent,
    NuevaCompraComponent,
    ComprobanteCompraComponent,
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
export class ComprasModule { }
