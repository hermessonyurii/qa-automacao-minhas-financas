# Teste Técnico - Analista de Qualidade de Software

## Visão geral

Este repositório contém a estratégia, documentação e implementação dos testes realizados para o sistema **Minhas Finanças**, uma aplicação de controle de gastos residenciais.

O objetivo desta entrega foi validar as principais regras de negócio do sistema por meio de testes manuais, testes automatizados de back-end, testes de front-end e testes E2E.

A aplicação original foi utilizada apenas como base de execução e análise. O código-fonte da aplicação não foi alterado e não está incluído neste repositório.

Durante o processo, utilizei ferramentas de IA como apoio para melhorar a organização da documentação, revisar a escrita dos relatos e auxiliar em dúvidas pontuais de configuração. A execução dos testes, análise dos comportamentos, identificação dos bugs e decisões sobre os cenários foram realizadas por mim.


## Objetivo da validação

A validação foi conduzida com foco nas regras de negócio solicitadas no desafio técnico:

- Cadastro de pessoas.
- Cadastro de categorias.
- Cadastro de transações.
- Consulta de totais por pessoa.
- Bloqueio de receitas para pessoas menores de idade.
- Validação da compatibilidade entre tipo da transação e finalidade da categoria.
- Permissão de categorias com finalidade Ambas para receitas e despesas.
- Exclusão de pessoa com remoção das transações vinculadas.

Além da execução manual, os principais cenários foram automatizados para compor uma pirâmide de testes adequada.

## Tecnologias utilizadas

### Back-end

- C#
- .NET
- xUnit
- HttpClient para testes de integração contra a API local

### Front-end

- TypeScript
- Vitest

### E2E

- Playwright
- Chromium

### Documentação

- Markdown
- Evidências com prints
- Registro de bugs em arquivos separados

## Estrutura do repositório

```text
qa-tests-minhas-financas/
├── backend-tests/
│   └── unit/
│       └── MinhasFinancas.UnitTests/
│           └── Tests/
│               └── RegrasNegocioTests.cs
│
├── integration/
│   └── MinhasFinancas.IntegrationTests/
│       └── Tests/
│           ├── PessoasApiTests.cs
│           ├── CategoriasApiTests.cs
│           └── TransacoesApiTests.cs
│
├── frontend-tests/
│   ├── vitest/
│   │   └── minhas-financas-vitest/
│   │       └── tests/
│   │           └── regras-negocio-front.test.ts
│   │
│   └── playwright/
│       └── minhas-financas-e2e/
│           └── tests/
│               ├── dashboard.spec.ts
│               └── navegacao.spec.ts
│
├── docs/
│   ├── evidencias/
│   │   ├── execucao-manual-api.md
│   │   └── prints/
│   │
│   └── bugs/
│       ├── BUG-001-receita-menor-retorna-500.md
│       ├── BUG-002-receita-categoria-despesa-retorna-500.md
│       └── BUG-003-despesa-categoria-receita-retorna-500.md
│
├── .gitignore
└── README.md
```

## Estratégia de testes

A estratégia foi organizada com base na pirâmide de testes.

### Testes unitários

Os testes unitários foram utilizados para validar regras isoladas, sem dependência da API, banco de dados ou interface.

Foram cobertos cenários como:

- Pessoa adulta deve ser considerada maior de idade.
- Pessoa menor deve ser considerada menor de idade.
- Categoria Receita deve permitir transação Receita.
- Categoria Receita não deve permitir transação Despesa.
- Categoria Despesa deve permitir transação Despesa.
- Categoria Despesa não deve permitir transação Receita.
- Categoria Ambas deve permitir Receita e Despesa.
- Pessoa menor não deve poder registrar Receita.

### Testes de integração

Os testes de integração foram criados para validar o comportamento real da API local em `http://localhost:5000`.

Esses testes executam requisições HTTP reais contra os endpoints da aplicação e validam os retornos da API para os principais fluxos de negócio.

Foram cobertos cenários como:

