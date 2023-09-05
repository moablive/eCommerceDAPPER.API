-- Criação da tabela Usuários
CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(70) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Sexo CHAR(1),
    RG VARCHAR(15),
    CPF CHAR(14),
    NomeMae VARCHAR(70),
    SituacaoCadastro CHAR(1) NOT NULL,
    DataCadastro DATETIME NOT NULL
);

-- Criação da tabela Contatos (Um-para-Um)
CREATE TABLE Contatos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Telefone VARCHAR(15),
    Celular VARCHAR(15),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);

-- Criação da tabela EnderecosEntrega (Um-para-Muitos)
CREATE TABLE EnderecosEntrega (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT NOT NULL,
    NomeEndereco VARCHAR(100) NOT NULL,
    CEP CHAR(10) NOT NULL,
    Estado CHAR(2) NOT NULL,
    Cidade VARCHAR(120) NOT NULL,
    Bairro VARCHAR(200) NOT NULL,
    Endereco VARCHAR(200) NOT NULL,
    Numero VARCHAR(20),
    Complemento VARCHAR(30),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);

-- Criação da tabela Departamentos
CREATE TABLE Departamentos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL
);

-- Criação da tabela UsuariosDepartamentos (Muitos-para-Muitos)
CREATE TABLE UsuariosDepartamentos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT NOT NULL,
    DepartamentoId INT NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (DepartamentoId) REFERENCES Departamentos(Id) ON DELETE CASCADE
);
