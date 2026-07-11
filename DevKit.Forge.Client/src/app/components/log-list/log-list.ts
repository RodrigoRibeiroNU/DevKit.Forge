import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { LogService, AnaliseLogDto } from '../../services/log';

@Component({
  selector: 'app-log-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatIconModule],
  templateUrl: './log-list.html',
  styleUrls: ['./log-list.scss']
})
export class LogListComponent implements OnInit {
  colunasExibidas: string[] = ['nomeArquivo', 'dataProcessamento', 'sucesso', 'qtdErros', 'qtdAvisos', 'acoes'];
  
  dadosLogs = new MatTableDataSource<AnaliseLogDto>();

  constructor(private logService: LogService) {}

  ngOnInit(): void {
    this.carregarLogs();

    this.logService.logAtualizado$.subscribe(() => {
      console.log('Notificação recebida. Recarregando logs...');
      this.carregarLogs();
    });
  }

  carregarLogs(): void {
    this.logService.obterLogs().subscribe({
      next: (dados) => {
        this.dadosLogs.data = dados;
      },
      error: (erro) => {
        console.error('Erro ao carregar', erro);
      }
    });
  }

  baixarRelatorio(id: string, nomeArquivoOriginal: string): void {
    this.logService.exportarRelatorio(id).subscribe({
      next: (blob: Blob) => {
        // Cria uma URL temporária na memória do navegador para o arquivo binário
        const url = window.URL.createObjectURL(blob);
        
        // Cria um elemento <a> oculto na árvore do DOM
        const a = document.createElement('a');
        a.href = url;
        a.download = `relatorio-${nomeArquivoOriginal}.json`;
        
        // Simula o clique do usuário para iniciar o download
        document.body.appendChild(a);
        a.click();
        
        // Limpa a memória destruindo a URL temporária e removendo o elemento
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
      },
      error: (err) => {
        console.error('Erro ao exportar o relatório:', err);
        // Aqui você pode colocar um snackbar ou alerta de erro se quiser
      }
    });
  }
}