- Cadastro de pessoa adulta.
- Cadastro de pessoa menor de idade.
- Cadastro de categorias Receita, Despesa e Ambas.
- Cadastro de Receita válida para pessoa adulta.
- Cadastro de Despesa válida para pessoa adulta.
- Cadastro de Despesa válida para pessoa menor de idade.
- Bloqueio de Receita para menor de idade.
- Bloqueio de Receita usando categoria Despesa.
- Bloqueio de Despesa usando categoria Receita.
- Categoria Ambas aceitando Receita.
- Categoria Ambas aceitando Despesa.

### Testes de front-end com Vitest

Os testes com Vitest validam regras esperadas para a camada de front-end, principalmente relacionadas às validações de transação.

Foram cobertos cenários como:

- Permitir Receita para pessoa adulta com categoria Receita.
- Permitir Despesa para pessoa menor com categoria Despesa.
- Bloquear Receita para pessoa menor de idade.
- Bloquear Receita usando categoria Despesa.
- Bloquear Despesa usando categoria Receita.
- Permitir Receita e Despesa quando a categoria for Ambas.

### Testes E2E com Playwright

Os testes E2E foram utilizados para validar o carregamento da aplicação e a navegação básica entre telas principais.

Foram cobertos cenários como:

- Carregamento da aplicação.
- Acesso à tela de Pessoas.
- Acesso à tela de Categorias.
- Acesso à tela de Transações.
- Validação de que o Dashboard não apresenta página em branco.

## Pré-condições para execução

Antes de executar os testes de integração e E2E, a aplicação original precisa estar em execução localmente.

### API

```text
http://localhost:5000
```

### Swagger

```text
http://localhost:5000/swagger/index.html
```

### Front-end

```text
http://localhost:5173
```

## Como executar os testes

### Testes unitários com xUnit

```bash
cd backend-tests/unit/MinhasFinancas.UnitTests
dotnet test
```

Resultado obtido localmente:

```text
Total: 8
Falhas: 0
Sucesso: 8
```

### Testes de integração com xUnit

Pré-condição: a API original deve estar em execução localmente em `http://localhost:5000`.

```bash
cd integration/MinhasFinancas.IntegrationTests
dotnet test
```

Resultado obtido localmente:

```text
Total: 13
Falhas: 0
Sucesso: 13
```

Observação: alguns cenários negativos validam bugs conhecidos documentados em `docs/bugs`. Nesses casos, a API retorna `500 Internal Server Error` para regras de negócio que deveriam retornar erro de validação, como `400 Bad Request`.

### Testes de front-end com Vitest

```bash
cd frontend-tests/vitest/minhas-financas-vitest
npm install
npm test
```

Resultado obtido localmente:

```text
Test Files: 1 passed
Tests: 6 passed
```

### Testes E2E com Playwright

Pré-condição: o front-end original deve estar em execução em `http://localhost:5173`.

```bash
cd frontend-tests/playwright/minhas-financas-e2e
npm install
npx playwright install
npm test
```

Resultado obtido localmente:

```text
6 passed
```

## Execução manual da API

Além dos testes automatizados, foi realizada uma execução manual via Swagger para validar os principais fluxos e registrar evidências.

A documentação completa está disponível em:

```text
docs/evidencias/execucao-manual-api.md
```

Os prints da execução manual foram organizados em:

```text
docs/evidencias/prints/
```

A massa de dados utilizada nos testes manuais foi identificada com o sufixo `QA001`, facilitando a rastreabilidade dos registros criados.

## Massa de dados utilizada na execução manual

### Pessoas

| Registro | ID | Status |
|---|---|---|
| Pessoa Adulta QA001 | `019e14a2-baa6-7792-a642-742e2867550a` | 201 Created |
| Pessoa Menor QA001 | `019e14ab-bffa-7d39-8eaf-9bff6ed5c03b` | 201 Created |

### Categorias

| Registro | ID | Finalidade | Status |
|---|---|---|---|
| Receita QA001 | `019e14af-42df-7c4d-b7ca-dc01330b6720` | Receita | 201 Created |
| Despesa QA001 | `019e14b0-9603-7e6c-9c2c-9da6934b9732` | Despesa | 201 Created |
| Categoria Ambas QA001 | `019e14b1-0a5b-7765-80e0-f2c04301913e` | Ambas | 201 Created |

## Principais cenários validados manualmente

