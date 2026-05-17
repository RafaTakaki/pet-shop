# 🔧 Conectar Cloud Run com MongoDB Atlas

## ❌ Problema
Cloud Run usa IPs dinâmicos, então não dá para adicionar um IP fixo no whitelist do MongoDB Atlas.

## ✅ Soluções (em ordem de facilidade)

### Opção 1: Permitir acesso de qualquer IP (RÁPIDO - apenas para testes)
⚠️ **Aviso**: Menos seguro, use apenas em desenvolvimento/testes

1. Acesse [MongoDB Atlas Console](https://account.mongodb.com/account/login)
2. Vá para **Network Access** → **IP Whitelist**
3. Clique em **ADD IP ADDRESS**
4. Digite `0.0.0.0/0` (acesso de qualquer IP)
5. Clique em **Confirm**
6. Aguarde 5-10 minutos para propagar

**Resultado esperado**: O endpoint retornará 200/201, não mais erro 500

---

### Opção 2: VPC Peering (RECOMENDADO - Seguro)
Conecta Cloud Run ao MongoDB via rede privada

#### No Google Cloud:
1. Acesse [Cloud Console → VPC Network → VPC peering](https://console.cloud.google.com/networking/peering)
2. Clique em **CREATE PEERING CONNECTION**
3. Escolha o projeto e VPC do Cloud Run
4. Configure peering com a rede do MongoDB

#### No MongoDB Atlas:
1. Vá para **Network Access** → **VPC Peering**
2. Aceite o peering request do GCP
3. Teste a conexão

---

### Opção 3: Usar VPC Connector (INTERMEDIÁRIA)
1. Crie um VPC Connector no Cloud Run
2. Configure Cloud Run para usar o connector
3. Configure IP estático para o connector
4. Adicione esse IP ao whitelist do MongoDB

---

## 🧪 Testar a Conexão

Após configurar, execute na Cloud Run:

```bash
curl -X 'POST' \
  'https://petshop-senac-XXXXX.europe-west1.run.app/api/Usuario/CriarUsuario' \
  -H 'Content-Type: application/json' \
  -d '{
    "nome": "teste",
    "email": "teste@teste.com.br",
    "senha": "Teste123-",
    "apelido": "teste",
    "dataNascimento": "2026-04-28T00:58:26.852Z"
  }'
```

Você deve receber um **201 Created** ou **400** (validação), não mais **500**.

---

## 📋 Verificar Logs

Se ainda houver erro, veja os logs no Cloud Run:

1. Acesse [Cloud Console → Cloud Run](https://console.cloud.google.com/run)
2. Clique no seu serviço **petshop-senac**
3. Vá para **LOGS** → **All logs**
4. Procure por mensagens de erro do MongoDB

Exemplos de erros comuns:
- `MongoServerSelectionError` → IP não está no whitelist
- `Authentication failed` → Credenciais incorretas
- `ECONNREFUSED` → Firewall bloqueando

---

## 🔐 Credenciais de Teste

Verifique no seu `appsettings.json`:

```json
"MongoDbSettings": {
  "ConnectionString": "mongodb+srv://takaki12:Takaki123@cluster0.qgvlqgb.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0",
  "DatabaseName": "PetShop"
}
```

Valide:
- ✅ Username correto: `takaki12`
- ✅ Senha correta: `Takaki123`
- ✅ Database: `PetShop`
- ✅ Cluster: `cluster0.qgvlqgb.mongodb.net`
