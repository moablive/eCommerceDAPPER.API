-- Criação de procedimentos armazenados na tabela Usuarios
DELIMITER //
CREATE PROCEDURE SelecionarUsuarios()
BEGIN
    SELECT * FROM Usuarios;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE SelecionarUsuario(IN id INT)
BEGIN
    SELECT * FROM Usuarios WHERE Id = id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE CadastrarUsuario(
    IN nome VARCHAR(70),
    IN email VARCHAR(100),
    IN sexo CHAR(1),
    IN rg VARCHAR(15),
    IN cpf CHAR(14),
    IN nomeMae VARCHAR(70),
    IN situacaoCadastro CHAR(1),
    IN dataCadastro DATETIME
)
BEGIN
    INSERT INTO Usuarios (Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro)
    VALUES (nome, email, sexo, rg, cpf, nomeMae, situacaoCadastro, dataCadastro);
    SELECT LAST_INSERT_ID() AS Id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE AtualizarUsuario(
    IN id INT,
    IN nome VARCHAR(70),
    IN email VARCHAR(100),
    IN sexo CHAR(1),
    IN rg VARCHAR(15),
    IN cpf CHAR(14),
    IN nomeMae VARCHAR(70),
    IN situacaoCadastro CHAR(1),
    IN dataCadastro DATETIME
)
BEGIN
    UPDATE Usuarios SET
        Nome = nome,
        Email = email,
        Sexo = sexo,
        RG = rg,
        CPF = cpf,
        NomeMae = nomeMae,
        SituacaoCadastro = situacaoCadastro,
        DataCadastro = dataCadastro
    WHERE Id = id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeletarUsuario(IN id INT)
BEGIN
    DELETE FROM Usuarios WHERE Id = id;
END //
DELIMITER ;
