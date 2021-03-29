# ClientesAPI
API simples com CRUD de clientes(id,nome,cpf e telefone), um endpoint api/auth que recebe um obj{username,password}, retorna um token, necassario para post,put e delete

execute ClientesAPI.sql para criar o DB da API(MSSQL Server) localmente

com a API rodando

estou utlizando swagger logo no endereço https://localhost:44372/api/swagger/index.html será encontrada a UI do swagger

bem como os endpoints e schemas

1 - execute post {
  "username": "admin",
  "password": "j#$rTfz!329)"  
}

2 - no endpoint api/auth
irá obter:
{
  "user": {
    "username": "admin",
    "password": "",
    "role": "manager"
  },
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWRtaW4iLCJyb2xlIjoibWFuYWdlciIsIm5iZiI6MTYxNjk4NTE1NSwiZXhwIjoxNjE2OTkyMzU1LCJpYXQiOjE2MTY5ODUxNTV9.TpvocaGGJg0rTgb9ukwDQ8I9hyo91tlcOU71qPMFAzQ"
}

3 - use o token para fazer (post,put,delete)api/clientes com o header 

"Authorization": bearer 

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWRtaW4iLCJyb2xlIjoibWFuYWdlciIsIm5iZiI6MTYxNjk4NTE1NSwiZXhwIjoxNjE2OTkyMzU1LCJpYXQiOjE2MTY5ODUxNTV9.TpvocaGGJg0rTgb9ukwDQ8I9hyo91tlcOU71qPMFAzQ
