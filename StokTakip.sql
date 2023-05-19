-- CREATE DATABASE StockTracking;

USE StockTracking;

CREATE TABLE
    Users (
        ID INT PRIMARY KEY AUTO_INCREMENT,
        UserName VARCHAR(250) NOT NULL,
        Password VARCHAR(250) NOT NULL,
        FullName VARCHAR(200) NOT NULL,
        PhotoPath VARCHAR(500),
        Email VARCHAR(250) NOT NULL,
        EmailValidated BOOLEAN DEFAULT 0,
        CreateDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
    );

-- CREATE DATABASE StockTracking;

USE StockTracking;

CREATE TABLE
    Categories (
        ID INT PRIMARY KEY AUTO_INCREMENT,
        UserID INT NOT NULL,
        CategoryName VARCHAR(300) NOT NULL,
        ImagePath VARCHAR(500),
        Color VARCHAR(50),
        Description VARCHAR(500),
        RowIndex INT NOT NULL DEFAULT 0,
        CrateDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT FK_Users_Categories_UserID FOREIGN KEY (UserID) REFERENCES Users(ID)
    );

CREATE TABLE
    Products (
        ID INT PRIMARY KEY AUTO_INCREMENT,
        CategoryID INT NOT NULL,
        ProductName VARCHAR(200) NOT NULL,
        ImagePath VARCHAR(500),
        Description VARCHAR(500),
        ProductUrl VARCHAR(350),
        Quantity INT NOT NULL DEFAULT 0,
        CreateDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT FK_Categories_Products_CategoryID FOREIGN KEY (CategoryID) REFERENCES Categories(ID) ON DELETe CASCADE
    );