import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogService } from '../../services/log';
import { finalize } from 'rxjs/operators';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-log-form',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './log-form.html',
  styleUrls: ['./log-form.scss']
})
export class LogFormComponent {
  enviando = false;
  mensagem = '';

  constructor(private logService: LogService, private cdr: ChangeDetectorRef) {}

  onArquivoSelecionado(event: any): void {
    const arquivo: File = event.target.files[0];
    if (!arquivo) return;

    this.enviando = true;
    this.mensagem = `Lendo arquivo: ${arquivo.name}...`;

    const leitor = new FileReader();

    leitor.onload = (e) => {
      const conteudo = e.target?.result as string;
      this.mensagem = 'Enviando para a API...';
      this.cdr.detectChanges();

      this.logService.enviarLog(arquivo.name, conteudo)
        .pipe(
          // Garante reset do estado de envio tanto em sucesso quanto em falha.
          finalize(() => {
            this.enviando = false;
            this.cdr.detectChanges();
          }) 
        )
        .subscribe({
          next: (resposta) => {
            this.mensagem = '✅ Arquivo processado e salvo no banco com sucesso!';
            // Permite reenvio do mesmo arquivo após processamento concluído.
            event.target.value = '';
            this.logService.nottificarLogAtualizacao();            
          },
          error: (erro) => {
            this.mensagem = '❌ Erro ao enviar arquivo.';
            console.error('Detalhes do erro:', erro);
          }
        });
    };

    leitor.readAsText(arquivo);
  }
}