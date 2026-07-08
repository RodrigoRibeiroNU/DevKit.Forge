# Diretrizes de Arquitetura e Dados - DevKit.Forge

## 1. Princípios de Design
* **Clean Architecture:** Camadas logicamente separadas. O Domínio é o núcleo rico e isolado.
* **CQRS:** Comandos (escrita) e Consultas (leitura) rodam em fluxos totalmente segregados.

## 2. Regras do Entity Framework Core
* Mapeamentos explícitos via Fluent API (arquivos `.Mapping`).
* Consultas de leitura devem obrigatoriamente utilizar `.AsNoTracking()` para otimização de memória.