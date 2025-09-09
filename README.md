# 🚗 Minimal API - Sistema de Gerenciamento de Veículos

Uma API RESTful moderna desenvolvida com .NET 8 usando Minimal APIs para gerenciamento de administradores e veículos, com autenticação JWT e banco de dados MySQL.

## 📋 Índice

- [Características](#-características)
- [Tecnologias](#-tecnologias)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação](#-instalação)
- [Configuração](#-configuração)
- [Executando a Aplicação](#-executando-a-aplicação)
- [API Endpoints](#-api-endpoints)
- [Autenticação](#-autenticação)
- [Banco de Dados](#-banco-de-dados)
- [Testes](#-testes)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Contribuição](#-contribuição)
- [Licença](#-licença)

## ✨ Características

- 🔐 **Autenticação JWT** - Sistema seguro de autenticação com tokens
- 👥 **Gerenciamento de Administradores** - CRUD completo com diferentes perfis
- 🚙 **Gestão de Veículos** - Cadastro e consulta de veículos
- 📊 **Swagger/OpenAPI** - Documentação interativa da API
- 🗄️ **Entity Framework Core** - ORM com suporte a migrations
- 🔍 **Auditoria Automática** - Campos de criação e atualização preenchidos automaticamente
- ⚡ **Performance Otimizada** - Queries com AsNoTracking e índices otimizados
- 🧪 **Testes Unitários** - Projeto de testes incluído
- 📱 **CORS Habilitado** - Pronto para integração com frontend

## 🛠️ Tecnologias

- **Framework**: .NET 8.0
- **Arquitetura**: Minimal APIs
- **Banco de Dados**: MySQL 8.0+
- **ORM**: Entity Framework Core 8.0
- **Autenticação**: JWT Bearer Tokens
- **Documentação**: Swagger/OpenAPI
- **Testes**: MSTest

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- [Git](https://git-scm.com/)
- Editor de código ([Visual Studio Code](https://code.visualstudio.com/) recomendado)

## 🚀 Instalação

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd minimal-api
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Configure o banco de dados MySQL**
   - Certifique-se de que o MySQL está executando
   - Crie um banco de dados chamado `minimal_api`

## ⚙️ Configuração

1. **Configure a string de conexão**
   
   Edite o arquivo `Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "MySql": "Server=localhost;Database=minimal_api;Uid=seu_usuario;Pwd=sua_senha;"
     },
     "Jwt": "sua_chave_jwt_super_secreta_aqui"
   }
   ```

2. **Execute as migrations**
   ```bash
   dotnet ef database update -p Api
   ```

## 🏃‍♂️ Executando a Aplicação

1. **Executar em modo de desenvolvimento**
   ```bash
   dotnet run --project Api
   ```

2. **A aplicação estará disponível em:**
   - API: `http://localhost:5004`
   - Swagger: `http://localhost:5004/swagger`

## 📚 API Endpoints

### 🏠 Home
- `GET /` - Informações da API

### 👥 Administradores
- `POST /administradores/login` - Fazer login
- `GET /administradores` - Listar administradores (requer autenticação)
- `GET /administradores/{id}` - Obter administrador por ID
- `POST /administradores` - Criar administrador (requer perfil Admin)

### 🚗 Veículos
- `GET /veiculos` - Listar veículos
- `GET /veiculos/{id}` - Obter veículo por ID
- `POST /veiculos` - Criar veículo (requer autenticação)
- `PUT /veiculos/{id}` - Atualizar veículo (requer autenticação)
- `DELETE /veiculos/{id}` - Excluir veículo (requer perfil Admin)

## 🔐 Autenticação

A API usa autenticação JWT. Para acessar endpoints protegidos:

1. **Faça login para obter o token:**
   ```bash
   POST /administradores/login
   {
     "email": "administrador@teste.com",
     "senha": "123456"
   }
   ```

2. **Use o token no header Authorization:**
   ```
   Authorization: Bearer {seu_token_aqui}
   ```

### 👤 Perfis de Usuário
- **Adm**: Acesso total ao sistema
- **Editor**: Acesso limitado (não pode deletar)

## 🗄️ Banco de Dados

### Tabelas Principais

#### Administradores
```sql
- Id (int, PK, Identity)
- Email (varchar(255), único)
- Senha (varchar(255))
- Perfil (varchar(10)) # 'Adm' ou 'Editor'
- DataCriacao (datetime)
- DataAtualizacao (datetime)
- CriadoPor (varchar(100))
- AtualizadoPor (varchar(100))
```

#### Veiculos
```sql
- Id (int, PK, Identity)
- Nome (varchar(150))
- Marca (varchar(100))
- Ano (int)
- DataCriacao (datetime)
- DataAtualizacao (datetime)
- CriadoPor (varchar(100))
- AtualizadoPor (varchar(100))
```

### Migrations

Para criar novas migrations:
```bash
# Criar migration
dotnet ef migrations add NomeDaMigration -p Api --context DbContexto

# Aplicar migration
dotnet ef database update -p Api --context DbContexto

# Ver migrations pendentes
dotnet ef migrations list -p Api --context DbContexto
```

## 🧪 Testes

Execute os testes unitários:

```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 📁 Estrutura do Projeto

```
minimal-api/
├── Api/                                    # Projeto principal da API
│   ├── Dominio/                           # Camada de domínio
│   │   ├── DTOs/                          # Data Transfer Objects
│   │   ├── Entidades/                     # Entidades do banco
│   │   ├── Enuns/                         # Enumerações
│   │   ├── Interfaces/                    # Interfaces de serviços
│   │   ├── ModelViews/                    # ViewModels
│   │   └── Servicos/                      # Implementação dos serviços
│   ├── Infraestrutura/                    # Camada de infraestrutura
│   │   └── Db/                            # Configurações do banco
│   │       ├── Configurations/            # Configurações EF das entidades
│   │       ├── Interceptors/              # Interceptors EF
│   │       ├── DbContexto.cs              # Contexto do EF
│   │       └── DbContextoExtensions.cs    # Extensões de query
│   ├── Migrations/                        # Migrations do EF
│   ├── Properties/                        # Configurações de launch
│   ├── Program.cs                         # Ponto de entrada
│   ├── Startup.cs                         # Configuração dos endpoints
│   └── appsettings.json                   # Configurações da aplicação
├── Test/                                  # Projeto de testes
│   ├── Domain/                            # Testes de domínio
│   ├── Helpers/                           # Utilitários de teste
│   ├── Mocks/                             # Mocks para testes
│   └── Requests/                          # Testes de requisições
└── README.md                              # Este arquivo
```

## 🔧 Configurações Avançadas

### Performance
- **AsNoTracking**: Queries de leitura otimizadas
- **Índices**: Criados automaticamente via migrations
- **Connection Pooling**: Configurado no EF Core
- **Retry Policy**: Reconexão automática em falhas

### Auditoria
- **Interceptor Automático**: Preenche campos de auditoria
- **Timestamps UTC**: Padronização de fuso horário
- **Rastreamento de Usuário**: Identificação de quem fez alterações

### Segurança
- **JWT Tokens**: Autenticação stateless
- **Perfis de Acesso**: Controle granular de permissões
- **CORS**: Configurado para desenvolvimento
- **Validation**: DTOs com validação automática

## 📊 Dados Iniciais

A aplicação vem com um administrador padrão:
- **Email**: `administrador@teste.com`
- **Senha**: `123456`
- **Perfil**: `Adm`

⚠️ **Importante**: Altere essas credenciais em produção!

## 🚀 Deploy

### Desenvolvimento
```bash
dotnet run --project Api --environment Development
```

### Produção
```bash
dotnet publish Api -c Release -o ./publish
dotnet ./publish/mininal-api.dll
```

### Docker (Opcional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./publish .
ENTRYPOINT ["dotnet", "mininal-api.dll"]
```

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

### Padrões de Commit
- `feat:` - Nova funcionalidade
- `fix:` - Correção de bug
- `docs:` - Alterações na documentação
- `refactor:` - Refatoração de código
- `test:` - Adição de testes

## 📝 Roadmap

- [ ] Implementar soft delete
- [ ] Adicionar paginação automática
- [ ] Sistema de logs estruturados
- [ ] Implementar cache Redis
- [ ] Rate limiting
- [ ] Health checks
- [ ] Metrics e observabilidade

## 🐛 Problemas Conhecidos

- A senha é armazenada em texto plano (implementar hash em produção)
- Logs sensíveis em desenvolvimento (desabilitar em produção)

## 📞 Suporte

Para suporte ou dúvidas:
- Abra uma [issue](../../issues)
- Consulte a [documentação do Swagger](http://localhost:5004/swagger)

---

## 🙏 Agradecimentos

- DIO
- Contributors

---

**Desenvolvido com ❤️ usando .NET 8 e Minimal APIs**