### Cadastro de pessoa adulta

Foi realizado o cadastro de uma pessoa adulta pelo endpoint `POST /api/v1/Pessoas`, utilizando nome e data de nascimento válidos.

A API retornou `201 Created`, criando o registro com sucesso e retornando os dados da pessoa cadastrada, incluindo idade calculada corretamente.

Resultado: comportamento conforme esperado.

### Cadastro de pessoa menor de idade

Foi realizado o cadastro de uma pessoa menor de idade pelo endpoint `POST /api/v1/Pessoas`.

A API retornou `201 Created`, criando o registro com sucesso.

Resultado: comportamento conforme esperado, pois a regra de negócio restringe apenas o lançamento de receitas para menores, não o cadastro da pessoa.

### Cadastro de categorias

Foram cadastradas categorias com as finalidades Receita, Despesa e Ambas.

A API retornou `201 Created` em todos os cenários.

Resultado: comportamento conforme esperado.

### Cadastro de Receita válida para pessoa adulta

Foi cadastrada uma transação do tipo Receita para uma pessoa adulta utilizando categoria com finalidade Receita.

A API retornou `201 Created`.

Resultado: comportamento conforme esperado.

### Cadastro de Despesa válida para pessoa adulta

Foi cadastrada uma transação do tipo Despesa para uma pessoa adulta utilizando categoria com finalidade Despesa.

A API retornou `201 Created`.

Resultado: comportamento conforme esperado.

### Cadastro de Despesa válida para pessoa menor de idade

Foi cadastrada uma transação do tipo Despesa para uma pessoa menor de idade utilizando categoria com finalidade Despesa.

A API retornou `201 Created`.

Resultado: comportamento conforme esperado, pois menor de idade pode possuir despesas.

### Tentativa de cadastrar Receita para pessoa menor de idade

Foi realizada uma tentativa de cadastro de Receita para uma pessoa menor de idade.

A regra de negócio foi aplicada e a transação não foi criada. Porém, a API retornou `500 Internal Server Error`.

Resultado: regra aplicada, mas retorno HTTP inadequado.

### Tentativa de cadastrar Receita usando categoria Despesa

Foi realizada uma tentativa de cadastro de Receita utilizando uma categoria com finalidade Despesa.

A regra de negócio foi aplicada e a transação não foi criada. Porém, a API retornou `500 Internal Server Error`.

Resultado: regra aplicada, mas retorno HTTP inadequado.

### Tentativa de cadastrar Despesa usando categoria Receita

Foi realizada uma tentativa de cadastro de Despesa utilizando uma categoria com finalidade Receita.

A regra de negócio foi aplicada e a transação não foi criada. Porém, a API retornou `500 Internal Server Error`.

Resultado: regra aplicada, mas retorno HTTP inadequado.

### Categoria Ambas aceitando Receita e Despesa

Foram cadastradas transações dos tipos Receita e Despesa utilizando categoria com finalidade Ambas.

A API retornou `201 Created` nos dois cenários.

Resultado: comportamento conforme esperado.

### Consulta de totais por pessoa

Foi realizada a consulta do endpoint `GET /api/v1/Totais/pessoas`.

Para a pessoa `Pessoa Adulta QA001`, o sistema retornou:

```json
{
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "nome": "Pessoa Adulta QA001",
  "totalReceitas": 1300,
  "totalDespesas": 330,
  "saldo": 970
}
```

O cálculo está correto, considerando:

```text
Receitas: 1000 + 300 = 1300
Despesas: 250 + 80 = 330
Saldo: 1300 - 330 = 970
```

Para a pessoa `Pessoa Menor QA001`, o sistema retornou:

```json
{
  "pessoaId": "019e14ab-bffa-7d39-8eaf-9bff6ed5c03b",
  "nome": "Pessoa Menor QA001",
  "totalReceitas": 0,
  "totalDespesas": 100,
  "saldo": -100
}
```

Resultado: comportamento conforme esperado.

### Exclusão de pessoa e validação de cascata

Foi criada uma pessoa específica para validação da exclusão. Após a exclusão pelo endpoint `DELETE /api/v1/Pessoas/{id}`, a API retornou `204 No Content`.

