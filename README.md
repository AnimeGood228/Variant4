Scaffold-DbContext "Server=1misa;Database=Variant4;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
-- Таблица пользователей
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Login NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    RegisteredDate DATE NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL
);

-- Таблица заявок пациентов
CREATE TABLE PatientRequests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Article NVARCHAR(100) NOT NULL UNIQUE,        
    Title NVARCHAR(200) NOT NULL,                
    Type NVARCHAR(100) NOT NULL,                  
    Description NVARCHAR(MAX),                    
    CreatedDate DATE NOT NULL,                    
    Status INT NOT NULL,                          
    UserId INT NULL,                              
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
