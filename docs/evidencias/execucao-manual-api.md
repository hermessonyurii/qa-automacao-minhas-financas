# Evidencias da execucao manual - API Minhas Financas

## Ambiente de teste

- Aplicacao: Minhas Financas
- API: `http://localhost:5000/swagger/index.html`
- Front-end: `http://localhost:5173`
- Versao da API: `v1`
- Ferramenta utilizada: Swagger UI
- Tipo de validacao: Execucao manual de cenarios funcionais e regras de negocio

---

## Objetivo da execucao

Esta execucao teve como objetivo validar os principais fluxos e regras de negocio do sistema de controle de gastos residenciais, com foco em:

- Cadastro de pessoas;
- Cadastro de categorias;
- Cadastro de transacoes;
- Regra de bloqueio de Receita para menor de idade;
- Regra de compatibilidade entre categoria e tipo da transacao;
- Categoria com finalidade Ambas;
- Calculo de totais por pessoa;
- Exclusao de pessoa e validacao de exclusao em cascata.

---

## Massa de dados utilizada

Durante os testes, foi criada uma massa especifica identificada com o sufixo `QA001`, com o objetivo de facilitar a rastreabilidade dos dados e evitar confusao com registros antigos existentes na base.

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

---

# Print 01 - Cadastro de pessoa adulta

**Evidencia:** `docs/evidencias/prints/print-01-cadastro-pessoa-adulta.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma pessoa adulta pelo endpoint `POST /api/v1/Pessoas`, utilizando o nome `Pessoa Adulta QA001` e a data de nascimento `1995-01-01`.

O sistema retornou `201 Created`, criando o registro com sucesso. A resposta apresentou os dados da pessoa criada, incluindo `id`, `nome`, `dataNascimento` e `idade`.

A idade retornada foi `31`, confirmando que o calculo de idade foi realizado corretamente para uma pessoa maior de idade.

## Acao necessaria

Nenhuma acao necessaria. O cadastro de pessoa adulta funcionou conforme esperado.

---

# Print 02 - Cadastro de pessoa menor de idade

**Evidencia:** `docs/evidencias/prints/print-02-cadastro-pessoa-menor.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma pessoa menor de idade pelo endpoint `POST /api/v1/Pessoas`, utilizando o nome `Pessoa Menor QA001` e a data de nascimento `2015-01-01`.

O sistema retornou `201 Created`, criando o registro com sucesso. A resposta apresentou idade `11`.

Esse comportamento esta correto, pois a regra de negocio nao impede o cadastro de pessoas menores de idade. A restricao se aplica apenas ao lancamento de transacoes do tipo Receita para menores.

## Acao necessaria

Nenhuma acao necessaria. O sistema permitiu corretamente o cadastro de pessoa menor de idade.

---

# Print 03 - Cadastro de categoria Receita

**Evidencia:** `docs/evidencias/prints/print-03-cadastro-categoria-receita.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro da categoria `Receita QA001` pelo endpoint `POST /api/v1/Categorias`, utilizando `finalidade: 1`.

O sistema retornou `201 Created`, criando a categoria com sucesso. A categoria foi criada com finalidade Receita e utilizada posteriormente nos testes de transacoes do tipo Receita.

## Acao necessaria

Nenhuma acao necessaria. O cadastro de categoria com finalidade Receita funcionou conforme esperado.

---

# Print 04 - Cadastro de categoria Despesa

**Evidencia:** `docs/evidencias/prints/print-04-cadastro-categoria-despesa.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro da categoria `Despesa QA001` pelo endpoint `POST /api/v1/Categorias`, utilizando `finalidade: 0`.

O sistema retornou `201 Created`, criando a categoria com sucesso. A categoria foi criada com finalidade Despesa e utilizada posteriormente nos testes de transacoes do tipo Despesa.

## Acao necessaria

Nenhuma acao necessaria. O cadastro de categoria com finalidade Despesa funcionou conforme esperado.

---

# Print 05 - Cadastro de categoria Ambas

**Evidencia:** `docs/evidencias/prints/print-05-cadastro-categoria-ambas.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro da categoria `Categoria Ambas QA001` pelo endpoint `POST /api/v1/Categorias`, utilizando `finalidade: 2`.

O sistema retornou `201 Created`, criando a categoria com sucesso.

Essa categoria foi utilizada para validar a regra em que uma categoria com finalidade Ambas deve aceitar transacoes dos tipos Receita e Despesa.

## Acao necessaria

Nenhuma acao necessaria. O cadastro de categoria com finalidade Ambas funcionou conforme esperado.

---

# Print 06 - Cadastro de Receita valida para pessoa adulta

**Evidencia:** `docs/evidencias/prints/print-06-receita-valida-adulto.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma transacao do tipo Receita para a pessoa `Pessoa Adulta QA001`, utilizando a categoria `Receita QA001`.

