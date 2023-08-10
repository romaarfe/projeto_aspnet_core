<h3 align="center"> 
  ### 🚧 WEB RPG CREATION 🚧
</h3>

---
### 🎮 DESCRIÇÃO DO PROBLEMA 

* Registo de grupos de e gestão dos personagens, com todos seus equipamentos e especialidades. Para uso limitado dos Jogadores e para total controle do Game Master em suas partidas de Roleplaying Game. 

* Utilizar como base um sistema de RPG pré-estabelecido (Isle of Ixx do Into the Odd neste caso), podendo ser facilmente adaptável para outros jogos.  

* Tem como objetivo promover organização e gestão dos grupos e personagens por parte, principalmente do Game Master. Mas, há também, a possibilidade do Jogador (utilizador comum) criar/registar seu próprio personagem de forma “aleatória”.  

* Todo processo assenta em ASP.NET Core 6 em formato MVC (Model, View e Controller), com o Entity Framework e o Identity, além, claro, de C#, HTML, CSS, Bootstrap e um pouco de Javascript. 

* Há conexão com a Base de dados, onde ficam registadas estas informações. 

---
### 👾 TAREFAS IMPLEMENTADAS

- [x] Inicialmente criar o projeto com Identity e configurá-lo para aceitar Login por parte do Game Master e limitar acesso às funções por parte dos Jogadores. 

- [x] Criar os Modelos e suas relações para criação da base de dados e também a criação automática, via Entity Framework, dos Controllers e Views. 

- [x] Realizar CRUD de todas informações relacionadas aos Modelos. 

- [x] Com base nessas informações e com a criação de um Grupo, apresentar os membros desse grupo, através de listas, na página principal Home.  

- [x] Criar métodos que geram valores aleatórios, com base na regra do jogo, para os atributos do Personagem quando tentam criar um personagem. 

- [x] Limitar estes campos para apenas leitura. 

- [x] Limitar a quantidade de Equipamentos associados aos personagens com base no valor final do atributo poder. 

- [x] Qualquer um não logado (Jogador) pode visualizar Home, Personagens, Criar Novos Personagens, e ver Detalhes dos Personagens. 

---
### 🤖 FERRAMENTAS / VERSÕES

*	**Início do planeamento em 2023-04-30**

* **Reformulação do Diagrama de Entidades em 2023-05-05:**
  - [x] Exclusão do Artefato, Bugiganga e PacoteInicial
  - [x] Os três acima foram diferenciados em Tipos dentro do Equipamento
  - [x] Retirada de alguns preenchimentos obrigatório
  - [x] Reestruturação dos Modelos
 
* **Modificações em 2023-05-06:**
  - [x] Alterado alguns tipos para poder incluir mais informações pertinentes
  - [x] Alteração considerável no Equipamento
  - [x] Primeira tentativa, de muitas, frustrada de incluir sistema de Login no projeto já iniciado

* **Modificações em 2023-05-13:**
  - [x] Recriação do projeto “do zero” para inclusão do sistema de Login automático pelo modo Identity
  - [x] Remodelações das Views e nos Controllers

* **Modificações em 2023-05-21:**
  - [x] Verificação do Personagem Controller com a View
  - [x] Tratar dos envios de informações no método Create()
  - [x] Modificar argumentos do Create(POST)
  - [x] Criar variáveis de Classe

* **Modificações em 2023-05-27:**
  - [x] Desisti de múltiplos métodos Random
  - [x] Testei as variáveis de Classe como Static
  - [x] Testei métodos com retorno
  - [x] Objetivo era evitar modificação dos Value através do Inspect via HTML e ao fazer Submit
  - [x] Resolvi da maneira mais simples, porém funcional

* **Modificações em 2023-06-03 e 04:**
  - [x] Modificação do Program.cs para ser Main e facilitar a criação das Roles e contas de Utilizadores
  - [x] Configuração do Program e criação na Base de Dados
  - [x] Alteração do Login, Access Denied, Register... para funcionar direto na página
  - [x] Todo novo Registo é automaticamente um Role "Jogador"

* **Modificações em 2023-06-10 e 11:**
  - [x] Inclusão de tudo relacionado ao Identity
  - [x] Ficaram por configurar Logins Externos e Servidor de envio de Emails
  - [x] A base para tudo isso já funciona, falta apenas a implementação

---
### 📈 ANÁLISE DE DADOS
* **Entity**
  
![Imagem1](https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/6238b49f-0066-47a3-ad60-004cba1c90a6)

* **Identity**
  
![Imagem2](https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/040385c4-7ceb-49bb-86c2-1999850aadc9)

---
### 📊 TABELAS / CAMPOS / TIPOS
* **Grupo**:
  * Id - INT, NN, PK, AI
  * Nome - NVARCHAR(MAX)
  * Descricao - NVARCHAR(MAX)
  * DataFim - DATETIME2(7)
  * DataInicio - DATETIME2(7)

