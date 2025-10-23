# Projeto StorageApp - API REST

## Introdução

O **Projeto StorageApp** é uma API REST para gerenciamento completo de estoque, pedidos e controle de usuários.  
A aplicação permite que usuários gerenciem produtos, marcas, categorias e pedidos, com diferentes níveis de acesso e permissões hierárquicas.

---

## Objetivo do Sistema

A API oferece funcionalidades de gerenciamento de estoque, pedidos e usuários, com controle de acesso baseado em níveis (*Member*, *Manager* e *Admin*).  
Principais objetivos:

- Cadastro e autenticação de usuários com segurança via JWT.
- Autenticação via **JWT**, garantindo segurança nas requisições através de microsserviço dedicado.
- Gestão completa de produtos, marcas e categorias (CRUD).
- Controle de pedidos com rastreabilidade completa.
- Diferenciação de permissões: membros visualizam dados limitados; gerentes controlam operações; administradores têm acesso total.
- Observabilidade completa com **New Relic** e logs estruturados com **Serilog**.

---

## Tecnologias Utilizadas

- **.NET**: framework robusto para desenvolvimento de aplicações enterprise.  
- **Entity Framework**: ORM para mapeamento objeto-relacional e gerenciamento de banco de dados.  
- **SQL Server**: banco de dados relacional para armazenamento persistente.  
- **JWT (JSON Web Token)**: autenticação stateless baseada em tokens.  
- **Bcrypt**: hash de senhas para armazenamento seguro.  
- **Swagger / OpenAPI**: documentação interativa da API.  
- **Docker + Docker Compose**: containerização e orquestração de serviços.  
- **New Relic**: plataforma de observabilidade para monitoramento e performance.  
- **Serilog**: biblioteca de logging estruturado para rastreamento de eventos.

---

## Estrutura do Projeto

O projeto adota uma **arquitetura em camadas**, com separação entre **backend** e **frontend**:

- **Backend**: lógica de negócio, persistência de dados e API.  
- **Frontend**: interface do usuário e experiência de navegação.

### Arquitetura de Microsserviços

- **Microsserviço Principal (StorageApp)**: gerenciamento de estoque, produtos, pedidos, marcas e categorias.
- **Microsserviço de Usuário**: autenticação JWT e gerenciamento de usuários isolado.

---

## Funcionalidades Principais

- **Cadastro de Usuário**: validação e registro de novos usuários.  
- **Autenticação JWT**: login seguro com token para requisições subsequentes via microsserviço dedicado.  
- **Níveis de Acesso**: *Member* (usuário padrão), *Manager* (gerente) e *Admin* (administrador).  
- **Gerenciamento de Produtos**: CRUD completo com controle de estoque.  
- **Gerenciamento de Marcas**: organização de produtos por fabricante.  
- **Gerenciamento de Categorias**: classificação e agrupamento de produtos.  
- **Gerenciamento de Pedidos**: criação, acompanhamento e histórico de pedidos.  
- **Gerenciamento de Usuários**: criação e atualização de usuários por administradores.  
- **Observabilidade**: monitoramento com New Relic e logs estruturados com Serilog.

---

## Endpoints Disponíveis

### 🔐 Autenticação (Microsserviço de Usuário)

| Método | Endpoint     | Descrição                | Request Body             | Respostas     | Permissão |
|--------|--------------|--------------------------|--------------------------|---------------|-----------|
| **POST**   | `/register`  | Cadastra um novo usuário | `name, email, password`  | `201`, `400`  | Público   |
| **POST**   | `/login`     | Autentica usuário        | `email, password`        | `200`, `400`  | Público   |

---

### 📦 Produtos

| Método | Endpoint         | Descrição                  | Request Body                         | Respostas                          | Permissão |
|--------|------------------|-----------------------------|--------------------------------------|------------------------------------|-----------|
| **GET**    | `/products`      | Retorna todos os produtos   | —                                    | `200`, `401`                       | Member+   |
| **GET**    | `/products/{id}` | Retorna um produto pelo ID  | —                                    | `200`, `400`, `401`, `404`         | Member+   |
| **POST**   | `/products`      | Cria um novo produto        | `name, quantity, description, price, brandId, categoryId` | `201`, `400`, `401`, `403` | Manager+  |
| **PUT**    | `/products/{id}` | Atualiza um produto pelo ID | `name, quantity, description, price, brandId, categoryId` | `200`, `400`, `401`, `403`, `404` | Manager+  |
| **DELETE** | `/products/{id}` | Deleta um produto pelo ID   | —                                    | `200`, `401`, `403`, `404`         | Admin     |

---

### 🏷️ Marcas (Brands)

| Método | Endpoint       | Descrição                | Request Body    | Respostas                          | Permissão |
|--------|----------------|--------------------------|-----------------|------------------------------------|-----------| 
| **GET**    | `/brands`      | Retorna todas as marcas  | —               | `200`, `401`                       | Member+   |
| **GET**    | `/brands/{id}` | Retorna uma marca pelo ID| —               | `200`, `400`, `401`, `404`         | Member+   |
| **POST**   | `/brands`      | Cria uma nova marca      | `name`          | `201`, `400`, `401`, `403`         | Manager+  |
| **PUT**    | `/brands/{id}` | Atualiza uma marca pelo ID| `name`         | `200`, `400`, `401`, `403`, `404`  | Manager+  |
| **DELETE** | `/brands/{id}` | Deleta uma marca pelo ID | —               | `200`, `401`, `403`, `404`         | Admin     |