Payload utilizado:

```json
{
  "descricao": "Receita valida QA001",
  "valor": 1000,
  "tipo": 1,
  "categoriaId": "019e14af-42df-7c4d-b7ca-dc01330b6720",
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "data": "2026-05-10"
}
```

O sistema retornou `201 Created`, criando a transacao com sucesso.

O comportamento esta correto, pois a pessoa e maior de idade e a categoria possui finalidade compativel com o tipo da transacao.

## Acao necessaria

Nenhuma acao necessaria. O sistema permitiu corretamente o cadastro de Receita para pessoa adulta.

---

# Print 07 - Cadastro de Despesa valida para pessoa adulta

**Evidencia:** `docs/evidencias/prints/print-07-despesa-valida-adulto.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma transacao do tipo Despesa para a pessoa `Pessoa Adulta QA001`, utilizando a categoria `Despesa QA001`.

Payload utilizado:

```json
{
  "descricao": "Despesa valida adulto QA001",
  "valor": 250,
  "tipo": 0,
  "categoriaId": "019e14b0-9603-7e6c-9c2c-9da6934b9732",
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "data": "2026-05-10"
}
```

O sistema retornou `201 Created`, criando a transacao com sucesso.

O comportamento esta correto, pois a categoria possui finalidade Despesa e foi utilizada em uma transacao do mesmo tipo.

## Acao necessaria

Nenhuma acao necessaria. O cadastro de Despesa para pessoa adulta funcionou conforme esperado.

---

# Print 08 - Cadastro de Despesa valida para pessoa menor de idade

**Evidencia:** `docs/evidencias/prints/print-08-despesa-valida-menor.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma transacao do tipo Despesa para a pessoa `Pessoa Menor QA001`, utilizando a categoria `Despesa QA001`.

Payload utilizado:

```json
{
  "descricao": "Despesa valida menor QA001",
  "valor": 100,
  "tipo": 0,
  "categoriaId": "019e14b0-9603-7e6c-9c2c-9da6934b9732",
  "pessoaId": "019e14ab-bffa-7d39-8eaf-9bff6ed5c03b",
  "data": "2026-05-10"
}
```

O sistema retornou `201 Created`, criando a despesa com sucesso.

Esse comportamento esta correto, pois a regra de negocio restringe apenas Receitas para menores de idade, nao Despesas.

## Acao necessaria

Nenhuma acao necessaria. O sistema permitiu corretamente o cadastro de Despesa para pessoa menor de idade.

---

# Print 09 - Tentativa de cadastrar Receita para pessoa menor de idade

**Evidencia:** `docs/evidencias/prints/print-09-receita-invalida-menor-500.png`

## Cenario testado e resultado obtido

Foi realizada uma tentativa de cadastro de transacao do tipo Receita para a pessoa `Pessoa Menor QA001`, utilizando a categoria `Receita QA001`.

Payload utilizado:

```json
{
  "descricao": "Receita invalida menor QA001",
  "valor": 500,
  "tipo": 1,
  "categoriaId": "019e14af-42df-7c4d-b7ca-dc01330b6720",
  "pessoaId": "019e14ab-bffa-7d39-8eaf-9bff6ed5c03b",
  "data": "2026-05-10"
}
```

A regra de negocio foi aplicada e a transacao nao foi criada. Porem, a API retornou `500 Internal Server Error`.

Response body retornado:

```json
{
  "StatusCode": 500,
  "Message": "Ocorreu um erro interno no servidor.",
  "Detailed": "Menores de 18 anos nao podem registrar receitas."
}
```

A validacao de negocio foi aplicada corretamente, pois a Receita para menor de idade foi bloqueada. No entanto, o status HTTP retornado nao esta adequado para esse tipo de situacao.

Por se tratar de uma regra de negocio prevista, o retorno mais adequado seria `400 Bad Request`, com mensagem clara ao consumidor da API.

## Acao necessaria

Ajustar o tratamento da excecao de regra de negocio para retornar status HTTP adequado, preferencialmente `400 Bad Request`, evitando que uma validacao esperada seja apresentada como erro interno do servidor.

---

# Print 10 - Tentativa de cadastrar Receita usando categoria Despesa

