# Containerização com Docker - Pet Shop API

## 📋 Visão Geral

Este documento descreve como containerizar e executar a Pet Shop API usando Docker e Docker Compose.

## 🐳 Estrutura de Arquivos Docker

- **Dockerfile**: Multi-stage build para otimizar o tamanho da imagem
- **.dockerignore**: Arquivos a serem excluídos da build
- **docker-compose.yml**: Orquestração de containers (API + MongoDB)
- **appsettings.Production.json**: Configurações para ambiente de produção

## 🚀 Quick Start

### Pré-requisitos
- Docker instalado ([Download Docker Desktop](https://www.docker.com/products/docker-desktop))
- Docker Compose (incluído no Docker Desktop)

### Executar a Aplicação Completa

```bash
# Na raiz do projeto
docker-compose up -d
```

A API estará disponível em: `http://localhost:8080`
Swagger UI: `http://localhost:8080/swagger/index.html`

### Parar a Aplicação

```bash
docker-compose down
```

### Remover Volumes de Dados (limpar banco)

```bash
docker-compose down -v
```

## 🔨 Construir Imagem Manualmente

```bash
# Build da imagem
docker build -t petshop-api:latest .

# Run do container
docker run -d \
  --name petshop-api \
  -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e MongoDbSettings__ConnectionString=mongodb://localhost:27017 \
  -e MongoDbSettings__DatabaseName=PetShop \
  petshop-api:latest
```

## 📝 Variáveis de Ambiente

O `docker-compose.yml` configura automaticamente:

| Variável | Descrição | Valor Padrão |
|----------|-----------|--------------|
| `ASPNETCORE_ENVIRONMENT` | Ambiente da aplicação | `Production` |
| `MongoDbSettings__ConnectionString` | String de conexão MongoDB | `mongodb://takaki12:Takaki123@mongodb:27017` |
| `MongoDbSettings__DatabaseName` | Nome do banco de dados | `PetShop` |
| `JwtSettings__SecretKey` | Chave JWT | Configurada |
| `EmailSettings__SmtpServer` | Servidor SMTP | `smtp.gmail.com` |
| `PetApiSettings__ApiKey` | API Key externo | Configurada |

## 🔐 Segurança - Importante!

⚠️ **NÃO comitar credentials em repositório público!**

Use um arquivo `.env` para variáveis sensíveis:

```bash
# .env (adicione ao .gitignore)
MONGO_ROOT_USERNAME=seu_usuario
MONGO_ROOT_PASSWORD=sua_senha
JWT_SECRET_KEY=sua_chave_secreta
SMTP_PASSWORD=sua_senha_smtp
PET_API_KEY=sua_api_key
```

Depois atualize o `docker-compose.yml`:

```yaml
environment:
  - MongoDbSettings__ConnectionString=mongodb://${MONGO_ROOT_USERNAME}:${MONGO_ROOT_PASSWORD}@mongodb:27017
  - EmailSettings__SenderPassword=${SMTP_PASSWORD}
```

Execute com:
```bash
docker-compose --env-file .env up -d
```

## 🩺 Health Checks

Os containers têm health checks configurados:

```bash
# Ver status dos containers
docker-compose ps

# Ver logs de um container específico
docker-compose logs petshop-api
docker-compose logs mongodb
```

## 🗄️ MongoDB via Docker

O MongoDB será executado no container e terá:
- **Host**: `mongodb` (dentro da rede Docker)
- **Porta**: `27017`
- **Usuário**: `takaki12`
- **Senha**: `Takaki123`
- **Banco**: `PetShop`

### Acessar MongoDB via CLI

```bash
# Dentro do container
docker-compose exec mongodb mongosh

# Ou via cliente local
mongosh "mongodb://takaki12:Takaki123@localhost:27017"
```

## 📊 Monitoramento

```bash
# Ver uso de recursos dos containers
docker stats

# Ver logs em tempo real
docker-compose logs -f petshop-api

# Ver histórico completo
docker-compose logs petshop-api
```

## 🔄 Atualizações

Para atualizar a aplicação:

```bash
# Parar containers
docker-compose down

# Fazer pull de novas alterações de código
git pull

# Rebuild da imagem
docker-compose build --no-cache

# Iniciar novamente
docker-compose up -d
```

## 📦 Publicar Imagem em Registro

### Docker Hub

```bash
# Login
docker login

# Tag da imagem
docker tag petshop-api:latest seu-usuario/petshop-api:latest

# Push
docker push seu-usuario/petshop-api:latest

# Usar em outro lugar
docker pull seu-usuario/petshop-api:latest
```

### Azure Container Registry

```bash
# Login
az acr login --name seu-registry

# Tag
docker tag petshop-api:latest seu-registry.azurecr.io/petshop-api:latest

# Push
docker push seu-registry.azurecr.io/petshop-api:latest
```

## 🐛 Troubleshooting

### API não consegue conectar ao MongoDB

```bash
# Verificar se MongoDB está rodando
docker-compose ps

# Verificar logs
docker-compose logs mongodb

# Verificar nome da rede
docker network ls
docker network inspect petshop-network
```

### Porta já em uso

```bash
# Se porta 8080 está em uso, mude em docker-compose.yml:
# ports:
#   - "8081:8080"

# Ou libere a porta
lsof -i :8080  # Linux/Mac
netstat -ano | findstr :8080  # Windows
```

### Reconstruir tudo do zero

```bash
docker-compose down -v
docker system prune -a
docker-compose up -d --build
```

## 📚 Referências

- [Documentação Docker](https://docs.docker.com/)
- [Docker Compose Reference](https://docs.docker.com/compose/compose-file/)
- [ASP.NET Core in Docker](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/container-docker-introduction/)
- [MongoDB Docker Hub](https://hub.docker.com/_/mongo)

## ✅ Checklist de Produção

- [ ] Usar secrets/environment variables para credentials
- [ ] Configurar volumes persistentes para dados
- [ ] Configurar backups automáticos do MongoDB
- [ ] Limitar recursos (CPU/Memory) dos containers
- [ ] Usar registros privados para imagens
- [ ] Configurar CI/CD com Docker
- [ ] Monitorar logs e métricas
- [ ] Implementar autoscaling se necessário
