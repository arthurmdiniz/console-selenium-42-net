# console-selenium-42-net

Projeto de automação de testes de interface com **.NET 10**, **Selenium WebDriver** e **MSTest**, organizado de forma modular para facilitar manutenção e evolução.

## Estrutura da solução

- `ConsoleSelenium.slnx`: solução .NET.
- `ConsoleSelenium/`: aplicação console base do projeto.
- `ConsoleSelenium.Tests/`: projeto de testes automatizados.
  - `MSTestSettings.cs`: configuração do MSTest (execução paralela por classe).
  - `WebFormularioDesafioTest.cs`: cenários de teste de busca no DuckDuckGo.

## Tecnologias e pacotes

- [.NET 10](https://dotnet.microsoft.com/download)
- [Selenium.WebDriver](https://www.nuget.org/packages/Selenium.WebDriver) 4.52.0
- [Selenium.Support](https://www.nuget.org/packages/Selenium.Support) 4.52.0
- [MSTest](https://www.nuget.org/packages/MSTest) 4.0.2

## Pré-requisitos

- .NET SDK 10.0+
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

Para executar apenas a classe de testes do desafio:

```bash
dotnet test --filter "FullyQualifiedName~WebFormularioDesafioTest"
```

## Cenários cobertos

Os cenários estão implementados em `ConsoleSelenium.Tests/WebFormularioDesafioTest.cs`:

- **Parte 1 — Pesquisa básica:** acessa o DuckDuckGo, pesquisa por `Selenium WebDriver C#` e valida o título da página de resultados.
- **Parte 2 — Autocompletar:** digita `C# unit testing`, aguarda as sugestões dinâmicas, seleciona a terceira opção e valida o redirecionamento para a busca escolhida.
- **Parte 3 — Histórico e navegação:** pesquisa por `Automated Testing`, clica no primeiro resultado orgânico, valida a saída do domínio do DuckDuckGo, retorna com `Navigate().Back()` e confirma a restauração da página de resultados.
