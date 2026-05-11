# BUG-002 - Receita com categoria Despesa retorna 500 em vez de erro de validacao

## Cenario testado e resultado obtido

Foi realizada uma tentativa de cadastro de transacao do tipo Receita utilizando uma categoria com finalidade Despesa.

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

Response body:

```json
{
  "StatusCode": 500,
  "Message": "Ocorreu um erro interno no servidor.",
  "Detailed": "Nao e possivel registrar receita em categoria de despesa."
}
```

## Resultado esperado

O sistema deve bloquear o cadastro de Receita em categoria com finalidade Despesa e retornar erro de validacao de negocio, preferencialmente `400 Bad Request`.

## Acao necessaria

Ajustar o tratamento da excecao de compatibilidade entre tipo da transacao e finalidade da categoria, retornando status HTTP adequado.

## Evidencia

Print 10.
