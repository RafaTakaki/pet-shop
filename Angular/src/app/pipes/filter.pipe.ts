import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterByTipo'
})
export class FilterByTipoPipe implements PipeTransform {
  transform(pets: any[], tipo: string): any[] {
    if (!pets || !tipo) {
      return pets; // Retorna todos os pets se não houver filtro
    }
    return pets.filter(pet => pet.tipoPet === tipo); // Filtra pets por tipoPet
  }
}
