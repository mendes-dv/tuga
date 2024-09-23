USE master;
GO

-- Create the GestMiniMercados database
CREATE DATABASE GestMiniMercadosDB;
GO

-- Use the new database
USE GestMiniMercadosDB;
GO

-- Create the Categoria table
CREATE TABLE Categoria (
                           Id_Categoria INT PRIMARY KEY IDENTITY(1,1),
                           Nome_Categoria NVARCHAR(255) NOT NULL,
                           Iva FLOAT NOT NULL
);
GO

-- Create the Produto table
CREATE TABLE Produto (
                         Id_Produto INT PRIMARY KEY IDENTITY(1,1),
                         Nome_Produto NVARCHAR(255) NOT NULL,
                         Id_Categoria INT NOT NULL,
                         Qtd_Produto INT NOT NULL,
                         Preco_Unitario_Compra FLOAT NOT NULL,
                         Preco_Unitario_Venda FLOAT NOT NULL,
                         Margem_Unitaria FLOAT NOT NULL,
                         Preco_Campanha FLOAT NOT NULL,
                         Qtd_Total INT NOT NULL,
                         FOREIGN KEY (Id_Categoria) REFERENCES Categoria(Id_Categoria)
);
GO

-- Create the User table
CREATE TABLE Users (
                      N_Contribuinte BIGINT PRIMARY KEY,
                      Nome_Funcionario NVARCHAR(255) NOT NULL,
                      Tp_Utilizador NVARCHAR(50) NOT NULL,
                      Data_Contrato DATETIME NOT NULL,
                      Telemovel BIGINT NOT NULL,
                      Morada NVARCHAR(255) NOT NULL,
                      Estado_Civil NVARCHAR(50) NOT NULL,
                      Data_Nascimento DATETIME NOT NULL,
                      Password NVARCHAR(255) NOT NULL,
                      Foto VARBINARY(MAX)
);
GO

-- Create the UserLogin table
CREATE TABLE UserLogin (
                           N_Contribuinte BIGINT PRIMARY KEY,
                           Password NVARCHAR(255) NOT NULL
);
GO

-- Create the Fornecedor table
CREATE TABLE Fornecedor (
                            Id_Fornecedor INT PRIMARY KEY IDENTITY(1,1),
                            Nome_Fornecedor NVARCHAR(255) NOT NULL,
                            Id_Categoria INT NOT NULL,
                            Morada NVARCHAR(255) NOT NULL,
                            N_Contribuinte BIGINT NOT NULL,
                            FOREIGN KEY (Id_Categoria) REFERENCES Categoria(Id_Categoria)
);
GO

-- Create the Entrega table
CREATE TABLE Entrega (
                         Id_Fornecedor INT NOT NULL,
                         Id_Produto INT NOT NULL,
                         Data_Entrega DATETIME NOT NULL,
                         PRIMARY KEY (Id_Fornecedor, Id_Produto)
);
GO

-- Create the Ferias table
CREATE TABLE Ferias (
                        N_Contribuinte BIGINT NOT NULL,
                        Data_Inicio_Ferias DATETIME NOT NULL,
                        Data_Fim_Ferias DATETIME NOT NULL,
                        PRIMARY KEY (N_Contribuinte, Data_Inicio_Ferias)
);
GO

-- Create the HorarioDeTrabalho table
CREATE TABLE HorarioDeTrabalho (
                                   N_Contribuinte BIGINT NOT NULL,
                                   Data_Inicio_Trabalho DATETIME NOT NULL,
                                   Data_Fim_Trabalho DATETIME NOT NULL,
                                   PRIMARY KEY (N_Contribuinte, Data_Inicio_Trabalho)
);
GO

-- Create the InfoFornecedores table
CREATE TABLE InfoFornecedores (
                                  Nome_Fornecedor NVARCHAR(255) NOT NULL,
                                  Nome_CategoriaFornecida NVARCHAR(255) NOT NULL,
                                  Morada NVARCHAR(255) NOT NULL,
                                  N_Contribuinte BIGINT NOT NULL
);
GO

-- Create the InfoProds table
CREATE TABLE InfoProds (
                           Nome_Produto NVARCHAR(255) NOT NULL,
                           Qtd_Atual_Produto INT NOT NULL,
                           Qtd_Maxima_Produto INT NOT NULL,
                           Preço_Venda FLOAT NOT NULL,
                           Preço_Compra FLOAT NOT NULL,
                           Margem_Lucro FLOAT NOT NULL,
                           Preço_Campanha FLOAT NOT NULL
);
GO

