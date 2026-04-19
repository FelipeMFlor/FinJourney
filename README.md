# FinJourney — Documento de Referência para Colaboradores

Este documento foi criado para apresentar o projeto FinJourney a novos colaboradores, especialmente ao responsável pelo front-end. Contém a visão geral do produto, todas as decisões técnicas tomadas e as instruções para a inteligência artificial que irá auxiliar no desenvolvimento.

---

## O que é o FinJourney

FinJourney é uma aplicação de controle financeiro pessoal com foco em jornada e desenvolvimento pessoal. O nome carrega a ideia central do produto: finanças como uma jornada, não apenas como números.

O usuário não apenas registra receitas e despesas — ele evolui ao longo do uso. O produto foi pensado para ser motivador, acompanhando o crescimento financeiro do usuário ao longo do tempo. Em versões futuras, um sistema de recompensas e gamificação vai reforçar essa identidade de jornada.

O MVP — produto mínimo viável — foca nas funcionalidades essenciais de controle financeiro: cadastro de usuário, gerenciamento de contas, categorias e lançamentos financeiros. A base está sendo construída com arquitetura evolutiva para que as camadas de gamificação e recompensas entrem sem necessidade de reestruturação.

---

## Visão detalhada do produto

O FinJourney permite que o usuário cadastre múltiplas contas financeiras de tipos diferentes — conta corrente, cartão de crédito, investimento e carteira. Cada tipo tem comportamento e campos específicos.

O usuário cria categorias para organizar seus lançamentos — como Salário, Mercado, Transporte — e registra movimentações financeiras de débito ou crédito vinculadas a essas categorias e contas.

Uma regra central do sistema é que nenhum débito pode ser realizado sem saldo disponível na conta de origem. O pagamento de fatura de cartão de crédito também é tratado como uma movimentação entre contas — debita da conta de origem e quita o saldo do cartão.

O produto será acessível via web e mobile através de um front-end em React que consome uma API REST em C# com .NET.

---

## Stack tecnológica

**Back-end**
- Linguagem: C# com .NET 10
- Arquitetura: Modular Monolith com Feature Folders
- Framework: ASP.NET Core — API REST
- ORM: Entity Framework Core
- Banco de dados: PostgreSQL rodando via Docker
- Visualização do banco: pgAdmin via Docker (acessível em localhost:8080)

**Front-end**
- Framework: React
- Tipo: aplicação web que consome a API REST do back-end

---

## Estrutura do repositório

```
FinJourney/                     ← pasta raiz
├── docker-compose.yml          ← sobe PostgreSQL e pgAdmin
├── backend/
│   ├── FinJourney.slnx         ← arquivo da solution .NET
│   └── FinJourney/             ← projeto .NET
│       ├── Features/
│       │   ├── Users/
│       │   ├── Accounts/
│       │   ├── Categories/
│       │   └── Movements/
│       ├── Shared/
│       │   ├── AppDbContext.cs
│       │   └── BaseEntity.cs
│       ├── appsettings.json
│       └── Program.cs
└── frontend/                   ← projeto React (a construir)
```

---

## Estrutura interna de cada feature

Cada feature do back-end contém exatamente quatro arquivos:

```
Accounts/
├── Account.cs              ← entidade (modelo de dados)
├── AccountsController.cs   ← recebe as requisições HTTP
├── AccountsService.cs      ← contém a lógica de negócio
└── AccountsRepository.cs   ← acessa o banco de dados
```

O fluxo de uma requisição sempre segue a mesma ordem: Controller → Service → Repository → banco de dados. O retorno percorre o caminho inverso.

---

## Features do MVP

**Users** — cadastro e autenticação do usuário. No MVP o login valida e-mail e senha sem criptografia e retorna os dados do usuário em JSON. A autenticação real com JWT será implementada após o MVP funcional.

**Accounts** — o usuário cadastra múltiplas contas escolhendo entre quatro tipos: Conta Corrente, Cartão de Crédito, Investimento e Wallet. Cada tipo tem campos específicos conforme abaixo.

| Tipo | Campos específicos |
|---|---|
| Conta Corrente | nenhum além da base |
| Cartão de Crédito | Limite, DiaVencimento, DiaFechamento |
| Investimento | TaxaRendimento, DataVencimento |
| Wallet | nenhum além da base (diferenciais a definir) |