**Evidencia:** `docs/evidencias/prints/print-10-receita-categoria-despesa-500.png`

## Cenario testado e resultado obtido

Foi realizada uma tentativa de cadastro de transacao do tipo Receita utilizando a categoria `Despesa QA001`.

Payload utilizado:

```json
{
  "descricao": "Receita com categoria despesa QA001",
  "valor": 200,
  "tipo": 1,
  "categoriaId": "019e14b0-9603-7e6c-9c2c-9da6934b9732",
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "data": "2026-05-10"
}
```

A regra de negocio foi aplicada e a transacao nao foi criada. Porem, a API retornou `500 Internal Server Error`.

Response body retornado:

```json
{
  "StatusCode": 500,
  "Message": "Ocorreu um erro interno no servidor.",
  "Detailed": "Nao e possivel registrar receita em categoria de despesa."
}
```

O bloqueio da transacao esta correto, pois uma categoria com finalidade Despesa nao deve permitir transacoes do tipo Receita. Porem, o status retornado nao e adequado para uma validacao esperada de regra de negocio.

## Acao necessaria

Ajustar o tratamento da excecao de compatibilidade entre tipo da transacao e finalidade da categoria, retornando erro de validacao de negocio, preferencialmente `400 Bad Request`.

---

# Print 11 - Tentativa de cadastrar Despesa usando categoria Receita

**Evidencia:** `docs/evidencias/prints/print-11-despesa-categoria-receita-500.png`

## Cenario testado e resultado obtido

Foi realizada uma tentativa de cadastro de transacao do tipo Despesa utilizando a categoria `Receita QA001`.

Payload utilizado:

```json
{
  "descricao": "Despesa com categoria receita QA001",
  "valor": 120,
  "tipo": 0,
  "categoriaId": "019e14af-42df-7c4d-b7ca-dc01330b6720",
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "data": "2026-05-10"
}
```

A regra de negocio foi aplicada e a transacao nao foi criada. Porem, a API retornou `500 Internal Server Error`.

Response body retornado:

```json
{
  "StatusCode": 500,
  "Message": "Ocorreu um erro interno no servidor.",
  "Detailed": "Nao e possivel registrar despesa em categoria de receita."
}
```

O bloqueio esta correto, pois uma categoria com finalidade Receita nao deve permitir transacoes do tipo Despesa. Porem, o status HTTP retornado nao esta adequado.

## Acao necessaria

Ajustar o tratamento de excecoes de negocio para retornar erro de validacao adequado, preferencialmente `400 Bad Request`, com mensagem clara ao consumidor da API.

---

# Print 12 - Cadastro de Receita utilizando categoria Ambas

