import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './Components/menu/menu.component';
import { FooterComponent } from './Components/footer/footer.component';
import { ContentComponent } from './Components/content/content.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './Components/login/login.component';
import { HomeComponent } from './Components/home/home.component';
import { JwtInterceptor } from './Security/jwt.interceptor';

import { HeaderComponent } from './Components/header/header.component';

import { NgxPaginationModule } from 'ngx-pagination';
import {MatDialogModule} from '@angular/material/dialog';
import { PerfilComponent } from './Components/perfil/perfil.component';
import { ErrorsComponent } from './Components/errors/errors.component';

import { ProductosModule } from './Components/productos/productos.module';
import { ClienteModule } from './Components/Cliente/cliente.module';
import { CategoriaModule } from './Components/categoria/categoria.module';
import { ProveedoresModule } from './Components/proveedores/proveedores.module';
import { ComprasModule } from './Components/compras/compras.module';
import { VentasModule } from './Components/ventas/ventas.module';
import { UsuariosModule } from './Components/usuarios/usuarios.module';
import { EmpresaModule } from './Components/empresa/empresa.module';


@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    FooterComponent,
    ContentComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    PerfilComponent,
    ErrorsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    MatDialogModule,
    ProductosModule,
    ClienteModule,
    CategoriaModule,
    ProveedoresModule,
    ComprasModule,
    VentasModule,
    UsuariosModule,
    EmpresaModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
