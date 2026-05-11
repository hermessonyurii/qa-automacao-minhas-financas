# BUG-001 - Receita para menor de idade retorna 500 em vez de erro de validacao

## Cenario testado e resultado obtido

Foi realizada uma tentativa de cadastro de transacao do tipo Receita para a pessoa `Pessoa Menor QA001`, utilizando uma categoria com finalidade Receita.

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

Response body:

```json
{
  "StatusCode": 500,
  "Message": "Ocorreu um erro interno no servidor.",
  "Detailed": "Menores de 18 anos nao podem registrar receitas."
}
```

## Resultado esperado

O sistema deve bloquear o cadastro de Receita para pessoa menor de idade e retornar erro de validacao de negocio, preferencialmente `400 Bad Request`, com mensagem clara.

## Acao necessaria

Ajustar o tratamento da excecao de regra de negocio para retornar status HTTP adequado, evitando que uma validacao esperada seja tratada como erro interno do servidor.

## Evidencia

Print 09.
