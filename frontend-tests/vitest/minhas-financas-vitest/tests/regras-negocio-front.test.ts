import { describe, expect, it } from "vitest";

enum TipoTransacao {
  Despesa = 0,
  Receita = 1,
}

enum FinalidadeCategoria {
  Despesa = 0,
  Receita = 1,
  Ambas = 2,
}

function calcularIdade(dataNascimento: string, dataReferencia = new Date("2026-05-10")) {
  const nascimento = new Date(dataNascimento);
  let idade = dataReferencia.getFullYear() - nascimento.getFullYear();

  const mesAtual = dataReferencia.getMonth();
  const diaAtual = dataReferencia.getDate();
  const mesNascimento = nascimento.getMonth();
  const diaNascimento = nascimento.getDate();

  const aindaNaoFezAniversario =
    mesAtual < mesNascimento ||
    (mesAtual === mesNascimento && diaAtual < diaNascimento);

  if (aindaNaoFezAniversario) {
    idade--;
  }

  return idade;
}

function ehMenorDeIdade(dataNascimento: string) {
  return calcularIdade(dataNascimento) < 18;
}

function categoriaPermiteTipo(finalidade: FinalidadeCategoria, tipo: TipoTransacao) {
  if (finalidade === FinalidadeCategoria.Ambas) {
    return true;
  }

  const despesa =
    finalidade === FinalidadeCategoria.Despesa && tipo === TipoTransacao.Despesa;
  const receita =
    finalidade === FinalidadeCategoria.Receita && tipo === TipoTransacao.Receita;

  return despesa || receita;
}

function podeCadastrarTransacao(
  dataNascimentoPessoa: string,
  tipo: TipoTransacao,
  finalidadeCategoria: FinalidadeCategoria
) {
  if (ehMenorDeIdade(dataNascimentoPessoa) && tipo === TipoTransacao.Receita) {
    return {
      permitido: false,
      motivo: "Menores de 18 anos não podem registrar receitas.",
    };
  }

  if (!categoriaPermiteTipo(finalidadeCategoria, tipo)) {
    return {
      permitido: false,
      motivo: "Categoria incompatível com o tipo da transação.",
    };
  }

  return {
    permitido: true,
    motivo: null,
  };
}

describe("Regras esperadas no front-end - Transações", () => {
  it("deve permitir Receita para pessoa adulta com categoria Receita", () => {
    const resultado = podeCadastrarTransacao(
      "1995-01-01",
      TipoTransacao.Receita,
      FinalidadeCategoria.Receita
    );

    expect(resultado.permitido).toBe(true);
    expect(resultado.motivo).toBeNull();
  });

  it("deve permitir Despesa para pessoa menor de idade com categoria Despesa", () => {
    const resultado = podeCadastrarTransacao(
      "2015-01-01",
      TipoTransacao.Despesa,
      FinalidadeCategoria.Despesa
    );

    expect(resultado.permitido).toBe(true);
    expect(resultado.motivo).toBeNull();
  });

  it("deve bloquear Receita para pessoa menor de idade", () => {
    const resultado = podeCadastrarTransacao(
      "2015-01-01",
      TipoTransacao.Receita,
      FinalidadeCategoria.Receita
    );

    expect(resultado.permitido).toBe(false);
    expect(resultado.motivo).toBe("Menores de 18 anos não podem registrar receitas.");
  });

  it("deve bloquear Receita usando categoria Despesa", () => {
    const resultado = podeCadastrarTransacao(
      "1995-01-01",
      TipoTransacao.Receita,
      FinalidadeCategoria.Despesa
    );

    expect(resultado.permitido).toBe(false);
    expect(resultado.motivo).toBe("Categoria incompatível com o tipo da transação.");
  });

  it("deve bloquear Despesa usando categoria Receita", () => {
    const resultado = podeCadastrarTransacao(
      "1995-01-01",
      TipoTransacao.Despesa,
      FinalidadeCategoria.Receita
    );

    expect(resultado.permitido).toBe(false);
    expect(resultado.motivo).toBe("Categoria incompatível com o tipo da transação.");
  });

  it("deve permitir Receita e Despesa quando a categoria for Ambas", () => {
    const receita = podeCadastrarTransacao(
      "1995-01-01",
      TipoTransacao.Receita,
      FinalidadeCategoria.Ambas
    );

    const despesa = podeCadastrarTransacao(
      "1995-01-01",
      TipoTransacao.Despesa,
      FinalidadeCategoria.Ambas
    );

    expect(receita.permitido).toBe(true);
    expect(despesa.permitido).toBe(true);
  });
});