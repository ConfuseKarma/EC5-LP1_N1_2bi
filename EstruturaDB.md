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
    Imagem VARBINARY(MAX) NULL
);
```

```sql
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY,
    Data DATETIME NOT NULL
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

### Stored Procedures de Usuarios

```sql
CREATE PROCEDURE spInsert_Usuarios
(
    @id INT,
    @nome VARCHAR(MAX),
    @cpf VARCHAR(14),
    @telefone VARCHAR(20),
    @email VARCHAR(100),
    @endereco VARCHAR(MAX),
    @numero VARCHAR(10),
    @cidade VARCHAR(MAX),
    @estado VARCHAR(2),
    @cep VARCHAR(10),
    @senha VARCHAR(100) 
)
AS
BEGIN
    INSERT INTO Usuarios
    (id, nome, cpf, telefone, email, endereco, numero, cidade, estado, cep, senha)
    VALUES
    (@id, @nome, @cpf, @telefone, @email, @endereco, @numero, @cidade, @estado, @cep, @senha);
END
GO


```

```sql
CREATE PROCEDURE spUpdate_Usuarios
(
    @id INT,
    @nome VARCHAR(MAX),
    @cpf VARCHAR(14),
    @telefone VARCHAR(20),
    @email VARCHAR(100),
    @endereco VARCHAR(MAX),
    @cidade VARCHAR(MAX),
    @estado VARCHAR(2),
    @cep VARCHAR(10),
    @senha VARCHAR(100) 
)
AS
BEGIN
    UPDATE Usuarios SET
    nome = @nome,
    cpf = @cpf,
    telefone = @telefone,
    email = @email,
    endereco = @endereco,
    cidade = @cidade,
    estado = @estado,
    cep = @cep,
    senha = @senha 
    WHERE id = @id;
END
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
    @Imagem VARBINARY(MAX) NULL
)
AS
BEGIN
    INSERT INTO Produtos
    (Id, Nome, Preco, Descricao, Imagem)
    VALUES
    (@Id, @Nome, @Preco, @Descricao, @Imagem)
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
    Imagem VARBINARY(MAX) NULL
)
AS
BEGIN
    UPDATE Produtos SET
    Nome = @Nome,
    Preco = @Preco,
    Descricao = @Descricao,
    Imagem = @Imagem
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
