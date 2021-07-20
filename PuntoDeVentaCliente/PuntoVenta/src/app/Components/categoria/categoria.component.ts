import { Component, OnInit } from '@angular/core';
import { CategoriaService } from 'src/app/Services/categoria-service';
import {NgxPaginationModule} from 'ngx-pagination'; 

@Component({
  selector: 'app-categoria',
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.css']
})
export class CategoriaComponent implements OnInit {
  categorias: any;
  loanding: boolean = true;
  filters: string = '';
  p: number = 1;

  constructor(private categoriaService: CategoriaService) { 
  }

  ngOnInit(): void {
    this.getCategoria();
  }

  getCategoria(): void {
     this.categoriaService.getCategoria().subscribe(c => {
        this.categorias = c.data;
        this.loanding = false;
        
     })
  }

  filtrar(): void {
    if(this.filters){
       this.loanding = true;
       this.categoriaService.getFilterCategoria(this.filters).subscribe( c => {
          this.categorias = c.data;
          this.loanding = false;
       })
    }
  }

  clearFilter(): void {
      this.filters = '';
      this.getCategoria();
  }

  updateEstatusCategoria(id: number): void {
     this.categoriaService.updateEstatusCategoria(id).subscribe(c => {
        if(c.success){
           this.getCategoria();
        }
     })
  }

}
