# MiniMercado SaaS API 🛒

Uma API robusta para gerenciamento de PDV (Ponto de Venda) e controle de estoque, desenvolvida com foco em escalabilidade, desacoplamento e boas práticas de arquitetura.

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, dividido nas seguintes camadas:

- **MiniMercadoSaas.API**: Ponto de entrada da aplicação, contendo os Controllers, configurações de JWT e registros de serviços.
- **MiniMercadoSaas.Application**: Contém a lógica de negócio, interfaces de serviço, DTOs (Data Transfer Objects) e validadores (FluentValidation).
- **MiniMercadoSaas.Domain**: O coração do projeto. Contém entidades, enums, contratos de eventos e interfaces de repositório.
- **MiniMercadoSaas.Infrastructure**: Implementação de acesso a dados (EF Core), repositórios, contexto do banco de dados e consumidores de mensageria.

## 🚀 Tecnologias Utilizadas

- **.NET 9** (C#)
- **Entity Framework Core**: ORM para persistência de dados.
- **MySQL**: Banco de dados relacional.
- **MassTransit**: Abstração para mensageria distribuída.
- **RabbitMQ**: Message Broker para processamento assíncrono (suporta fallback para *InMemory*).
- **JWT (JSON Web Token)**: Autenticação e autorização segura.
- **FluentValidation**: Validação de dados de entrada.
- **Swagger/OpenAPI**: Documentação interativa da API.

## 🛠️ Funcionalidades Principais

### 1. Gestão de Vendas (PDV)
- Abertura de vendas vinculadas ao operador.
- Adição dinâmica de itens com cálculo automático de subtotal e total.
- Finalização de venda com baixa automática no estoque.
- Cancelamento de venda com estorno de estoque (restrito a Gerentes/Admin).

### 2. Controle de Estoque Proativo
- Rastreamento completo de movimentações (Entrada, Saída por Venda, Devolução).
- **Mensageria de Alerta**: Sistema que dispara notificações automáticas quando um produto atinge o estoque mínimo.

### 3. Segurança
- Autenticação via JWT.
- Autorização baseada em Roles (Operador, Gerente, Admin).

## 📡 Mensageria e Eventos

O sistema utiliza um padrão de **Event-Driven Architecture** para alertas de estoque:

1. Quando uma venda é finalizada, o `VendaService` verifica se o nível do produto caiu abaixo do mínimo.
2. Caso positivo, um evento `EstoqueBaixoEvent` é publicado via **MassTransit**.
3. O `EstoqueBaixoConsumer` captura essa mensagem de forma assíncrona para processar o alerta (atualmente via log de console, podendo ser expandido para e-mail ou push notifications).

## ⚙️ Como Executar

### Pré-requisitos
- .NET 9 SDK
- MySQL Server

### Configuração
1. Clone o repositório.
2. No arquivo `appsettings.json` da API, configure a sua string de conexão em `DefaultConnection`.
3. (Opcional) Para usar RabbitMQ real, instale via Docker:
   ```bash
   docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
   ```
4. Execute as migrations para criar o banco de dados:
   ```bash
   dotnet ef database update --project MiniMercadoSaas.Infrastructure --startup-project MiniMercadoSaas.API
   ```
5. Inicie a aplicação:
   ```bash
   dotnet run --project MiniMercadoSaas.API
   ```

---
Desenvolvido como um exemplo de sistema SaaS moderno e desacoplado.
