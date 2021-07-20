import { Direccion } from "./Direccion";

export interface EditClienteModel {
    id: number;
    nombre: string;
    apellido: string;
    email: string;
    cedula: string;
    telefono: string;
    idCategoria: number;
    FechaCreado: Date;
    estatus: boolean;
}