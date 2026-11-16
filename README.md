# console-selenium-42-net

Projeto de automação de testes de interface com **.NET 10**, **Selenium WebDriver** e **MSTest**, organizado de forma modular para facilitar manutenção e evolução.

## Estrutura da solução

- `ConsoleSelenium.slnx`: solução .NET.
- `ConsoleSelenium/`: aplicação console base do projeto.
- `ConsoleSelenium.Tests/`: projeto de testes automatizados.
  - `Infrastructure/`: configurações e criação do WebDriver.
  - `Pages/`: abstrações de páginas (Page Objects) para interação com a interface.
  - `DuckDuckGoSearchTests.cs`: cenários de teste de busca no DuckDuckGo.

## Pré-requisitos

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Microsoft Edge](https://www.microsoft.com/edge) instalado
- Edge WebDriver compatível com a versão do navegador (o Selenium Manager pode resolver automaticamente em ambientes suportados)

## Como executar

Restaurar dependências e compilar:

```bash
dotnet restore
dotnet build
```

Executar os testes:

```bash
dotnet test
```

## Cenários cobertos

- Busca direta por termo e validação do título da página de resultados.
- Uso do autocompletar para selecionar uma sugestão e validar a pesquisa realizada.
- Navegação para o primeiro resultado orgânico, retorno à página de resultados e validação do estado restaurado.