**Categories** — o usuário cadastra categorias com nome, ícone e tipo sugerido (Débito ou Crédito). A categoria sugere o tipo do lançamento mas não o bloqueia — o usuário pode alterar na hora do lançamento para cobrir casos como estornos e reembolsos.

**Movements** — lançamentos financeiros de débito ou crédito. O usuário informa data, descrição, categoria, valor e conta de origem. O pagamento de fatura de cartão também vive nesta feature como uma movimentação entre contas.

---

## Regras de negócio

- Nenhum débito pode ser realizado sem saldo disponível na conta de origem
- Contas do tipo Investimento não permitem movimentação direta no MVP
- O tipo efetivo do lançamento é sempre definido pelo usuário — a categoria apenas sugere
- O pagamento de fatura de cartão debita da conta de origem e quita o saldo devedor do cartão
- Todas as entidades possuem Id em Guid gerado automaticamente, CreatedAt e UpdatedAt

---

## Rotas da API

As rotas seguem o padrão REST simples sem versionamento:

| Método | Rota | Descrição |
|---|---|---|
| POST | /users | Cadastrar usuário |
| POST | /users/login | Autenticar usuário |
| GET | /accounts | Listar contas |
| POST | /accounts | Criar conta |
| GET | /categories | Listar categorias |
| POST | /categories | Criar categoria |
| GET | /movements | Listar movimentações |
| POST | /movements | Criar movimentação |

---

## Decisões técnicas relevantes para o front-end

**Login:** o endpoint de login retorna um objeto JSON com os dados do usuário. Não há token no MVP — o front-end armazena o ID do usuário e envia nas requisições. Isso será substituído por JWT após o MVP.

**Contas:** ao criar uma conta, o front-end deve enviar o tipo da conta (ContaCorrente, CartaoCredito, Investimento, Wallet) e os campos específicos daquele tipo.

**Categorias:** ao exibir o formulário de novo lançamento, o front-end deve pré-selecionar o tipo (Débito ou Crédito) com base no campo TipoSugerido da categoria selecionada, mas permitir que o usuário altere.

**Movimentações:** o front-end deve impedir visualmente que o usuário tente um débito em conta Investimento, mas a validação definitiva é feita no back-end.

---

## Checklist do front-end

### Configuração inicial
- [ ] Criar projeto React dentro da pasta `frontend`
- [ ] Configurar chamadas HTTP para a API (axios ou fetch)
- [ ] Definir estrutura de pastas do projeto React

### Telas do MVP
- [ ] Tela de cadastro de usuário
- [ ] Tela de login
- [ ] Tela de listagem de contas
- [ ] Tela de criação de conta (com campos dinâmicos por tipo)
- [ ] Tela de listagem de categorias
- [ ] Tela de criação de categoria
- [ ] Tela de listagem de movimentações
- [ ] Tela de criação de movimentação (com tipo pré-selecionado pela categoria)
- [ ] Tela de pagamento de fatura de cartão

### Comportamentos esperados
- [ ] Pré-selecionar tipo do lançamento com base na categoria escolhida
- [ ] Exibir campos específicos por tipo de conta no formulário de criação
- [ ] Impedir visualmente débito em conta do tipo Investimento
- [ ] Exibir saldo atual em cada conta listada

---

## Instruções para a inteligência artificial que irá auxiliar no front-end

Você está auxiliando no desenvolvimento do front-end do projeto FinJourney. Leia este documento integralmente antes de qualquer resposta.

**Contexto do projeto:** FinJourney é uma aplicação de controle financeiro pessoal com foco em jornada e desenvolvimento pessoal. O front-end é construído em React e consome uma API REST em C# com .NET.

**Regras de comportamento:**
- Nunca sugira mudanças na API ou nas regras de negócio — essas decisões são do back-end e estão fechadas
- Sempre respeite o contrato das rotas descritas neste documento
- Não adicione bibliotecas ou dependências sem justificativa clara de necessidade
- Quando houver mais de uma abordagem possível, apresente as opções antes de sugerir uma
- O login no MVP não usa token — o front-end armazena o ID do usuário retornado pela API
- A autenticação real com JWT será implementada futuramente — não antecipe essa implementação

**O que esperar da API:**
- Todas as respostas de sucesso retornam os dados da entidade criada ou consultada
- Erros de validação retornam status 400
- Tentativa de débito sem saldo retorna erro — trate isso no front-end com mensagem clara ao usuário
- Os IDs de todas as entidades são Guids — strings no formato "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
