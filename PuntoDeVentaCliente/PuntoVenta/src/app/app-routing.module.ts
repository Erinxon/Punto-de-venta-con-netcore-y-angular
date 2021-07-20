import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteComponent } from './Components/Cliente/cliente.component';
import { AgregarClienteComponent } from './Components/Cliente/agregar-cliente/agregar-cliente.component';
import { LoginComponent } from './Components/login/login.component';
import { AuthGuard } from './Security/Auth.guard';
import { HomeComponent } from './Components/home/home.component';
import { EditarClienteComponent } from './Components/Cliente/editar-cliente/editar-cliente.component';
import { ProductosComponent } from './Components/productos/productos.component';
import { CategoriaComponent } from './Components/categoria/categoria.component';
import { ProveedoresComponent } from './Components/proveedores/proveedores.component';
import { AgregarCategoriaComponent } from './Components/categoria/agregar-categoria/agregar-categoria.component';
import { EditarCategoriaComponent } from './Components/categoria/editar-categoria/editar-categoria.component';
import { AgregarProductoComponent } from './Components/productos/agregar-producto/agregar-producto.component';
import { EditarProductoComponent } from './Components/productos/editar-producto/editar-producto.component';
import { AgregarProveedorComponent } from './Components/proveedores/agregar-proveedor/agregar-proveedor.component';
import { EditarProveedorComponent } from './Components/proveedores/editar-proveedor/editar-proveedor.component';
import { ComprasComponent } from './Components/compras/compras.component';
import { NuevaCompraComponent } from './Components/compras/nueva-compra/nueva-compra.component';
import { BrowserModule } from '@angular/platform-browser';
import { VentasComponent } from './Components/ventas/ventas.component';
import { NuevaVentaComponent } from './Components/ventas/nueva-venta/nueva-venta.component';
import { FacturaVentaComponent } from './Components/ventas/factura-venta/factura-venta.component';
import { ComprobanteCompraComponent } from './Components/compras/comprobante-compra/comprobante-compra.component';
import { PerfilComponent } from './Components/perfil/perfil.component';
import { UsuariosComponent } from './Components/usuarios/usuarios.component';
import { AgregarUsuarioComponent } from './Components/usuarios/agregar-usuario/agregar-usuario.component';
import { EditarUsuarioComponent } from './Components/usuarios/editar-usuario/editar-usuario.component';
import { EmpresaComponent } from './Components/empresa/empresa.component';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'cliente', component: ClienteComponent, canActivate: [AuthGuard]},
  {path: 'cliente/agregar', component: AgregarClienteComponent, canActivate: [AuthGuard]},
  {path: 'cliente/editar/:id', component: EditarClienteComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'productos', component: ProductosComponent, canActivate: [AuthGuard]},
  {path: 'productos/agregar', component: AgregarProductoComponent, canActivate: [AuthGuard]},
  {path: 'productos/editar/:id', component: EditarProductoComponent, canActivate: [AuthGuard]},
  {path: 'categorias', component: CategoriaComponent, canActivate: [AuthGuard]},
  {path: 'categorias/agregar', component: AgregarCategoriaComponent, canActivate: [AuthGuard]},
  {path: 'categorias/editar/:id', component: EditarCategoriaComponent, canActivate: [AuthGuard]},
  {path: 'proveedores', component: ProveedoresComponent, canActivate: [AuthGuard]},
  {path: 'proveedores/agregar', component: AgregarProveedorComponent, canActivate: [AuthGuard]},
  {path: 'proveedores/editar/:id', component: EditarProveedorComponent, canActivate: [AuthGuard]},
  {path: 'compras', component: ComprasComponent, canActivate: [AuthGuard]},
  {path: 'compras/nuevaCompra', component: NuevaCompraComponent, canActivate: [AuthGuard]},
  {path: 'compras/comprobante/:id', component: ComprobanteCompraComponent, canActivate: [AuthGuard]},
  {path: 'ventas', component: VentasComponent, canActivate: [AuthGuard]},
  {path: 'ventas/nuevaVenta', component: NuevaVentaComponent, canActivate: [AuthGuard]},
  {path: 'ventas/factura/:id', component: FacturaVentaComponent, canActivate: [AuthGuard]},
  {path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard]},
  {path: 'usuarios', component: UsuariosComponent, canActivate: [AuthGuard]},
  {path: 'usuarios/agregar', component: AgregarUsuarioComponent, canActivate: [AuthGuard]},
  {path: 'usuarios/editar/:id', component: EditarUsuarioComponent, canActivate: [AuthGuard]},
  {path: 'empresa', component: EmpresaComponent, canActivate: [AuthGuard]},
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
