# PROJETO DE 50 HORAS EM C#
## PARA CURSO DE PROGRAMADOR DE INFORMÁTICA - IEFP BRAGA

### Web RPG Creation

### Descrição do problema 

* Registo de grupos de e gestão dos personagens, com todos seus equipamentos e especialidades. Para uso limitado dos Jogadores e para total controle do Game Master em suas partidas de Roleplaying Game. 

* Utilizar como base um sistema de RPG pré-estabelecido (Isle of Ixx do Into the Odd neste caso), podendo ser facilmente adaptável para outros jogos.  

* Tem como objetivo promover organização e gestão dos grupos e personagens por parte, principalmente do Game Master. Mas, há também, a possibilidade do Jogador (utilizador comum) criar/registar seu próprio personagem de forma “aleatória”.  

* Todo processo assenta em ASP.NET Core 6 em formato MVC (Model, View e Controller), com o Entity Framework e o Identity, além, claro, de C#, HTML, CSS, Bootstrap e um pouco de Javascript. 

* Há conexão com a Base de dados, onde ficam registadas estas informações. 

### Lista de tarefas a serem implementadas 

* Inicialmente criar o projeto com Identity e configurá-lo para aceitar Login por parte do Game Master e limitar acesso às funções por parte dos Jogadores. 

* Criar os Modelos e suas relações para criação da base de dados e também a criação automática, via Entity Framework, dos Controllers e Views. 

* Realizar CRUD de todas informações relacionadas aos Modelos. 

* Com base nessas informações e com a criação de um Grupo, apresentar os membros desse grupo, através de listas, na página principal Home.  

* Criar métodos que geram valores aleatórios, com base na regra do jogo, para os atributos do Personagem quando tentam criar um personagem. 

* Limitar estes campos para apenas leitura. 

* Limitar a quantidade de Equipamentos associados aos personagens com base no valor final do atributo poder. 

* Qualquer um não logado (Jogador) pode visualizar Home, Personagens, Criar Novos Personagens, e ver Detalhes dos Personagens. 

### Ferramentas e versões

*	Início do planeamento em 2023-04-30

* Reformulação do Diagrama de Entidades em 2023-05-05:
  * Exclusão do Artefato, Bugiganga e PacoteInicial
  * Os três acima foram diferenciados em Tipos dentro do Equipamento
  * Retirada de alguns preenchimentos obrigatório
  * Reestruturação dos Modelos
 
* Modificações em 2023-05-06:
  * Alterado alguns tipos para poder incluir mais informações pertinentes
  * Alteração considerável no Equipamento
  * Primeira tentativa, de muitas, frustrada de incluir sistema de Login no projeto já iniciado

* Modificações em 2023-05-13:
  * Recriação do projeto “do zero” para inclusão do sistema de Login automático pelo modo Identity
  * Remodelações das Views e nos Controllers

### Análise de dados

### Lista de tabelas, campos e tipos
* Grupo:
  * Id - INT, NN, PK, AI
o	Nome - NVARCHAR(MAX)
o	Descricao - NVARCHAR(MAX)
o	DataFim - DATETIME2(7)
o	DataInicio - DATETIME2(7)

•	Personagem:
o	Id - INT, NN, PK, AI
o	Nome - NVARCHAR(MAX)
o	Descricao - NVARCHAR(MAX)
o	Nivel - NVARCHAR(MAX)
o	XP - NVARCHAR(MAX)
o	PoderAtual - NVARCHAR(MAX)
o	PoderMaximo - NVARCHAR(MAX)
o	AgirAtual - NVARCHAR(MAX)
o	AgirMaximo - NVARCHAR(MAX)
o	MenteAtual - NVARCHAR(MAX)
o	MenteMaximo - NVARCHAR(MAX)
o	HpAtual - NVARCHAR(MAX)
o	HpMaximo - NVARCHAR(MAX)
o	GrupoId - INT, NN, FK
o	Moeda - NVARCHAR(MAX)

•	Especialidade:
o	Id - INT, NN, PK, AI
o	Nome - NVARCHAR(MAX)
o	Descricao - NVARCHAR(MAX)
o	PersonagemId - INT, NN, FK

•	Equipamento:
o	Id - INT, NN, PK, AI
o	Nome - NVARCHAR(MAX)
o	Quantidade - NVARCHAR(MAX)
o	Valor - NVARCHAR(MAX)
o	Tipo - NVARCHAR(MAX)
o	Ataque - NVARCHAR(MAX)
o	Protecao - NVARCHAR(MAX)
o	PoderProfanoId - INT, NN, FK
o	PersonagemId - INT, NN, FK


•	PoderProfano:
o	Id - INT, NN, PK, AI
o	Nome - NVARCHAR(MAX)
o	Descricao - NVARCHAR(MAX)


### Namespaces
•	WebRPGCreation.Controllers
•	WebRPGCreation.Data
•	WebRPGCreation.Models
•	WebRPGCreation.Migrations

### Classes e métodos
	WebRPGCreation
•	Program

	Data
•	ApplicationDbContext

	Models
•	Equipamento
•	Especialidade
•	Grupo
•	Personagem
•	PoderProfano
•	ErrorViewModel

	Controllers
•	EquipamentoController
o	Index( )
o	Details( )
o	Create( )
o	Edit( )
o	Delete( )
o	DeleteConfirmed( )
o	EquipamentoExists( )

•	EspecialidadeController
o	Index( )
o	Details( )
o	Create( )
o	Edit( )
o	Delete( )
o	DeleteConfirmed( )
o	EspecialidadeExists( )

•	GrupoController
o	Index( )
o	Details( )
o	Create( )
o	Edit( )
o	Delete( )
o	DeleteConfirmed( )
o	GrupoExists( )


•	PersonagemController
o	Index( )
o	Details( )
o	Create( )
o	Edit( )
o	Delete( )
o	DeleteConfirmed( )
o	PersonagemExists( )

•	PoderProfanoController
o	Index( )
o	Details( )
o	Create( )
o	Edit( )
o	Delete( )
o	DeleteConfirmed( )
o	PoderProfanoeExists( )

•	HomeController
o	Index( )
o	Error( )


### Outras informações
•	Material Extra (wwwroot):
o	Sketchy Theme (Bootstrap)
o	Image


