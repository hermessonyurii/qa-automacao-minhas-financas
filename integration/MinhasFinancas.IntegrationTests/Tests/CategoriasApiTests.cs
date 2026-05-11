using System.Net;
using System.Net.Http.Json;

namespace MinhasFinancas.IntegrationTests.Tests;

public class CategoriasApiTests
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("http://localhost:5000")
    };

    [Fact]
    public async Task Deve_cadastrar_categoria_receita_com_sucesso()
    {
        var payload = new
        {
            descricao = $"Receita Integracao {Guid.NewGuid()}",
            finalidade = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Categorias", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_categoria_despesa_com_sucesso()
    {
        var payload = new
        {
            descricao = $"Despesa Integracao {Guid.NewGuid()}",
            finalidade = 0
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Categorias", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_categoria_ambas_com_sucesso()
    {
        var payload = new
        {
            descricao = $"Categoria Ambas Integracao {Guid.NewGuid()}",
            finalidade = 2
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Categorias", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}