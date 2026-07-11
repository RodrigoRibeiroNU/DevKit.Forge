# 🤝 Guia de Contribuição - DevKit.Forge

Em primeiro lugar, obrigado por considerar contribuir para o **DevKit.Forge**! É a comunidade e a colaboração que fazem projetos de software alcançarem a excelência.

Este documento estabelece as diretrizes para garantir que o processo de contribuição seja fluido, organizado e mantenha o alto padrão de engenharia do projeto.

---

## 🛠️ Entendendo a Arquitetura

Antes de submeter código, é fundamental entender o ecossistema que estamos construindo:

* **Backend (.NET 10):** Utilizamos *Domain-Driven Design (DDD)* e o padrão de mensageria *CQRS* via MediatR. Todo novo caso de uso deve ser encapsulado em seu respectivo Handler e coberto por testes unitários usando *xUnit* e *Moq*.
* **Frontend (Angular 18+):** A interface é baseada em *Standalone Components* e utiliza o Angular Material M3. O design system exige o uso de bordas arredondadas (pílulas), sombras sutis para simular elevação (cards flutuantes) e o mínimo uso de componentes pesados, favorecendo performance.

---

## 🐞 Como Reportar Bugs

Se você encontrou um erro no processamento dos logs ou na interface, abra uma *Issue* no repositório seguindo este padrão:

1. **Título Claro:** Descreva o bug de forma concisa.
2. **Passos para Reproduzir:** Explique o que você fez até o erro acontecer.
3. **Comportamento Esperado:** O que deveria ter acontecido.
4. **Comportamento Atual:** O que realmente aconteceu (inclua prints, se aplicável).
5. **Ambiente:** Versão do Node.js, .NET SDK e navegador.

---

## ✨ Como Propor Novas Funcionalidades (Features)

Queremos evoluir o DevKit.Forge! Para propor uma nova feature:
1. Abra uma *Issue* com a tag `enhancement`.
2. Descreva o problema que essa nova funcionalidade resolve.
3. Sugira uma possível solução técnica, arquitetura ou rascunho de tela.

---

## 💻 Processo de Desenvolvimento e Pull Requests

Se você vai colocar a mão no código, siga este fluxo de trabalho profissional:

### 1. Preparação
Faça um *fork* do repositório, clone-o localmente e crie uma branch a partir da `main`.
```bash
git checkout -b feature/nome-da-sua-feature
# ou
git checkout -b fix/nome-do-bug
```

### 2. Padrão de Commits (Conventional Commits)
Nosso histórico de versionamento precisa ser limpo e semântico. Use os prefixos abaixo em seus commits:
* `feat:` Uma nova funcionalidade.
* `fix:` Correção de um bug.
* `docs:` Alterações na documentação (README, CONTRIBUTING, etc).
* `style:` Formatação de código ou alterações visuais de CSS/SCSS (não afeta regras de negócio).
* `refactor:` Mudança de código que não corrige bug nem adiciona feature.
* `test:` Adição ou correção de testes automatizados (xUnit).
* `build:` Alterações que afetam o sistema de build ou dependências (npm, NuGet).
* `ci:` Mudanças nos arquivos de configuração e scripts de CI (GitHub Actions).

*Exemplo:* `feat(api): add endpoint to export log metrics as CSV`

### 3. Validação Local Obrigatória
Antes de abrir o Pull Request, certifique-se de que a esteira vai passar:
* O projeto backend compila sem avisos (`dotnet build`).
* Todos os testes de unidade passam (`dotnet test`).
* O frontend compila para produção sem erros de dependência (`npm run build`).

### 4. Abrindo o Pull Request
* Envie a sua branch para o seu *fork*.
* Abra o Pull Request apontando para a branch `main` do repositório original.
* Aguarde a validação da pipeline de CI (GitHub Actions). Se a pipeline quebrar, o PR não será mesclado.
* Um revisor irá analisar seu código, sugerir melhorias se necessário e aprová-lo.

---

Obrigado por ajudar a construir o **DevKit.Forge**! 🚀