import { Direccion } from "./Direccion";

export interface Cliente {
    nombre: string;
    apellido: string;
    email: string;
    cedula: string;
    telefono: string;
    direccion: Direccion
}