**Evidencia:** `docs/evidencias/prints/print-12-receita-categoria-ambas.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma transacao do tipo Receita utilizando a categoria `Categoria Ambas QA001`.

Payload utilizado:

```json
{
  "descricao": "Receita categoria ambas QA001",
  "valor": 300,
  "tipo": 1,
  "categoriaId": "019e14b1-0a5b-7765-80e0-f2c04301913e",
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "data": "2026-05-10"
}
```

O sistema retornou `201 Created`, criando a transacao com sucesso.

Esse comportamento esta correto, pois categorias com finalidade Ambas devem aceitar transacoes do tipo Receita e Despesa.

## Acao necessaria

Nenhuma acao necessaria. O sistema permitiu corretamente o cadastro de Receita em categoria com finalidade Ambas.

---

# Print 13 - Cadastro de Despesa utilizando categoria Ambas

**Evidencia:** `docs/evidencias/prints/print-13-despesa-categoria-ambas.png`

## Cenario testado e resultado obtido

Foi realizado o cadastro de uma transacao do tipo Despesa utilizando a categoria `Categoria Ambas QA001`.

Payload utilizado:

```json
{
  "descricao": "Despesa categoria ambas QA001",
  "valor": 80,
  "tipo": 0,
  "categoriaId": "019e14b1-0a5b-7765-80e0-f2c04301913e",
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "data": "2026-05-10"
}
```

O sistema retornou `201 Created`, criando a transacao com sucesso.

Esse comportamento esta correto, pois a categoria com finalidade Ambas aceitou corretamente uma transacao do tipo Despesa.

## Acao necessaria

Nenhuma acao necessaria. O sistema permitiu corretamente o cadastro de Despesa em categoria com finalidade Ambas.

---

# Print 14 - Criacao de pessoa para validacao de exclusao em cascata

**Evidencia:** `docs/evidencias/prints/print-14-criacao-pessoa-cascata.png`

## Cenario testado e resultado obtido

Foi criada a pessoa `Pessoa Cascata QA001` pelo endpoint `POST /api/v1/Pessoas`, com o objetivo de validar posteriormente a exclusao de pessoa e a remocao de dados vinculados.

O sistema retornou `201 Created`, criando a pessoa com sucesso.

Dados retornados:

```text
ID: 019e14bf-f69b-7c00-853f-34bd42f8945e
Nome: Pessoa Cascata QA001
Data de nascimento: 1990-01-01
Idade: 36
```

## Acao necessaria

Nenhuma acao necessaria. A pessoa foi criada corretamente para uso no cenario de exclusao.

---

# Print 15 - Exclusao da pessoa utilizada no teste de cascata

**Evidencia:** `docs/evidencias/prints/print-15-exclusao-pessoa-cascata.png`

## Cenario testado e resultado obtido

Foi realizada a exclusao da pessoa `Pessoa Cascata QA001` pelo endpoint `DELETE /api/v1/Pessoas/{id}`.

ID utilizado:

```text
019e14bf-f69b-7c00-853f-34bd42f8945e
```

O sistema retornou `204 No Content`, indicando que a exclusao foi realizada com sucesso.

Apos a exclusao, foi consultado o endpoint `GET /api/v1/Transacoes` para confirmar se existiam transacoes associadas a pessoa removida. A listagem nao retornou transacoes vinculadas a pessoa excluida, confirmando que nao permaneceram registros orfaos.

## Acao necessaria

Nenhuma acao necessaria. A exclusao da pessoa funcionou corretamente e a validacao posterior nao identificou transacoes vinculadas a pessoa removida.

---

# Validacao complementar - Totais por pessoa

## Cenario testado e resultado obtido

Foi realizada a consulta do endpoint `GET /api/v1/Totais/pessoas` apos o cadastro das transacoes validas para `Pessoa Adulta QA001` e `Pessoa Menor QA001`.

Para `Pessoa Adulta QA001`, o sistema retornou:

```json
{
  "pessoaId": "019e14a2-baa6-7792-a642-742e2867550a",
  "nome": "Pessoa Adulta QA001",
  "totalReceitas": 1300,
  "totalDespesas": 330,
  "saldo": 970
}
```

O calculo esta correto, considerando:

```text
Receitas:
1000 + 300 = 1300

Despesas:
250 + 80 = 330

Saldo:
1300 - 330 = 970
```

Para `Pessoa Menor QA001`, o sistema retornou:

```json
{
  "pessoaId": "019e14ab-bffa-7d39-8eaf-9bff6ed5c03b",
  "nome": "Pessoa Menor QA001",
  "totalReceitas": 0,
  "totalDespesas": 100,
  "saldo": -100
}
```

O calculo tambem esta correto, pois a pessoa menor possui apenas uma despesa valida de `100` e nenhuma Receita.

## Acao necessaria

Nenhuma acao necessaria. O calculo de totais por pessoa funcionou conforme esperado.

---

# Resumo dos resultados

## Cenario testado e resultado obtido

Foram validados os principais fluxos da API:

- Cadastro de pessoa adulta;
- Cadastro de pessoa menor de idade;
- Cadastro de categorias Receita, Despesa e Ambas;
- Cadastro de Receita valida para adulto;
- Cadastro de Despesa valida para adulto;
- Cadastro de Despesa valida para menor;
- Bloqueio de Receita para menor;
- Bloqueio de Receita usando categoria Despesa;
- Bloqueio de Despesa usando categoria Receita;
- Categoria Ambas aceitando Receita;
- Categoria Ambas aceitando Despesa;
- Consulta de totais por pessoa;
- Exclusao de pessoa;
- Validacao pos-exclusao para evitar transacoes orfas.

Durante os testes, foram identificadas inconsistencias no retorno HTTP de algumas regras de negocio. As validacoes funcionam, pois as transacoes invalidas foram bloqueadas, porem a API retornou `500 Internal Server Error` em cenarios que deveriam retornar erro de validacao de negocio.

## Bugs identificados

- BUG-001 - Receita para menor de idade retorna `500 Internal Server Error`;
- BUG-002 - Receita com categoria Despesa retorna `500 Internal Server Error`;
- BUG-003 - Despesa com categoria Receita retorna `500 Internal Server Error`.

## Acao necessaria

Documentar os bugs encontrados em arquivos separados dentro de `docs/bugs` e utilizar esses cenarios como base para os testes automatizados de integracao.
