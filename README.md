# 🛠️ DevKit.Forge

[![.NET 10](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-18+-red.svg)](https://angular.dev/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

O **DevKit.Forge** é uma plataforma corporativa de auditoria, processamento e análise inteligente de arquivos de log. Projetado com foco em alta performance e separação clara de responsabilidades, o sistema permite que desenvolvedores e equipes de DevOps realizem o upload de logs pesados, extraiam métricas críticas e monitorem o status de saúde da infraestrutura de forma centralizada, fluida e intuitiva.

---

## 🏗️ Arquitetura e Tecnologias

O ecossistema é arquitetado de forma totalmente desacoplada, dividindo-se em duas camadas principais para garantir máxima escalabilidade:

### 🖥️ Backend (`/DevKit.Forge.Services.Api`)
* **Runtime:** .NET 10.0
* **Padrão Arquitetural:** Domínio Blindado (DDD - Domain-Driven Design) com encapsulamento rigoroso de entidades.
* **Padrão de Mensageria:** CQRS (Command Query Responsibility Segregation) implementado via **MediatR**.
* **Persistência / Acesso a Dados:** Repositórios assíncronos desacoplados (`IAnaliseLogRepository`).
* **Testes de Unidade (`/DevKit.Forge.UnitTests`):** Cobertura completa de Handlers utilizando **xUnit**, **Moq** e **FluentAssertions**. Utiliza técnicas avançadas de *Reflection* (`Activator.CreateInstance`) para testar o comportamento de entidades com setters privados e construtores protegidos sem violar o encapsulamento do domínio.

### 🎨 Frontend (`/DevKit.Forge.Client`)
* **Framework:** Angular (Arquitetura moderna baseada em *Standalone Components*).
* **UI/UX:** **Angular Material 3** com Material Design integrado (Paletas Personalizadas *Azure/Blue*).
* **Layout:** Interface altamente responsiva com efeito de *Cards Flutuantes* (elevação 3D), cantos arredondados, campo de busca minimalista com filtro em memória e pílulas de status coloridas (`badge-chips`).
* **Gerenciamento de Dados:** Paginação de performance em memória acoplada nativamente via `MatPaginator`.

---

## 🚀 Como Executar o Projeto Localmente

### 📋 Pré-requisitos
Antes de começar, certifique-se de ter instalado em sua máquina:
* SDK do [.NET 10.0](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/) (Versão LTS recomendada)
* [Angular CLI](https://angular.dev/tools/cli) instalado globalmente (`npm install -g @angular/cli`)

---

### ⚙️ 1. Configurando e Executando o Backend (.NET)

Abra o terminal na raiz da solução e execute os comandos abaixo:

```bash
# Restaurar os pacotes NuGet de todos os projetos da solução
dotnet restore

# Executar o projeto da Web API
dotnet run --project DevKit.Forge.Services.Api/DevKit.Forge.Services.Api.csproj
```
> 💡 **Nota:** O servidor subirá localmente. Você poderá acessar a documentação interativa dos endpoints através da interface do **Swagger** gerada automaticamente pela API.

#### 🧪 Executando os Testes de Unidade
Para rodar a suite de testes automatizados e validar o cálculo das regras de negócio de severidade de saúde dos logs:
```bash
dotnet test
```

---

### 💻 2. Configurando e Executando o Frontend (Angular)

Abra um segundo terminal na raiz da solução, navegue até a pasta do cliente e inicie o servidor de desenvolvimento:

```bash
# Navegar até o diretório do frontend
cd DevKit.Forge.Client

# Instalar as dependências do ecossistema Node/Angular
npm install

# Iniciar o servidor local com Hot Reload
ng serve
```
> 🌐 **Acesso:** Assim que o build concluir, abra o seu navegador e acesse a aplicação em: `http://localhost:4200`

---

## 📋 Funcionalidades Concluídas e Disponíveis

- [x] **Upload Inteligente Drag & Drop:** Área de upload elegante com linha tracejada interna, ícones descritivos e feedback de estado dinâmico ("Enviando...").
- [x] **Mecanismo de Análise Automatizada:** Classificação imediata e severidade do status do log em categorias (`Saudável`, `Atenção`, `Instável`, `Crítico`) com base no volume de erros e avisos identificados.
- [x] **Tabela Avançada de Histórico:** Grid totalmente customizada no Angular Material, dotada de paginação sob demanda e ordenação visual clara.
- [x] **Filtro Preditivo em Tempo Real:** Input de busca estilizado com ícone de lupa, que filtra dinamicamente por nome do arquivo, volume de erros ou status do histórico diretamente na memória do client.
- [x] **Exportação de Relatórios:** Botão redondo minimalista de download que consome a API e gera dinamicamente arquivos JSON estruturados usando arquivos Blob nativos do navegador.

---

## 🛠️ Estrutura de Pastas Principal

```text
DevKit.Forge/
│
├── DevKit.Forge.Application/       # Regras de Aplicação (Queries, Handlers, DTOs)
├── DevKit.Forge.Domain/            # Núcleo do Domínio (Entidades Blindadas, Interfaces)
├── DevKit.Forge.Services.Api/      # Controllers e Configuração de Entrada da API (.NET 10)
├── DevKit.Forge.UnitTests/         # Testes de Unidade (xUnit + Moq)
│
└── DevKit.Forge.Client/            # Aplicação Frontend (Angular)
    └── src/app/components/
        ├── log-form/               # Componente da Área de Upload (HTML/SCSS/TS)
        └── log-list/               # Componente da Tabela de Histórico (HTML/SCSS/TS)
```

---

## 🗺️ Roadmap (Próximos Passos de Evolução)

O **DevKit.Forge** é um ecossistema corporativo em constante evolução. Para consolidar sua robustez arquitetural e entregar inteligência analítica de dados a nível sênior, as seguintes melhorias, módulos e refatorações estão planejados para as próximas versões do projeto:

### 🧠 Fase 1: Inteligência Artificial & Diagnósticos Analíticos
- [ ] **Módulo de IA Integrado:** Implementação de engenharia de prompts refinada utilizando modelos de linguagem locais (SLMs via Ollama) para análise preditiva, diagnóstico automatizado de causas-raiz de falhas e sugestões automáticas de correção diretamente na interface.
- [ ] **Análise Heurística Avançada:** Implementação de algoritmos de agrupamento baseados em expressões regulares dinâmicas para consolidar milhões de linhas de *Stack Traces* idênticas em incidentes únicos, evitando sobrecarga visual.

### 🗄️ Fase 2: Persistência Robusta & Arquitetura de Dados
- [ ] **Persistência de Dados Relacional:** Integração do Entity Framework Core acoplado ao banco de dados PostgreSQL para armazenar o histórico consolidado de auditorias e o perfil consolidado de métricas por arquivo de log.
- [ ] **Camada de Armazenamento de Arquivos (Blob Storage):** Migração do sistema de arquivos temporários local para uma infraestrutura desacoplada baseada em provedores de nuvem (*Object/Blob Storage* como AWS S3 ou Azure Blob), salvando os arquivos de log originais com total segurança.
- [ ] **Paginação Real no Servidor (Server-side Pagination):** Refatoração da listagem do Angular para que as operações de ordenação, paginação e filtragem preditiva ocorram no nível do banco de dados (API), permitindo o tratamento fluido de milhões de registros históricos.

### 📊 Fase 3: Dashboards de Governança & Exportação Avançada
- [ ] **Painel de Métricas (Dashboard):** Criação de uma central de monitoramento de saúde de infraestrutura no frontend contendo gráficos interativos de alta performance (via Apache ECharts ou Chart.js), ilustrando a evolução temporal de erros e a integridade geral dos sistemas.
- [ ] **Exportação Avançada e Corporativa:** Suporte para novos formatos de relatórios analíticos de auditoria, gerando de forma assíncrona arquivos PDF estruturados e planilhas Excel (.xlsx) altamente estilizadas e formatadas com dados consolidados.

### ⚡ Fase 4: Comunicação Síncrona & Webhooks
- [ ] **Alertas em Tempo Real:** Criação de um barramento de notificações orientadas a eventos para disparar e-mails automatizados e ganchos web (*Webhooks*) customizáveis para plataformas como Slack e Discord sempre que falhas críticas ou instabilidades sejam detectadas.
- [ ] **Streaming de Processamento com WebSockets:** Integração do ASP.NET Core SignalR acoplado ao frontend para fornecer feedback em tempo real (barras de progresso reais linha por linha) durante a leitura assíncrona de arquivos de log pesados que superem a casa dos gigabytes.

---
## 👤 Autor

Desenvolvido por **Rodrigo Ribeiro**. Se gostou do projeto ou quer conversar sobre soluções arquiteturais com .NET e Angular, sinta-se à vontade para entrar em contacto!

* **E-mail:** rodrigong@gmail.com
* **LinkedIn:** [Conecte-se comigo no LinkedIn](https://linkedin.com/in/SEU-LINKEDIN-AQUI)

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.