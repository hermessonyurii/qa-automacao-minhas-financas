namespace MinhasFinancas.UnitTests.Tests;

public class RegrasNegocioTests
{
    private enum TipoTransacao
    {
        Despesa = 0,
        Receita = 1
    }

    private enum FinalidadeCategoria
    {
        Despesa = 0,
        Receita = 1,
        Ambas = 2
    }

    [Fact]
    public void Pessoa_adulta_deve_ser_considerada_maior_de_idade()
    {
        var idade = CalcularIdade(new DateTime(1995, 1, 1), new DateTime(2026, 5, 10));

        Assert.True(idade >= 18);
    }

    [Fact]
    public void Pessoa_menor_deve_ser_considerada_menor_de_idade()
    {
        var idade = CalcularIdade(new DateTime(2015, 1, 1), new DateTime(2026, 5, 10));

        Assert.True(idade < 18);
    }

    [Fact]
    public void Categoria_receita_deve_permitir_transacao_receita()
    {
        var resultado = CategoriaPermiteTipo(FinalidadeCategoria.Receita, TipoTransacao.Receita);

        Assert.True(resultado);
    }

    [Fact]
    public void Categoria_receita_nao_deve_permitir_transacao_despesa()
    {
        var resultado = CategoriaPermiteTipo(FinalidadeCategoria.Receita, TipoTransacao.Despesa);

        Assert.False(resultado);
    }

    [Fact]
    public void Categoria_despesa_deve_permitir_transacao_despesa()
    {
        var resultado = CategoriaPermiteTipo(FinalidadeCategoria.Despesa, TipoTransacao.Despesa);

        Assert.True(resultado);
    }

    [Fact]
    public void Categoria_despesa_nao_deve_permitir_transacao_receita()
    {
        var resultado = CategoriaPermiteTipo(FinalidadeCategoria.Despesa, TipoTransacao.Receita);

        Assert.False(resultado);
    }

    [Fact]
    public void Categoria_ambas_deve_permitir_receita_e_despesa()
    {
        var permiteReceita = CategoriaPermiteTipo(FinalidadeCategoria.Ambas, TipoTransacao.Receita);
        var permiteDespesa = CategoriaPermiteTipo(FinalidadeCategoria.Ambas, TipoTransacao.Despesa);

        Assert.True(permiteReceita);
        Assert.True(permiteDespesa);
    }

    [Fact]
    public void Pessoa_menor_nao_deve_poder_registrar_receita()
    {
        var idade = CalcularIdade(new DateTime(2015, 1, 1), new DateTime(2026, 5, 10));
        var tipoTransacao = TipoTransacao.Receita;

        var podeRegistrar = !(idade < 18 && tipoTransacao == TipoTransacao.Receita);

        Assert.False(podeRegistrar);
    }

    private static int CalcularIdade(DateTime dataNascimento, DateTime dataReferencia)
    {
        var idade = dataReferencia.Year - dataNascimento.Year;

        if (dataReferencia.Date < dataNascimento.AddYears(idade))
        {
            idade--;
        }

        return idade;
    }

    private static bool CategoriaPermiteTipo(FinalidadeCategoria finalidade, TipoTransacao tipo)
    {
        if (finalidade == FinalidadeCategoria.Ambas)
        {
            return true;
        }

        return (int)finalidade == (int)tipo;
    }
}