-- Create the IvaCategoria table
CREATE TABLE IvaCategoria (
                              Nome_Categoria NVARCHAR(255) NOT NULL,
                              Iva_Categoria FLOAT NOT NULL
);
GO

-- Create the ProdsPerCat table
CREATE TABLE ProdsPerCat (
                             Nome_Categoria NVARCHAR(255) NOT NULL,
                             Qtd_Prods INT NOT NULL
);
GO

-- Sample Data Insertion

-- Insert sample data into Categoria
INSERT INTO Categoria (Nome_Categoria, Iva) VALUES ('Beverages', 0.23);
INSERT INTO Categoria (Nome_Categoria, Iva) VALUES ('Snacks', 0.18);
INSERT INTO Categoria (Nome_Categoria, Iva) VALUES ('Dairy', 0.12);
GO

-- Insert sample data into User
INSERT INTO Users (N_Contribuinte, Nome_Funcionario, Tp_Utilizador, Data_Contrato, Telemovel, Morada, Estado_Civil, Data_Nascimento, Password, Foto)
VALUES 
(123456789, 'João Silva', 'Administrador', '2022-01-15', 912345678, 'Rua A, Nº10, Cidade A', 'Casado', '1985-05-20', 'password123', NULL),
(987654321, 'Maria Pereira', 'Funcionário', '2023-03-10', 915678912, 'Rua B, Nº20, Cidade B', 'Solteiro', '1990-10-05', 'mypassword', NULL);
GO

-- Insert sample data into Fornecedor
INSERT INTO Fornecedor (Nome_Fornecedor, Id_Categoria, Morada, N_Contribuinte)
VALUES 
('Fornecedor 1', 1, 'Rua X, Nº1, Cidade Y', 234567890),
('Fornecedor 2', 2, 'Rua Y, Nº2, Cidade Z', 345678901);
GO

-- Insert sample data into Produto
INSERT INTO Produto (Nome_Produto, Id_Categoria, Qtd_Produto, Preco_Unitario_Compra, Preco_Unitario_Venda, Margem_Unitaria, Preco_Campanha, Qtd_Total)
VALUES 
('Cerveja', 1, 100, 1.00, 1.50, 0.50, 1.20, 1000),
('Chips', 2, 200, 0.50, 1.00, 0.30, 0.80, 500);
GO

-- Insert sample data into Entrega
INSERT INTO Entrega (Id_Fornecedor, Id_Produto, Data_Entrega) 
VALUES 
(1, 1, '2024-01-10'),
(2, 2, '2024-01-12');
GO

-- Insert sample data into Ferias
INSERT INTO Ferias (N_Contribuinte, Data_Inicio_Ferias, Data_Fim_Ferias) 
VALUES 
(123456789, '2024-06-01', '2024-06-15');
GO

-- Insert sample data into HorarioDeTrabalho
INSERT INTO HorarioDeTrabalho (N_Contribuinte, Data_Inicio_Trabalho, Data_Fim_Trabalho) 
VALUES 
(123456789, '2024-01-01', '2024-12-31');
GO

-- Insert sample data into InfoFornecedores
INSERT INTO InfoFornecedores (Nome_Fornecedor, Nome_CategoriaFornecida, Morada, N_Contribuinte) 
VALUES 
('Fornecedor 1', 'Beverages', 'Rua X, Nº1, Cidade Y', 234567890),
('Fornecedor 2', 'Snacks', 'Rua Y, Nº2, Cidade Z', 345678901);
GO

-- Insert sample data into InfoProds
INSERT INTO InfoProds (Nome_Produto, Qtd_Atual_Produto, Qtd_Maxima_Produto, Preço_Venda, Preço_Compra, Margem_Lucro, Preço_Campanha) 
VALUES 
('Cerveja', 150, 200, 1.50, 1.00, 0.50, 1.20),
('Chips', 180, 250, 1.00, 0.50, 0.30, 0.80);
GO

-- Insert sample data into IvaCategoria
INSERT INTO IvaCategoria (Nome_Categoria, Iva_Categoria) 
VALUES 
('Beverages', 0.23),
('Snacks', 0.18);
GO

-- Insert sample data into ProdsPerCat
INSERT INTO ProdsPerCat (Nome_Categoria, Qtd_Prods) 
VALUES 
('Beverages', 5),
('Snacks', 3);
GO
