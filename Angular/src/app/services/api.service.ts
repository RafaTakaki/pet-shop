import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { forkJoin, Observable, of, switchMap, map } from 'rxjs';
import { CadastroPet } from '../models/cadastro-pet';
import { Agendamento } from '../models/agendamentos';
import { Notificacao } from '../models/notificacao';
import { API_BASE_URL } from '../api.config';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = API_BASE_URL;
  private apiUrlCachorro = `${API_BASE_URL}/Pet/listarRacaCachorros`;
  private apiUrlGato = `${API_BASE_URL}/Pet/listarRacaGatos`;

  constructor(private http: HttpClient) { }

  sendEmail(email: string): Observable<string> {
    const url = `${this.apiUrl}/Usuario/RecuperarSenha`;
    return this.http.get(url, {
      params: { email },
      responseType: 'text'
    });
  }

  cadastro(email: string, senha: string, nome: string): Observable<string> {
    const url = `${this.apiUrl}/Usuario/CriarUsuario`;

    const body = {
      Nome: nome,
      Email: email,
      Senha: senha
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(url, body, { headers, responseType: 'text' });
  }

  cadastroPet(
    nomePet: string,
    tipoPet: string,
    idadePet: number,
    raca: string,
    sexo: string
  ): Observable<string> {
    const url = `${this.apiUrl}/Pet/cadastro`;

    const body = {
      NomePet: nomePet,
      TipoPet: tipoPet.toUpperCase(),
      IdadePet: idadePet,
      Raca: raca,
      Sexo: sexo
    };

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<string>(url, body, { headers: headers, responseType: 'text' as 'json' });
  }

buscaRacas(tipoPet: string): Observable<string[]> {
  const t = (tipoPet || '').toLowerCase();
  
  // CORREÇÃO: Comparando com strings totalmente em minúsculo
  const isDog = t.includes('cachorro') || t.includes('dog') || t.includes('cão');
  const isCat = t.includes('gato') || t.includes('cat');
  
  const url = isDog ? this.apiUrlCachorro : this.apiUrlGato;
  
  console.log('buscaRacas - URL:', url, 'isDog:', isDog, 'isCat:', isCat);
  
  return this.http.get<any>(url).pipe(
    map(response => {
      console.log('Response completa recebida do backend:', response);
      
      let racas: string[] = [];
      
      if (isDog) {
        racas = response?.racaCachorros || response?.RacaCachorros || [];
      } else if (isCat) {
        racas = response?.racaGatos || response?.RacaGatos || [];
      } else {
        // Caso o usuário digite algo diferente, tenta buscar em ambas propriedades por segurança
        racas = response?.racaCachorros || response?.racaGatos || [];
      }
      
      console.log('Array final mapeado e retornado:', racas);
      return racas;
    })
  );
}

  buscapets(): Observable<CadastroPet[]> {
    const url = `${this.apiUrl}/Cuidado/PetsCadastrados`;
    return this.http.get<string[]>(url).pipe(
      switchMap(names => {
        if (!names || names.length === 0) {
          return of([] as CadastroPet[]);
        }
        return forkJoin(
          names.map(name => this.http.get<CadastroPet>(`${this.apiUrl}/Cuidado/BuscaPetPorNome`, { params: new HttpParams().set('nome', name) }))
        );
      })
    );
  }

  cadastroAgendamento(nomePet: string, servico: string, data: string, observacao: string): Observable<string> {
    const url = `${this.apiUrl}/Cuidado/CadastrarServico`;
    const params = new HttpParams().set('nomePet', nomePet);
    const body = {
      TipoServico: servico,
      DataServico: data,
      Observacao: observacao
    };

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<string>(url, body, { headers, params });
  }

  buscaNomePetsToken(): Observable<string[]> {
    const url = `${this.apiUrl}/Cuidado/PetsCadastrados`;
    return this.http.get<string[]>(url);
  }

  buscaAgendamentosPorPet(nomePet: string): Observable<Agendamento[]> {
    const url = `${this.apiUrl}/Cuidado/ListarAgendamentosPorPet`;
    const params = new HttpParams().set('nomePet', nomePet);
    return this.http.get<Agendamento[]>(url, { params });
  }

  excluirPet(id: string): Observable<void> {
    const url = `${this.apiUrl}/Pet/${id}`;
    return this.http.delete<void>(url);
  }

  excluirAgendamento(idAgendamento: string): Observable<Agendamento[]> {
    console.warn('Excluir agendamento não está disponível no backend atual.');
    return of([] as Agendamento[]);
  }

  buscarNotificacoes(): Observable<Notificacao[]> {
    console.warn('A API de notificações não está disponível no backend atual.');
    return of([] as Notificacao[]);
  }

  numeroNotificacoes(): Observable<number> {
    return of(0);
  }
}
