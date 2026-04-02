# 🎮 Jogo do Termo em C#

Este projeto é uma implementação em C# de um jogo inspirado no famoso **Termo (Wordle)**. O jogador deve adivinhar uma palavra secreta de 5 letras em até 5 tentativas, recebendo dicas visuais a cada tentativa.

---

## 📌 Funcionalidades

- 🔄 Busca automática de palavras via API
- 🔤 Remoção de acentos para padronização
- 🎨 Sistema de cores para feedback das tentativas
- 🔁 Opção de jogar novamente
- 🌐 Link para consultar o significado da palavra

---

## 🚀 Como funciona o jogo

1. O sistema sorteia uma palavra aleatória de **5 letras** usando uma API online.
2. O jogador tem **5 tentativas** para acertar a palavra.
3. Após cada tentativa, o sistema mostra:
   - 🟩 **Verde** → letra correta na posição correta  
   - 🟨 **Amarelo** → letra existe, mas em posição errada  
   - 🟥 **Vermelho** → letra não existe na palavra  
4. Ao final:
   - Se acertar → mensagem de vitória 🎉  
   - Se errar → mostra a palavra correta 😢  
5. Um link é exibido para consultar o significado da palavra.

---

## 📂 Estrutura do Código

### 🔹 Classe `Program`

Contém toda a lógica do jogo.

---

### 🔹 Método `ObterPalavraAleatoria()`

Responsável por buscar uma palavra aleatória da API:

- Utiliza `HttpClient` para fazer requisição HTTP
- API utilizada: https://api.dicionario-aberto.net/random
- Filtra palavras com exatamente **5 letras**
- Remove acentos da palavra
- Retorna a palavra em maiúsculo

📌 Em caso de erro:
- Retorna a palavra padrão `"TERMO"`

---

### 🔹 Método `RemoverAcentos()`

Remove caracteres acentuados da string usando normalização Unicode.

Exemplo:
```
ação → acao
```

---

### 🔹 Método `MostrarRegras()`

Exibe as regras do jogo no console.

---

### 🔹 Método `ExibirPalavraComCores()`

Mostra o feedback visual da tentativa:

| Cor        | Significado |
|------------|------------|
| 🟩 Verde   | Letra correta e posição correta |
| 🟨 Amarelo | Letra existe, posição errada |
| 🟥 Vermelho| Letra não existe |

---

### 🔹 Método `JogarNovamente()`

Permite ao jogador escolher se deseja jogar novamente.

---

### 🔹 Método `Main()`

Controla todo o fluxo do jogo:

- Inicialização
- Loop principal
- Entrada do usuário
- Verificação de vitória/derrota

---

## 🌐 API utilizada

https://api.dicionario-aberto.net/random

---

## 🔗 Consulta de significado

https://dicio.com.br/{palavra}/

---

## 💡 Melhorias futuras

- Sistema de pontuação
- Interface gráfica
- Histórico de partidas

---

## ▶️ Como executar

```bash
dotnet run
```
