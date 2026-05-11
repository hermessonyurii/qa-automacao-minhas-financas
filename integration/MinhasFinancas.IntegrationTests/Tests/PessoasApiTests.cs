using System.Net;
using System.Net.Http.Json;

namespace MinhasFinancas.IntegrationTests.Tests;

public class PessoasApiTests
{
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("http://localhost:5000")
    };

    [Fact]
    public async Task Deve_cadastrar_pessoa_adulta_com_sucesso()
    {
        var payload = new
        {
            nome = $"Pessoa Adulta Integracao {Guid.NewGuid()}",
            dataNascimento = "1995-01-01"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Pessoas", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Deve_cadastrar_pessoa_menor_de_idade_com_sucesso()
    {
        var payload = new
        {
            nome = $"Pessoa Menor Integracao {Guid.NewGuid()}",
            dataNascimento = "2015-01-01"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/Pessoas", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}