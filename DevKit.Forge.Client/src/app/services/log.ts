import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

export interface AnaliseLogDto {
  id: string;
  nomeArquivo: string;
  dataProcessamento: string;
  sucesso: boolean;
  qtdErros: number;
  qtdAvisos: number;
}

@Injectable({
  providedIn: 'root'
})
export class LogService {
  private apiUrl = 'http://localhost:5065/api/Logs';

  private logAtualizadoSource = new Subject<void>();

  public logAtualizado$ = this.logAtualizadoSource.asObservable();

  constructor(private http: HttpClient) { }

  obterLogs(): Observable<AnaliseLogDto[]> {
    return this.http.get<AnaliseLogDto[]>(this.apiUrl);
  }

  enviarLog(nomeArquivo: string, conteudoArquivo: string): Observable<any> {
    const comando = {
      nomeArquivo: nomeArquivo,
      conteudoArquivo: conteudoArquivo
    };

    return this.http.post(`${this.apiUrl}/processar`, comando);
  }

  nottificarLogAtualizacao() {
    this.logAtualizadoSource.next();
  }

  exportarRelatorio(id: string): Observable<Blob> {
    // Passamos o responseType: 'blob' para o Angular entender que virá um arquivo binário, não um JSON comum
    return this.http.get(`${this.apiUrl}/${id}/exportar`, {
      responseType: 'blob'
    });
  }
}
