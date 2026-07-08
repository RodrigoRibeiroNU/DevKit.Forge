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
  colunasExibidas: string[] = ['nomeArquivo', 'dataProcessamento', 'sucesso', 'qtdErros', 'qtdAvisos'];
  
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
}