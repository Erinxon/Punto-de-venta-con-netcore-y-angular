export class ValidarCedula {
    
    isValida(cedula: string): boolean{
        cedula = this.formatCedula(cedula);
        return (this.getVerificador(cedula) == this.getUltimoDigito(cedula) && 
        this.validateLongitud(cedula));
    }

    private validateLongitud(cedula: string): boolean {
        return cedula.length === 11 ? true : false
    }
    
    private getVerificador(cedula: string): number {
        let sumDigitos, ultimoDigito, decenaMasCercana, concatenateDigitos = ''
        const digitosCedulas = Array.from(cedula)
        const DigitosMultiplicadores = [1, 2, 1, 2, 1, 2, 1, 2, 1, 2]
        for (let i = 0; i < 10; i++) {
            let digito = + digitosCedulas[i] * DigitosMultiplicadores[i]
            concatenateDigitos += digito
        }
        sumDigitos = this.getSumDigitos(concatenateDigitos);
        ultimoDigito = this.getUltimoDigito(sumDigitos.toString())
        decenaMasCercana = sumDigitos + (10 - ultimoDigito)
        return Math.abs(sumDigitos - decenaMasCercana)
    }
    
    private getSumDigitos(listDigitos: string): number {
        let sum = 0;
        let lista = Array.from(listDigitos)
        lista.forEach(digito => {
            sum += Number.parseInt(digito)
        });
        return sum
    }
    
    private getUltimoDigito(text: string):number {
        return Number.parseInt(text.charAt(text.length - 1))
    }
    
    private formatCedula(cedula: string): any {
        let newCedula;
        for(let i=0; i<=cedula.length;i++){
            cedula = cedula.replace('-','');
            newCedula = cedula
        }
        return newCedula
    }
 

}

