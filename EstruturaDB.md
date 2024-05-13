# Estrutura do Banco de Dados


```sql
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY,
    Nome VARCHAR(MAX),
    CPF VARCHAR(14),
    Telefone VARCHAR(20),
    Email VARCHAR(100),
    Senha VARCHAR(100),
    Endereco VARCHAR(MAX),
    Numero VARCHAR(10),
    Cidade VARCHAR(MAX),
    Estado VARCHAR(2),
    CEP VARCHAR(10)
);
```

```sql
CREATE TABLE Produtos (
    Id INT PRIMARY KEY,
    Nome NVARCHAR(255) NOT NULL,
    Preco DECIMAL(18, 2) NOT NULL,
    Descricao NVARCHAR(MAX),
    ImagemEmBase64 NVARCHAR(MAX)
);
```

```sql
CREATE TABLE PedidoItem (
    Id INT PRIMARY KEY,
    PedidoId INT NOT NULL,
    ProdutoId INT NOT NULL,
    Qtde INT NOT NULL,
    CONSTRAINT FK_PedidoItem_Pedido FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    CONSTRAINT FK_PedidoItem_Produto FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
);
```

```sql
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY,
    Data DATETIME NOT NULL
);
```

### Stored Procedures de Usuarios

```sql
create procedure spInsert_Usuarios
(
    @id int,
    @nome varchar(max),
    @cpf varchar(14),
    @telefone varchar(20),
    @email varchar(100),
    @endereco varchar(max),
    @cidade varchar(max),
    @estado varchar(2),
    @cep varchar(10)
)
as
begin
    insert into Usuarios
    (id, nome, cpf, telefone, email, endereco, cidade, estado, cep)
    values
    (@id, @nome, @cpf, @telefone, @email, @endereco, @cidade, @estado, @cep)
end
GO
```

```sql
create procedure spUpdate_Usuarios
(
    @id int,
    @nome varchar(max),
    @cpf varchar(14),
    @telefone varchar(20),
    @email varchar(100),
    @endereco varchar(max),
    @cidade varchar(max),
    @estado varchar(2),
    @cep varchar(10)
)
as
begin
    update Usuarios set
    nome = @nome,
    cpf = @cpf,
    telefone = @telefone,
    email = @email,
    endereco = @endereco,
    cidade = @cidade,
    estado = @estado,
    cep = @cep
    where id = @id
end
GO
```

### Stored Procedures de Produtos

```sql
CREATE PROCEDURE spInsert_Produtos
(
    @Id INT,
    @Nome NVARCHAR(255),
    @Preco DECIMAL(18, 2),
    @Descricao NVARCHAR(MAX),
    @ImagemEmBase64 NVARCHAR(MAX)
)
AS
BEGIN
    INSERT INTO Produtos
    (Id, Nome, Preco, Descricao, ImagemEmBase64)
    VALUES
    (@Id, @Nome, @Preco, @Descricao, @ImagemEmBase64)
END
GO
```
```sql
CREATE PROCEDURE spUpdate_Produtos
(
    @Id INT,
    @Nome NVARCHAR(255),
    @Preco DECIMAL(18, 2),
    @Descricao NVARCHAR(MAX),
    @ImagemEmBase64 NVARCHAR(MAX)
)
AS
BEGIN
    UPDATE Produtos SET
    Nome = @Nome,
    Preco = @Preco,
    Descricao = @Descricao,
    ImagemEmBase64 = @ImagemEmBase64
    WHERE Id = @Id
END
GO
```

## Stored Procedures Genéricas

### sp's Genéricas

```sql
create procedure spDelete
(
    @id int ,
    @tabela varchar(max)
)
as
begin
    declare @sql varchar(max);
    set @sql = ' delete ' + @tabela +
    ' where id = ' + cast(@id as varchar(max))
    exec(@sql)
end
GO

create procedure spConsulta
(
    @id int ,
    @tabela varchar(max)
)
as
begin
    declare @sql varchar(max);
    set @sql = 'select * from ' + @tabela +
    ' where id = ' + cast(@id as varchar(max))
    exec(@sql)
end
GO

create procedure spListagem
(
    @tabela varchar(max),
    @ordem varchar(max)
)
as
begin
    exec('select * from ' + @tabela +
    ' order by ' + @ordem)
end
GO

create procedure spProximoId
(
    @tabela varchar(max)
)
as
begin
    exec('select isnull(max(id) +1, 1) as MAIOR from '
    +@tabela)
end
GO

```
