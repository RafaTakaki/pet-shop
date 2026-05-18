import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterByTipo'
})
export class FilterByTipoPipe implements PipeTransform {
  transform(pets: any[], tipo: string): any[] {
    if (!pets || !tipo) {
      return pets; // Retorna todos os pets se não houver filtro
    }
    const normalize = (v: string) => {
      const s = (v || '').toLowerCase();
      if (s.includes('cachorro') || s.includes('dog')) return 'CACHORRO';
      if (s.includes('gato') || s.includes('cat')) return 'GATO';
      return s.toUpperCase();
    };
    const filtro = normalize(tipo);
    return pets.filter(pet => normalize(pet.tipoPet) === filtro);
  }
}
