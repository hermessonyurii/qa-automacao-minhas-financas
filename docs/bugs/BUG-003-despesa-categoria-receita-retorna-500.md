# BUG-003 - Despesa com categoria Receita retorna 500 em vez de erro de validacao

## Cenario testado e resultado obtido

Foi realizada uma tentativa de cadastro de transacao do tipo Despesa utilizando uma categoria com finalidade Receita.

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

Response body:

```json
{
  "StatusCode": 500,
  "Message": "Ocorreu um erro interno no servidor.",
  "Detailed": "Nao e possivel registrar despesa em categoria de receita."
}
```

## Resultado esperado

O sistema deve bloquear o cadastro de Despesa em categoria com finalidade Receita e retornar erro de validacao de negocio, preferencialmente `400 Bad Request`.

## Acao necessaria

Ajustar o tratamento da excecao de compatibilidade entre tipo da transacao e finalidade da categoria, retornando status HTTP adequado.

## Evidencia

Print 11.
