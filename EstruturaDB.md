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

```sql
CREATE TABLE Reviews (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProdutoId INT NOT NULL, 
    UsuarioId INT NOT NULL, 
    Pontuacao INT CHECK (Pontuacao BETWEEN 1 AND 5),
    Descricao NVARCHAR(MAX),   
    DataAvaliacao DATETIME NOT NULL DEFAULT GETDATE(), 
    FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) 
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

```sql

CREATE PROCEDURE [dbo].[spConsultaAvancadaUsuarios]
(
    @nome VARCHAR(MAX),
    @cpf VARCHAR(14)
)
AS
BEGIN
    IF @cpf = ''
    BEGIN
        SELECT *
        FROM Usuarios
        WHERE Nome LIKE '%' + @nome + '%';
    END
    ELSE
    BEGIN
        SELECT *
        FROM Usuarios
        WHERE Nome LIKE '%' + @nome + '%' AND CPF = @cpf;
    END
END


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
    @Imagem VARBINARY(MAX) NULL
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

```sql

CREATE PROCEDURE [dbo].[spConsultaAvancadaProdutos]
(
    @nome NVARCHAR(255),
    @preco DECIMAL(18, 2)
)
AS
BEGIN
    IF @preco IS NULL
    BEGIN
        SELECT *
        FROM Produtos
        WHERE Nome LIKE '%' + @nome + '%';
    END
    ELSE
    BEGIN
        SELECT *
        FROM Produtos
        WHERE Nome LIKE '%' + @nome + '%' AND Preco <= @preco;
    END
END


```

## Stored Procedures de Reviews

```sql
CREATE PROCEDURE spInsert_Review
(
    @ProdutoId INT,
    @UsuarioId INT,
    @Pontuacao INT,
    @Descricao NVARCHAR(MAX)
)
AS
BEGIN
    -- Insere a nova review na tabela Reviews
    INSERT INTO Reviews
    (ProdutoId, UsuarioId, Pontuacao, Descricao, DataAvaliacao)
    VALUES
    (@ProdutoId, @UsuarioId, @Pontuacao, @Descricao, GETDATE());
END
GO
```

```sql
CREATE PROCEDURE spUpdate_Review
(
    @Id INT,
    @ProdutoId INT,
    @UsuarioId INT,
    @Pontuacao INT,
    @Descricao NVARCHAR(MAX)
)
AS
BEGIN
    -- Atualiza os dados da review na tabela Reviews
    UPDATE Reviews
    SET
        ProdutoId = @ProdutoId,
        UsuarioId = @UsuarioId,
        Pontuacao = @Pontuacao,
        Descricao = @Descricao,
        DataAvaliacao = GETDATE()  -- Atualiza a data de avaliação para a data atual
    WHERE Id = @Id;
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
