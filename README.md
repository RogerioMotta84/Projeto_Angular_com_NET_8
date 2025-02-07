### Roteiro Projeto Angular com .NET 8

## 1. Requisitos de Ambiente
Antes de iniciar o desenvolvimento, é essencial garantir que o ambiente de desenvolvimento esteja configurado corretamente. Os seguintes softwares e ferramentas devem estar instalados:

## 1.1 Ferramentas Necessárias
Visual Studio 2022 (versão mais recente com suporte ao .NET 8)
Workloads necessários:

ASP.NET e desenvolvimento web
Node.js (versão 18 ou superior)
Instalar via site oficial

Angular CLI (versão mais recente)
Instalação via terminal:
***npm install -g @angular/cli***

## 2. Criando o Projeto .NET 8 no Visual Studio
## 2.1 Criando a Solução no Visual Studio

Abra o Visual Studio 2022.
Clique em Criar um novo projeto.
Escolha o modelo de projeto ASP.NET Core Web API e clique em Avançar.
Defina o nome da solução (exemplo: ProjetoAPI) e a localização do projeto.
Escolha .NET 8 (Long Term Support - LTS) como framework.
Desmarque as opções de Docker e Autenticação, pois não serão utilizadas.
Clique em Criar para gerar o projeto.

## 2.2 Estrutura Básica do Projeto
Após a criação, a estrutura inicial do projeto conterá:

Controllers/: Contém os endpoints da API.
Program.cs: Arquivo principal de inicialização do projeto.
appsettings.json: Configurações da aplicação.

## 2.3 Criando um Endpoint de Exemplo
1. No Visual Studio, dentro da pasta Controllers, crie um novo arquivo chamado LoginController.cs.
2. Adicione o código básico para criar um endpoint de login:
3. Execute o projeto pressionando Ctrl + F5 ou o botão executar e acesse https://localhost:5001/swagger para testar o endpoint.

## 3.2 Configurando a Comunicação com a API
● Crie um serviço Angular para chamadas HTTP:
***ng generate service services/auth***

● No arquivo auth.service.ts, implemente a chamada à API:
● Instale o módulo HTTP Client no Angular caso não esteja configurado

## 3.3 Criando a Tela de Login
Crie um novo componente para a tela de login: ng generate component pages/login
● No arquivo login.component.html
No arquivo login.component.ts, implemente a lógica:

## 3.4 Executando o Projeto Angular
Execute o projeto do frontend com o comando no terminal :
***ng serve***
Acesse no navegador http://localhost:4200 para visualizar a aplicação.


## A funcionalidade de cadastro de usuários permite que novos usuários se registrem na plataforma através de um endpoint POST.

### Backend (API)
No backend, o controlador responsável pelo cadastro de usuários está disponível no endpoint /api/register. A API espera que o corpo da requisição contenha o nome de usuário e a senha do novo usuário.

Método HTTP: POST
Endpoint: /api/register
Requisição: Nome de usuário e senha são obrigatórios.
Resposta: Se o nome de usuário já existir, a API retornará um erro. Caso contrário, o usuário será registrado com sucesso.
A documentação interativa da API está disponível através do Swagger, permitindo testar o cadastro diretamente na interface.

### Frontend (Angular)
No frontend, o componente LoginComponent gerencia o formulário de cadastro de usuários. Ele utiliza o serviço AuthService para realizar a requisição POST para o backend, enviando o nome de usuário e a senha.


## Implementação da Simulação do Banco de Dados
Este projeto utiliza uma lista estática na memória para simular um banco de dados. Isso permite testar as funcionalidades de login e registro de usuários sem a necessidade de um banco de dados real.

### Como funciona a simulação?
A lista usuarios atua como um banco de dados em memória, armazenando temporariamente os dados dos usuários cadastrados.
Quando um novo usuário se registra, seus dados são adicionados a essa lista.
No login, o sistema verifica se o nome de usuário e a senha informados correspondem a um dos registros da lista.
Limitações dessa abordagem
Persistência dos dados: Como os dados são armazenados em uma lista estática, eles são perdidos sempre que a aplicação é reiniciada.
Banco de dados real: Não há persistência real dos dados, pois não há conexão com um banco de dados físico.
Uso para testes: Essa solução é útil apenas para testes e desenvolvimento inicial. Para produção, é recomendado o uso de um banco de dados real, como SQL Server, MySQL, ou MongoDB.
Estrutura do Código

### 1. Lista de usuários simulando o banco de dados

// Lista estática para armazenar os usuários

***private static List<LoginModel> usuarios = new List<LoginModel>();***

### 2. Registro de usuário
Verifica se o nome de usuário já existe. Se não existir, um novo usuário é adicionado à lista.

var usuarioExistente = usuarios.FirstOrDefault(u => u.Username == model.Username);
if (usuarioExistente != null)
{
    return BadRequest("Usuário já existe");
}

usuarios.Add(new LoginModel { Username = model.Username, Password = model.Password });

### 3. Autenticação de usuário (login)
Verifica se o usuário e a senha informados estão na lista. Se encontrar um registro correspondente, o login é bem-sucedido.


var usuarioExistente = usuarios.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
if (usuarioExistente == null)
{
    return Unauthorized("Nome de usuário ou senha inválidos");
}


***Com isso, você pode testar a API sem a necessidade de configurar um banco de dados real, e a solução está pronta para ser expandida para integrar um banco de dados físico mais tarde, quando necessário.***

