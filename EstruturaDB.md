# Estrutura do Banco de Dados


```sql
CREATE TABLE Clientes (
    Id INT PRIMARY KEY,
    Nome VARCHAR(MAX),
    CPF VARCHAR(14),
    Telefone VARCHAR(20),
    Email VARCHAR(100),
    Endereco VARCHAR(MAX),
    Cidade VARCHAR(MAX),
    Estado VARCHAR(2),
    CEP VARCHAR(10)
);

```

### Stored Procedures de Clientes

```sql
create procedure spInsert_Clientes
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
    insert into Clientes
    (id, nome, cpf, telefone, email, endereco, cidade, estado, cep)
    values
    (@id, @nome, @cpf, @telefone, @email, @endereco, @cidade, @estado, @cep)
end
GO

create procedure spUpdate_Clientes
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
    update Clientes set
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

## Stored Procedures Genéricas

### spDelete

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