---

### 📑 Categorias (Categories)

| Método | Endpoint           | Descrição                    | Request Body    | Respostas                          | Permissão |
|--------|--------------------|------------------------------|-----------------|------------------------------------|-----------| 
| **GET**    | `/categories`      | Retorna todas as categorias  | —               | `200`, `401`                       | Member+   |
| **GET**    | `/categories/{id}` | Retorna uma categoria pelo ID| —               | `200`, `400`, `401`, `404`         | Member+   |
| **POST**   | `/categories`      | Cria uma nova categoria      | `name`          | `201`, `400`, `401`, `403`         | Manager+  |
| **PUT**    | `/categories/{id}` | Atualiza uma categoria pelo ID| `name`         | `200`, `400`, `401`, `403`, `404`  | Manager+  |
| **DELETE** | `/categories/{id}` | Deleta uma categoria pelo ID | —               | `200`, `401`, `403`, `404`         | Admin     |

---

### 🛒 Pedidos (Orders)

| Método | Endpoint       | Descrição                 | Request Body                  | Respostas                     | Permissão |
|--------|----------------|---------------------------|-------------------------------|-------------------------------|-----------|
| **GET**    | `/orders`      | Retorna todos os pedidos  | —                             | `200`, `401`                  | Member+   |
| **GET**    | `/orders/{id}` | Retorna um pedido pelo ID | —                             | `200`, `400`, `401`, `404`    | Member+   |
| **POST**   | `/orders`      | Cria um novo pedido       | `productId, quantity, date?`  | `201`, `400`, `401`           | Member+   |
| **PUT**    | `/orders/{id}` | Atualiza um pedido pelo ID| `productId, quantity, status` | `200`, `400`, `401`, `403`, `404` | Manager+  |
| **DELETE** | `/orders/{id}` | Deleta um pedido pelo ID  | —                             | `200`, `401`, `403`, `404`    | Admin     |

---

### 👤 Usuários (Users)

| Método | Endpoint        | Descrição                   | Request Body                  | Respostas                          | Permissão |
|--------|-----------------|------------------------------|-------------------------------|------------------------------------|-----------|
| **GET**    | `/users`        | Retorna todos os usuários    | —                             | `200`, `401`, `403`                | Admin     |
| **GET**    | `/users/{id}`   | Retorna um usuário pelo ID   | —                             | `200`, `401`, `403`, `404`         | Admin     |
| **POST**   | `/users`        | Cria um novo usuário         | `name, email, password, role` | `201`, `400`, `401`, `403`         | Admin     |
| **PUT**    | `/users/{id}`   | Atualiza um usuário pelo ID  | `name, email, password, role` | `200`, `400`, `401`, `403`, `404`  | Admin     |
| **DELETE** | `/users/{id}`   | Deleta um usuário pelo ID    | —                             | `200`, `401`, `403`, `404`         | Admin     |

---

### Autenticação pelo Swagger

- Para autenticar, use o endpoint **`POST /login`** do microsserviço de usuário informando e-mail/senha cadastrados. O servidor retornará um JWT (token) de autenticação.
- Na interface do **Swagger UI** (`http://localhost:5000/swagger`), clique em **"Authorize"** e cole o token JWT completo no formato `Bearer <token>` no campo de autorização.
- Em seguida, você pode testar todos os endpoints diretamente pelo Swagger: selecione um endpoint, clique em "Try it out" e veja as respostas da API.

### Níveis de Permissão

- **Member**: Acesso de leitura a produtos, marcas, categorias e pedidos. Pode criar pedidos.
- **Manager**: Todas as permissões de Member + criar/editar produtos, marcas, categorias e pedidos.
- **Admin**: Acesso total ao sistema, incluindo gerenciamento de usuários e exclusão de registros.

---

## Observabilidade e Logs

### New Relic

O projeto está integrado com **New Relic** para monitoramento de:
- Performance da API
- Métricas de requisições
- Erros e exceções
- Tempo de resposta
- Uso de recursos

Acesse o dashboard do New Relic para visualizar métricas em tempo real.

### Serilog

Todos os eventos da aplicação são registrados com **Serilog**, incluindo:
- Requisições HTTP
- Erros e exceções
- Operações de banco de dados
- Eventos de autenticação

Os logs são estruturados e podem ser consultados para troubleshooting e auditoria.

---

## Documentação

- **Swagger / OpenAPI:** Toda a API está documentada em Swagger, disponível em `/swagger`. A especificação OpenAPI lista os endpoints, modelos de dados e métodos suportados, permitindo explorar e testar cada rota interativamente.
- **Autenticação JWT:** Todas as rotas protegidas requerem token JWT válido no header `Authorization: Bearer <token>`.

---

## Estrutura de Dados

### Principais Entidades

- **User**: Usuários do sistema com diferentes níveis de acesso
- **Product**: Produtos do estoque com quantidade, preço e descrição
- **Brand**: Marcas/fabricantes dos produtos
- **Category**: Categorias para classificação de produtos
- **Order**: Pedidos realizados no sistema

---

## Suporte e Contribuição

Para reportar bugs ou sugerir melhorias, abra uma issue no repositório do projeto.

---

## Licença

Este projeto está sob a licença MIT. Consulte o arquivo LICENSE para mais detalhes.
