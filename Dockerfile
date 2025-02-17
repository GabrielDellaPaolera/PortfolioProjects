FROM golang:1.21-alpine

# Definir diretório de trabalho
WORKDIR /app

# Copiar arquivos do projeto para o contêiner
COPY . .

# Baixar dependências (se houver)
RUN go mod tidy || true

# Compilar o binário
RUN go build -o main .

# Expor a porta usada pelo servidor
EXPOSE 8080

# Comando para rodar a aplicação
CMD ["./main"]