* **Personagem**:
  * Id - INT, NN, PK, AI
  * Nome - NVARCHAR(MAX)
  * Descricao - NVARCHAR(MAX)
  * Nivel - NVARCHAR(MAX)
  * XP - NVARCHAR(MAX)
  * PoderAtual - NVARCHAR(MAX)
  * PoderMaximo - NVARCHAR(MAX)
  * AgirAtual - NVARCHAR(MAX)
  * AgirMaximo - NVARCHAR(MAX)
  * MenteAtual - NVARCHAR(MAX)
  * MenteMaximo - NVARCHAR(MAX)
  * HpAtual - NVARCHAR(MAX)
  * HpMaximo - NVARCHAR(MAX)
  * GrupoId - INT, NN, FK
  * Moeda - NVARCHAR(MAX)

* **Especialidade**:
  * Id - INT, NN, PK, AI
  * Nome - NVARCHAR(MAX)
  * Descricao - NVARCHAR(MAX)
  * PersonagemId - INT, NN, FK

* **Equipamento**:
  * Id - INT, NN, PK, AI
  * Nome - NVARCHAR(MAX)
  * Quantidade - NVARCHAR(MAX)
  * Valor - NVARCHAR(MAX)
  * Tipo - NVARCHAR(MAX)
  * Ataque - NVARCHAR(MAX)
  * Protecao - NVARCHAR(MAX)
  * PoderProfanoId - INT, NN, FK
  * PersonagemId - INT, NN, FK

* **PoderProfano**:
  * Id - INT, NN, PK, AI
  * Nome - NVARCHAR(MAX)
  * Descricao - NVARCHAR(MAX)

---
### 🗃 NAMESPACES
* **WebRPGCreation.Controllers**
*	**WebRPGCreation.Data**
*	**WebRPGCreation.Models**
*	**WebRPGCreation.Migrations**

---
### 📇 CLASSES / MÉTODOS
* **WebRPGCreation**
  * Program

<br>
<img width="538" alt="Captura de ecrã 2023-05-31, às 09 31 20" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/fcb2fcf9-7d77-476c-961a-b3526b0f0fe4">
<br>

* **Data**
  *	ApplicationDbContext
  
* **Models**
  *	Equipamento
  *	Especialidade
  *	Grupo
  *	Personagem
  *	PoderProfano
  *	ErrorViewModel
  
* **Controllers**
  *	***EquipamentoController***
    *	Index( )
    * Details( )
    * Create( )
    * Edit( )
    * Delete( )
    * DeleteConfirmed( )
    * EquipamentoExists( )


<br>
<img width="1404" alt="Captura de ecrã 2023-05-31, às 09 32 08" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/d8d9d389-cea5-417d-829d-38a54b051bbe">
<br>
<br>

* **Controllers**
  * ***EspecialidadeController***
    * Index( )
    * Details( )
    * Create( )
    * Edit( )
    * Delete( )
    * DeleteConfirmed( )
    * EspecialidadeExists( )


<br>
<img width="1403" alt="Captura de ecrã 2023-05-31, às 09 32 17" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/6bd1eca7-25f2-4546-8a62-1d0e0649c523">
<br>
<br>

* **Controllers**
  * ***GrupoController***
    * Index( )
    * Details( )
    * Create( )
    * Edit( )
    * Delete( )
    * DeleteConfirmed( )
    * GrupoExists( )


<br>
<img width="1403" alt="Captura de ecrã 2023-05-31, às 09 31 44" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/5f08d8d9-1a45-4261-a88d-2251f422db8e">
<br>
<br>

* **Controllers**
  * ***PersonagemController***
    * Index( )
    * Details( )
    * Create( )
    * Edit( )
    * Delete( )
    * DeleteConfirmed( )
    * PersonagemExists( )


<br>
<img width="1403" alt="Captura de ecrã 2023-05-31, às 09 31 56" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/f800ba8a-da94-4b03-af4e-0111c9475cd9">
<br>
<br>

* **Controllers**
  * ***PoderProfanoController***
    * Index( )
    * Details( )
    * Create( )
    * Edit( )
    * Delete( )
    * DeleteConfirmed( )
    * PoderProfanoeExists( )
 
 
<br>
<img width="1399" alt="Captura de ecrã 2023-05-31, às 09 32 27" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/0aa4ca8c-4f7e-4ea1-a3a1-20dd8283fbe5">
<br>
<br>

 * **Controllers**
   * ***HomeController***
     * Index( )
     * Error( )


<br>
<img width="1406" alt="Captura de ecrã 2023-05-31, às 09 31 03" src="https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/5169dd1d-610f-443a-b10f-a3302bcef2de">
<br>
<br>

---
### 📝 OUTRAS INFORMAÇÕES
* **Material Extra** *(wwwroot)*:
  *	Sketchy Theme (Bootstrap)
  *	Image

---
### 🎥 APRESENTAÇÃO
https://github.com/romaarfe/projeto_aspnet_core/assets/91450312/7a53b8ec-aa88-4ec5-ba53-6c001ee88748

---
#### 👨‍💻 PROJETO DE 50 HORAS EM ASP.NET CORE
##### 📖 PARA CURSO DE PROGRAMADOR DE INFORMÁTICA - IEFP BRAGA