Em seguida, foi consultado o endpoint `GET /api/v1/Transacoes` para confirmar se ainda existiam transações vinculadas à pessoa excluída.

Não foram encontradas transações vinculadas ao registro removido.

Resultado: comportamento conforme esperado.

## Bugs encontrados

Durante a validação manual e automatizada, foram identificados três comportamentos inconsistentes.

### BUG-001: Receita para menor de idade retorna 500

Arquivo:

```text
docs/bugs/BUG-001-receita-menor-retorna-500.md
```

Resumo:

A regra de negócio bloqueia corretamente a Receita para menor de idade, mas a API retorna `500 Internal Server Error`.

Resultado esperado:

A API deveria retornar erro de validação de negócio, preferencialmente `400 Bad Request`, com mensagem clara.

### BUG-002: Receita com categoria Despesa retorna 500

Arquivo:

```text
docs/bugs/BUG-002-receita-categoria-despesa-retorna-500.md
```

Resumo:

A regra de negócio bloqueia corretamente uma Receita utilizando categoria com finalidade Despesa, mas a API retorna `500 Internal Server Error`.

Resultado esperado:

A API deveria retornar erro de validação de negócio, preferencialmente `400 Bad Request`.

### BUG-003: Despesa com categoria Receita retorna 500

Arquivo:

```text
docs/bugs/BUG-003-despesa-categoria-receita-retorna-500.md
```

Resumo:

A regra de negócio bloqueia corretamente uma Despesa utilizando categoria com finalidade Receita, mas a API retorna `500 Internal Server Error`.

Resultado esperado:

A API deveria retornar erro de validação de negócio, preferencialmente `400 Bad Request`.

## Justificativa das escolhas de teste

A priorização foi feita com base no risco das regras de negócio.

Como o sistema trata controle financeiro residencial, os cenários mais importantes são aqueles que podem gerar inconsistência nos totais, nos lançamentos ou na classificação das transações.

Por isso, os testes priorizaram:

- Validação de idade para impedir Receita de menor de idade.
- Compatibilidade entre categoria e tipo da transação.
- Funcionamento da categoria com finalidade Ambas.
- Cálculo correto de receitas, despesas e saldo.
- Exclusão de pessoa sem deixar transações órfãs.

A suíte automatizada foi dividida em camadas para manter boa organização:

- Testes unitários para regras puras.
- Testes de integração para comportamento real da API.
- Vitest para validações esperadas na camada de front-end.
- Playwright para navegação e carregamento da aplicação pelo navegador.

Essa abordagem evita depender apenas de testes E2E, que são mais lentos e sensíveis a mudanças visuais, e concentra a maior parte da validação nas camadas mais estáveis.

## Observações importantes

- O código-fonte original da aplicação não foi alterado.
- O código-fonte original da aplicação não foi publicado neste repositório.
- Este repositório contém apenas testes, documentação, evidências e registros de bugs.
- As validações negativas que retornam `500 Internal Server Error` foram mantidas nos testes de integração como comportamento atual da aplicação e associadas aos bugs documentados.
- Do ponto de vista de negócio, esses cenários deveriam retornar erro de validação, preferencialmente `400 Bad Request`.

## Resumo da entrega

| Tipo de validação | Ferramenta | Resultado |
|---|---|---|
| Testes unitários | xUnit | 8 testes passando |
| Testes de integração | xUnit | 13 testes passando |
| Testes de front-end | Vitest | 6 testes passando |
| Testes E2E | Playwright | 6 testes passando |
| Evidências manuais | Swagger e prints | Documentadas |
| Bugs encontrados | Markdown | 3 bugs documentados |

## Conclusão

A validação confirmou que os principais fluxos do sistema funcionam corretamente para os cenários positivos.

Também foi possível identificar inconsistências importantes no tratamento de erros de regra de negócio. Embora as regras estejam sendo aplicadas e as transações inválidas sejam bloqueadas, a API retorna `500 Internal Server Error` em situações que deveriam ser tratadas como validação de negócio.

Com isso, a entrega cobre os pontos principais solicitados no desafio, incluindo análise funcional, documentação de bugs, pirâmide de testes e automação em back-end, front-end e E2E.