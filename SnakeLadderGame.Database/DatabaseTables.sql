CREATE DATABASE SnakeLadderGame;

USE SnakeLadderGame;

CREATE TABLE Tbl_Board (
	BoardId INT IDENTITY(1,1) PRIMARY KEY,
	BoardName VARCHAR(50) NOT NULL,
	BoardDimension INT NOT NULL
);

CREATE TABLE Tbl_BoardRoute (
	RouteId INT IDENTITY(1,1) PRIMARY KEY,
	Place INT NOT NULL,
	ActionType VARCHAR(30) NOT NULL CHECK(ActionType IN ('Normal', 'Ladder', 'Snake')),
	Destination INT,
	BoardId INT NOT NULL,
	CONSTRAINT FK_Board_Route FOREIGN KEY (BoardId) REFERENCES Tbl_Board(BoardId)
);

CREATE TABLE Tbl_GameRoom (
	GameId INT IDENTITY(1, 1) PRIMARY KEY,
	RoomCode VARCHAR(20) NOT NULL UNIQUE,
	BoardId INT NOT NULL,
	LastTurn INT NOT NULL DEFAULT(1),
	Winner INT,
	PlayerNumbers INT NOT NULL,

	CONSTRAINT FK_GameRoom_Board FOREIGN KEY (BoardId) REFERENCES Tbl_Board(BoardId)
);

CREATE TABLE Tbl_Player (
	PlayerId INT IDENTITY(1, 1) PRIMARY KEY,
	Color VARCHAR(20) NOT NULL,
	Position INT DEFAULT(1),
	Turn INT NOT NULL,
	RoomCode VARCHAR(20) NOT NULL,

	CONSTRAINT FK_Player_GameRoom FOREIGN KEY (RoomCode) REFERENCES Tbl_GameRoom(RoomCode)
);