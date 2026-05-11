using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MinhasFinancas.IntegrationTests.Tests;

public class TransacoesApiTests
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("http://localhost:5000")
    };

    [Fact]
    public async Task Deve_cadastrar_receita_valida_para_pessoa_adulta()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Adulta Transacao", "1995-01-01");
        var categoriaId = await CriarCategoriaAsync("Receita Transacao", 1);

        var payload = new
        {
            descricao = $"Receita valida {Guid.NewGuid()}",
            valor = 1000,
            tipo = 1,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_despesa_valida_para_pessoa_adulta()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Adulta Despesa", "1995-01-01");
        var categoriaId = await CriarCategoriaAsync("Despesa Transacao", 0);

        var payload = new
        {
            descricao = $"Despesa valida {Guid.NewGuid()}",
            valor = 250,
            tipo = 0,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_despesa_valida_para_pessoa_menor()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Menor Despesa", "2015-01-01");
        var categoriaId = await CriarCategoriaAsync("Despesa Menor", 0);

        var payload = new
        {
            descricao = $"Despesa menor {Guid.NewGuid()}",
            valor = 100,
            tipo = 0,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_bloquear_receita_para_pessoa_menor_mas_retorna_500_bug_conhecido()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Menor Receita", "2015-01-01");
        var categoriaId = await CriarCategoriaAsync("Receita Menor", 1);

        var payload = new
        {
            descricao = $"Receita invalida menor {Guid.NewGuid()}",
            valor = 500,
            tipo = 1,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task Deve_bloquear_receita_com_categoria_despesa_mas_retorna_500_bug_conhecido()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Adulta Categoria Invalida", "1995-01-01");
        var categoriaId = await CriarCategoriaAsync("Categoria Despesa Invalida", 0);

        var payload = new
        {
            descricao = $"Receita com categoria despesa {Guid.NewGuid()}",
            valor = 200,
            tipo = 1,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task Deve_bloquear_despesa_com_categoria_receita_mas_retorna_500_bug_conhecido()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Adulta Categoria Receita", "1995-01-01");
        var categoriaId = await CriarCategoriaAsync("Categoria Receita Invalida", 1);

        var payload = new
        {
            descricao = $"Despesa com categoria receita {Guid.NewGuid()}",
            valor = 120,
            tipo = 0,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_receita_com_categoria_ambas()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Adulta Ambas Receita", "1995-01-01");
        var categoriaId = await CriarCategoriaAsync("Categoria Ambas Receita", 2);

        var payload = new
        {
            descricao = $"Receita categoria ambas {Guid.NewGuid()}",
            valor = 300,
            tipo = 1,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_despesa_com_categoria_ambas()
    {
        var pessoaId = await CriarPessoaAsync("Pessoa Adulta Ambas Despesa", "1995-01-01");
        var categoriaId = await CriarCategoriaAsync("Categoria Ambas Despesa", 2);

        var payload = new
        {
            descricao = $"Despesa categoria ambas {Guid.NewGuid()}",
            valor = 80,
            tipo = 0,
            categoriaId,
            pessoaId,
            data = "2026-05-10"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Transacoes", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private async Task<string> CriarPessoaAsync(string nomeBase, string dataNascimento)
    {
        var payload = new
        {
            nome = $"{nomeBase} {Guid.NewGuid()}",
            dataNascimento
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Pessoas", payload);
        response.EnsureSuccessStatusCode();

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("id").GetString()!;
    }

    private async Task<string> CriarCategoriaAsync(string descricaoBase, int finalidade)
    {
        var payload = new
        {
            descricao = $"{descricaoBase} {Guid.NewGuid()}",
            finalidade
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Categorias", payload);
        response.EnsureSuccessStatusCode();

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("id").GetString()!;
    }
}