using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Dtos.ProductDto;
using WebApiPuntoVenta.Dtos.CategoriaDto;
using WebApiPuntoVenta.Dtos.ClienteDto;
using WebApiPuntoVenta.Dtos.DireccionDto;
using WebApiPuntoVenta.Dtos.ProveedorDto;
using WebApiPuntoVenta.Dtos.VentasDto;
using WebApiPuntoVenta.Dtos.ComprasDto;
using WebApiPuntoVenta.Dtos.UsuarioDto;
using WebApiPuntoVenta.Dtos.EmpresaDto;

namespace WebApiPuntoVenta
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            //Categorias
            CreateMap<Categoria, GetCategoriaDto>();
            CreateMap<AddCategoriaDto, Categoria>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            //Productos
            CreateMap<Producto, GetProductosDto>();
            CreateMap<AddProductoDto, Producto>();
            CreateMap<UpdateProductoDto, Producto>();
            //Direcciones
            CreateMap<Direccion, AddDireccionDto>();
            CreateMap<GetDireccionDto, Direccion>().ReverseMap();
            //Clientes
            CreateMap<Cliente, GetClienteDto>();
            CreateMap<AddClienteDto, Cliente>();
            CreateMap<UpdateClienteDto, Cliente>();
            //Proveedores
            CreateMap<Proveedore, GetProveedorDto>();
            CreateMap<AddProveedorDto, Proveedore>();
            CreateMap<UpdateProveedorDto, Proveedore>();
            //Ventas
            CreateMap<Venta, GetVentasDto>();
            CreateMap<GetDetalleVentasDto, DetalleVenta>().ReverseMap();
            CreateMap<AddVentasDto, Venta>();
            CreateMap<DetalleVenta, AddDetalleVentasDto>().ReverseMap();   
            //Compras
            CreateMap<Compra, GetCompraDto>();
            CreateMap<GetDetalleCompraDto, DetalleCompra>().ReverseMap();
            CreateMap<AddCompraDto, Compra>();
            CreateMap<DetalleCompra, AddDetalleCompraDto>().ReverseMap();   
            //Usuarios
            CreateMap<Usuario, GetUsuarioDto>();
            CreateMap<UpdateUsuarioDto, Usuario>();
            CreateMap<AddUsuarioDto, Usuario>();
            //Empresa
            CreateMap<Empresa, GetEmpresaDto>();
            CreateMap<UpdateEmpresaDto, Empresa>();
            CreateMap<AddEmpresaDto, Empresa>();

        }
    }
}
