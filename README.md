# 🎮 Royal Games API

API desenvolvida em ASP.NET para gerenciamento de um catálogo online de games, permitindo cadastrar, consultar, atualizar e remover jogos de forma estruturada seguindo boas práticas de arquitetura.

O projeto utiliza separação em camadas como Controllers, Domains, DTOs e Repositories, garantindo organização, escalabilidade e facilidade de manutenção.

📌 Sobre o Projeto

O Royal Games é um sistema de catálogo de jogos que disponibiliza uma API REST para gerenciamento de informações de games.

A API permite:

📚 Consultar jogos disponíveis no catálogo

➕ Cadastrar novos jogos

✏️ Atualizar informações de jogos

❌ Remover jogos

📦 Organizar os dados utilizando arquitetura em camadas

Este projeto foi desenvolvido utilizando boas práticas de desenvolvimento em .NET, como separação de responsabilidades e uso de DTOs.

# 🏗 Arquitetura do Projeto

O projeto segue uma arquitetura organizada por responsabilidades:

Royal_Games
│
├── Applications     → Regras de aplicação e serviços
├── Contexts         → Configuração de banco de dados
├── Controllers      → Endpoints da API
├── DTOs             → Objetos de transferência de dados
├── Domains          → Entidades e regras de negócio
├── Exceptions       → Tratamento de exceções
├── Interfaces       → Contratos da aplicação
├── Properties       → Configurações do projeto
├── Repositories     → Acesso e manipulação de dados
│
├── Program.cs
├── appsettings.json
└── Royal_Games.csproj

Essa estrutura permite:

melhor organização do código

maior facilidade de manutenção

separação clara entre regras de negócio e infraestrutura

# ⚙️ Tecnologias Utilizadas

C#

ASP.NET Core

.NET

REST API

Entity FrameWork
