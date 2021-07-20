import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NuevaVentaComponent } from './nueva-venta/nueva-venta.component';
import { VentasComponent } from './ventas.component';
import { FacturaVentaComponent } from './factura-venta/factura-venta.component';

import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  declarations: [
    VentasComponent,
    NuevaVentaComponent,
    FacturaVentaComponent,
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
export class VentasModule { }
