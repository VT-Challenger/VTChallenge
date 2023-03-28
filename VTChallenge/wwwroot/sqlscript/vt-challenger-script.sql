CREATE TABLE PLATFORM(
	PID int NOT NULL,
	NAME nvarchar(50) NULL,
	IMAGE nvarchar(250) NULL,
 CONSTRAINT PK_PLATFORM PRIMARY KEY CLUSTERED 
(
	PID ASC
))

GO
/****** Object:  Table USERS    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE USERS(
	UID nvarchar(250) NOT NULL,
	NAME nvarchar(50) NOT NULL,
	TAG nvarchar(50) NOT NULL,
	EMAIL nvarchar(250) NOT NULL,
	PASSWORD nvarchar(20) NOT NULL,
	IMAGE_SMALL nvarchar(250) NULL,
	IMAGE_LARGE nvarchar(250) NULL,
	RANK nvarchar(25) NULL,
	ROL nvarchar(50) NULL,
	SALT nvarchar(50) NULL,
	PASSENCRIPT varbinary(max) NULL,
 CONSTRAINT PK_USER PRIMARY KEY CLUSTERED 
(
	UID ASC
))

GO
/****** Object:  Table TOURNAMENT    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE TOURNAMENT(
	TID int NOT NULL,
	NAME nvarchar(50) NULL,
	RANK nvarchar(25) NULL,
	DATEINIT datetime NULL,
	DESCRIPTION nvarchar(max) NULL,
	PID int NULL,
	PLAYERS int NULL,
	ORGANIZATOR nvarchar(250) NOT NULL,
	IMAGE nvarchar(250) NULL,
	ICON nvarchar(50) NULL,
 CONSTRAINT PK_TOURNAMENT PRIMARY KEY CLUSTERED 
(
	TID ASC
))

GO
/****** Object:  Table TOURNAMENT_PLAYERS    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE TOURNAMENT_PLAYERS(
	TID int NOT NULL,
	UID nvarchar(250) NOT NULL,
	TEAM int NULL,
 CONSTRAINT PK_TOURNAMENT_PLAYERS PRIMARY KEY CLUSTERED 
(
	TID ASC,
	UID ASC
))

GO
/****** Object:  View V_TOURNAMENT_COMPLETE    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW V_TOURNAMENT_COMPLETE
AS
	SELECT ISNULL(TOURNAMENT.TID,0) AS TID, TOURNAMENT.NAME,TOURNAMENT.RANK AS TRANK, TOURNAMENT.DATEINIT, TOURNAMENT.DESCRIPTION, 
	PLATFORM.NAME AS PLATFORM, PLATFORM.IMAGE AS PLATFORM_IMAGE, TOURNAMENT.PLAYERS AS LIMIT_PLAYERS, USERS.NAME AS ORGANIZATOR, USERS.IMAGE_LARGE AS IMAGE_ORGANIZATOR,
	TOURNAMENT.IMAGE,TOURNAMENT.ICON, ISNULL(COUNT(TOURNAMENT_PLAYERS.UID), 0) AS INSCRIPTIONS
	FROM TOURNAMENT
	INNER JOIN  PLATFORM on TOURNAMENT.PID = PLATFORM.PID
	INNER JOIN USERS on TOURNAMENT.ORGANIZATOR = USERS.UID
	LEFT JOIN TOURNAMENT_PLAYERS ON TOURNAMENT.TID = TOURNAMENT_PLAYERS.TID
	GROUP BY TOURNAMENT.TID, TOURNAMENT.NAME, TOURNAMENT.DATEINIT, TOURNAMENT.DESCRIPTION, 
	PLATFORM.NAME, PLATFORM.IMAGE, TOURNAMENT.PLAYERS, USERS.NAME, USERS.IMAGE_LARGE,
	TOURNAMENT.IMAGE, TOURNAMENT.RANK,TOURNAMENT.ICON
GO
/****** Object:  View V_PLAYERS_TOURNAMENT    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW V_PLAYERS_TOURNAMENT
AS
	SELECT USERS.UID, USERS.NAME, USERS.TAG, USERS.IMAGE_SMALL,
    TOURNAMENT_PLAYERS.TEAM,
    TOURNAMENT.TID
	FROM USERS
	INNER JOIN  TOURNAMENT_PLAYERS on USERS.UID = TOURNAMENT_PLAYERS.UID
	INNER JOIN  TOURNAMENT on TOURNAMENT_PLAYERS.TID = TOURNAMENT.TID
GO
/****** Object:  Table MATCH    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE MATCH(
	MID int NOT NULL,
	TBLUE int NULL,
	TRED int NULL,
	RBLUE int NULL,
	RRED int NULL,
	DATE datetime NULL,
	RID int NOT NULL,
 CONSTRAINT PK_MATCH PRIMARY KEY CLUSTERED 
(
	MID ASC
))

GO
/****** Object:  Table ROUND    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE ROUND(
	RID int NOT NULL,
	NAME nvarchar(150) NULL,
	DATE datetime NULL,
	TID int NOT NULL,
 CONSTRAINT PK_ROUND PRIMARY KEY CLUSTERED 
(
	RID ASC
))

GO
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (1, 1, 2, 1, 2, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (2, 3, 4, 3, 0, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (3, 5, 6, 3, 0, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (4, 7, 8, 1, 2, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (5, 9, 10, 2, 1, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (6, 11, 12, 3, 0, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (7, 13, 14, 2, 1, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (8, 15, 16, 3, 0, CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (9, 2, 4, 2, 1, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 2)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (10, 5, 7, 3, 0, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 2)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (11, 10, 12, 0, 3, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 2)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (12, 13, 16, 2, 1, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 2)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (13, 2, 7, 3, 0, CAST(N'2023-03-29T20:30:00.000' AS DateTime), 3)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (14, 12, 16, 3, 0, CAST(N'2023-03-29T20:30:00.000' AS DateTime), 3)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (15, 1, 16, 3, 0, CAST(N'2023-03-31T20:30:00.000' AS DateTime), 4)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (16, 1, 2, 3, 0, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 5)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (17, 3, 4, 0, 3, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 5)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (18, 5, 6, 2, 1, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 5)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (19, 7, 8, 0, 3, CAST(N'2023-03-28T20:30:00.000' AS DateTime), 5)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (20, 1, 4, 3, 0, CAST(N'2023-03-21T21:00:00.000' AS DateTime), 6)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (21, 5, 8, 3, 0, CAST(N'2023-03-21T21:00:00.000' AS DateTime), 6)
INSERT MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) VALUES (22, 1, 5, 0, 3, CAST(N'2023-03-22T21:00:00.000' AS DateTime), 7)
GO
INSERT PLATFORM (PID, NAME, IMAGE) VALUES (1, N'Discord', N'discord.png')
INSERT PLATFORM (PID, NAME, IMAGE) VALUES (2, N'TikTok', N'tiktok.png')
INSERT PLATFORM (PID, NAME, IMAGE) VALUES (3, N'Youtube', N'youtube.png')
INSERT PLATFORM (PID, NAME, IMAGE) VALUES (4, N'Twitch', N'twitch.png')
GO
INSERT ROUND (RID, NAME, DATE, TID) VALUES (1, N'Octavos de Final', CAST(N'2023-03-26T20:30:00.000' AS DateTime), 1)
INSERT ROUND (RID, NAME, DATE, TID) VALUES (2, N'Cuartos de Final', CAST(N'2023-03-28T20:30:00.000' AS DateTime), 1)
INSERT ROUND (RID, NAME, DATE, TID) VALUES (3, N'Semifinal', CAST(N'2023-03-29T20:30:00.000' AS DateTime), 1)
INSERT ROUND (RID, NAME, DATE, TID) VALUES (4, N'Final', CAST(N'2023-03-31T20:30:00.000' AS DateTime), 1)
INSERT ROUND (RID, NAME, DATE, TID) VALUES (5, N'Cuartos de Final', CAST(N'2023-03-18T21:00:00.000' AS DateTime), 2)
INSERT ROUND (RID, NAME, DATE, TID) VALUES (6, N'Semifinal', CAST(N'2023-03-21T21:00:00.000' AS DateTime), 2)
INSERT ROUND (RID, NAME, DATE, TID) VALUES (7, N'Final', CAST(N'2023-03-22T21:00:00.000' AS DateTime), 2)
GO
INSERT TOURNAMENT (TID, NAME, RANK, DATEINIT, DESCRIPTION, PID, PLAYERS, ORGANIZATOR, IMAGE, ICON) VALUES (1, N'Neon Cup', N'Diamond 2', CAST(N'2023-03-13T17:20:04.237' AS DateTime), N'La Neon Cup es un torneo de Valorant altamente
                            competitivo que re?ne a 16 equipos de jugadores de
                            alto nivel de todo el mundo. El torneo se lleva a
                            cabo en partidos ?nicos de 5vs5, lo que significa
                            que cada equipo debe trabajar en estrecha
                            colaboraci?n para superar a sus oponentes y avanzar
                            en el torneo. Para poder participar en la Neon Cup,
                            se requiere un rango m?nimo de Diamante 2, lo que
                            garantiza que todos los equipos est?n formados por
                            jugadores altamente habilidosos y bien coordinados.
                            La competencia es intensa y cada equipo debe mostrar
                            un alto nivel de estrategia y habilidad para tener
                            ?xito.', 1, 80, N'613f3233-5b4b-5f76-9544-df92d7bd284c', N'neon-cup.jpg', N't-cup-uefa.svg')
INSERT TOURNAMENT (TID, NAME, RANK, DATEINIT, DESCRIPTION, PID, PLAYERS, ORGANIZATOR, IMAGE, ICON) VALUES (2, N'Omen Cup', N'Platinum 3', CAST(N'2023-03-18T21:00:00.000' AS DateTime), N'La Omen Cup es........', 1, 40, N'613f3233-5b4b-5f76-9544-df92d7bd284c', N'omen-cup.jpg', N't-cup-uefa.svg')
GO
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (1, N'1e7b5b9d-7152-504d-9322-f3fbe245bcb5', 1)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (1, N'613f3233-5b4b-5f76-9544-df92d7bd284c', 3)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (1, N'881a69ef-2cf6-56eb-86df-d77222719b7b', 1)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (1, N'f1fa8069-2b5a-59c5-8a6e-05529be8c8c5', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (1, N'UID1', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (1, N'UID2', 1)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'613f3233-5b4b-5f76-9544-df92d7bd284c', 1)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'881a69ef-2cf6-56eb-86df-d77222719b7b', 4)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'f1fa8069-2b5a-59c5-8a6e-05529be8c8c5', 8)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID1', 8)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID10', 8)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID11', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID12', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID13', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID14', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID15', 2)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID17', 3)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID18', 3)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID20', 3)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID21', 4)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID23', 4)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID24', 4)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID25', 4)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID26', 5)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID27', 5)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID28', 5)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID29', 5)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID30', 5)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID31', 6)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID32', 6)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID33', 6)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID34', 6)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID35', 6)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID36', 7)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID37', 7)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID38', 7)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID4', 8)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID40', 7)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID5', 1)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID6', 1)
INSERT TOURNAMENT_PLAYERS (TID, UID, TEAM) VALUES (2, N'UID7', 1)
GO
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'1e7b5b9d-7152-504d-9322-f3fbe245bcb5', N'Sarto', N'999', N'sartoxr@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/fe84218f-4338-0f85-62cd-dfa5596576a0/smallart.png', N'https://media.valorant-api.com/playercards/fe84218f-4338-0f85-62cd-dfa5596576a0/largeart.png', N'Platinum 2', N'player', N'÷û?«Wï«#lL|{(AkE²,æò¾U+Gaão¸é:VjÔmnªÃ', 0x7B62426F3DA6C18948DCD0A43AEC4334B2FB572E2BDC694869351057100CD6941EB68E283DC83B7E56E0C1F3DB23DF49CFADBF3B39CBDA52500690DAA7152EA7)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'48cea567-c83d-546c-b803-4c5d052db60d', N'WerlasS', N'POWER', N'prueba@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/e70e685b-4e42-e14b-dbfc-68957b3b914a/smallart.png', N'https://media.valorant-api.com/playercards/e70e685b-4e42-e14b-dbfc-68957b3b914a/largeart.png', N'Radiant', N'player', N'ÏRà3äô~És®ØÚÝiÄ¡i~vÍ,
ný¡a"Ã9
æ!¥ÒÈÓÍõoï', 0x5BE4721A4C2DB5F3F83DDAE1A1DD08A67DA221288EBEAFEA9A9F50927BA44DD5E51145DD9145D2BEB90B9BA6936C384EA013A0940F7B0CA8EF3AD8297848E3F8)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'613f3233-5b4b-5f76-9544-df92d7bd284c', N'Popolas', N'9103', N'david.dominguez.blanco.2001@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/68b0c8c2-4158-7b21-658d-b4ae86f137ce/smallart.png', N'https://media.valorant-api.com/playercards/68b0c8c2-4158-7b21-658d-b4ae86f137ce/largeart.png', N'Platinum 3', N'player', N'
?bEÞº=°
7fmL©DFVñ¹è~eíª­¡NÜÊd<8IàAúÂæ', 0x661A5B55D0AC28171E7398D70DABE0B4A7119C5CDC781C3BB2BAE8F95B2FE74A3268818FE02353582AE2CDFBF4567B269644C93E13C0C091F4799627E2EC00D7)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'881a69ef-2cf6-56eb-86df-d77222719b7b', N'TheFackUs', N'5468', N'ddominguez@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/36377e6d-4be8-a585-f4a6-4fbdbb697324/smallart.png', N'https://media.valorant-api.com/playercards/36377e6d-4be8-a585-f4a6-4fbdbb697324/largeart.png', N'Gold 3', N'player', N' 8¢U/æ	<Ñ(~Ákuü{U#×³<
Ä°zÆ ½M ØEþöüÝ½×', 0x7F7723FBE96E649A5F6CFE74487424202B2644451321C8329C0351FC0F2D5963174AA859902D781A8B53B7AE5EE2A12F03D9D67DFD077F29995BC4CBECFF8738)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'94066d89-adc4-5af4-9eaf-e551b2b6c669', N'Chulo Sin H', N'2277', N'inakihd@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/5289acbf-461a-1d24-b198-60a5864d28bd/smallart.png', N'https://media.valorant-api.com/playercards/5289acbf-461a-1d24-b198-60a5864d28bd/largeart.png', N'Diamond 1', N'player', N'.ðêp\g­ã\Kö2 Quïä¡
-ø:7ÛpRß/ÃMÐúévå¦¾W9ÈïQé', 0x8310693A575D095E636BD44233FB94E42BEB620DA46A48E80497D44A3D8264D7F6339A92DC69EDC8BDEE9407AE7D5646B4F3956D3E76BD5AC5C359C028AD7F80)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'f1fa8069-2b5a-59c5-8a6e-05529be8c8c5', N'MisterJimmy', N'EUW', N'jaimedoguez@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/73569eb5-4953-2b61-1923-919874420845/smallart.png', N'https://media.valorant-api.com/playercards/73569eb5-4953-2b61-1923-919874420845/largeart.png', N'Bronze 2', N'player', N'wé%îh;NIüí»ûî{°@Ý	¸$ÑÂZ¦âÞUmGrw', 0x2BB83F3D1C306AB2006D2B33BF64ACF7B5FC4404425A6BEA3A417E74017EA58E7EE5695CF0306EC4E88D12128520870F42E9DE92E23E129F7D8BB2A400036826)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID1', N'User1', N'1', N'user1@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID10', N'HeyQUePAsaXabales', N'10', N'davidmejide@punto.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID11', N'User11', N'11', N'user11@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID12', N'User12', N'12', N'user12@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID13', N'User13', N'13', N'user13@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID14', N'User14', N'14', N'user14@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID15', N'User15', N'15', N'user15@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID16', N'User16', N'16', N'user16@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID17', N'User17', N'17', N'user17@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID18', N'User18', N'18', N'user18@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID19', N'User19', N'19', N'user19@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID2', N'User2', N'2', N'user2@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID20', N'User20', N'20', N'user20@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID21', N'User21', N'21', N'user21@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID22', N'User22', N'22', N'user22@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID23', N'User23', N'23', N'user23@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID24', N'User24', N'24', N'user24@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID25', N'User25', N'25', N'user25@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID26', N'User26', N'26', N'user26@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID27', N'User27', N'27', N'user27@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID28', N'User28', N'28', N'user28@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID29', N'User29', N'29', N'user29@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID3', N'User3', N'3', N'user3@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID30', N'User30', N'30', N'user30@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID31', N'User31', N'31', N'user31@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID32', N'User32', N'32', N'user32@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID33', N'User33', N'33', N'user33@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID34', N'User34', N'34', N'user34@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID35', N'User35', N'35', N'user35@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID36', N'User36', N'36', N'user36@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID37', N'User37', N'37', N'user37@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID38', N'User38', N'38', N'user38@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID39', N'User39', N'39', N'user39@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID4', N'User4', N'4', N'user4@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID40', N'User40', N'40', N'user40@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID41', N'User41', N'41', N'user41@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID42', N'User42', N'42', N'user42@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID43', N'User43', N'43', N'user43@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID44', N'User44', N'44', N'user44@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID45', N'User45', N'45', N'user45@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID46', N'User46', N'46', N'user46@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID47', N'User47', N'47', N'user47@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID48', N'User48', N'48', N'user48@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID49', N'User49', N'49', N'user49@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID5', N'User5', N'5', N'user5@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID50', N'User50', N'50', N'user50@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID51', N'User51', N'51', N'user51@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID52', N'User52', N'52', N'user52@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID53', N'User53', N'53', N'user53@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID54', N'User54', N'54', N'user54@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID55', N'User55', N'55', N'user55@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID56', N'User56', N'56', N'user56@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID57', N'User57', N'57', N'user57@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID58', N'User58', N'58', N'user58@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID59', N'User59', N'59', N'user59@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID6', N'User6', N'6', N'user6@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID60', N'User60', N'60', N'user60@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID61', N'User61', N'61', N'user61@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID62', N'User62', N'62', N'user62@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID63', N'User63', N'63', N'user63@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID64', N'User64', N'64', N'user64@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID65', N'User65', N'65', N'user65@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID66', N'User66', N'66', N'user66@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID67', N'User67', N'67', N'user67@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID68', N'User68', N'68', N'user68@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID69', N'User69', N'69', N'user69@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID7', N'User7', N'7', N'user7@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID70', N'User70', N'70', N'user70@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID71', N'User71', N'71', N'user71@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID72', N'User72', N'72', N'user72@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID73', N'User73', N'73', N'user73@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID74', N'User74', N'74', N'user74@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID75', N'User75', N'75', N'user75@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID76', N'User76', N'76', N'user76@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID77', N'User77', N'77', N'user77@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID78', N'User78', N'78', N'user78@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID79', N'User79', N'79', N'user79@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID8', N'User8', N'8', N'user8@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID80', N'User80', N'80', N'user80@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
INSERT USERS (UID, NAME, TAG, EMAIL, PASSWORD, IMAGE_SMALL, IMAGE_LARGE, RANK, ROL, SALT, PASSENCRIPT) VALUES (N'UID9', N'User9', N'9', N'user9@gmail.com', N'P@ssw0rd', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'https://media.valorant-api.com/playercards/f4361ec4-4e77-48c1-fa4f-b99b03134f96/largeart.png', N'Diamond 2', N'Player', NULL, NULL)
GO
ALTER TABLE MATCH  WITH CHECK ADD  CONSTRAINT FK_MATCH_ROUND FOREIGN KEY(RID)
REFERENCES ROUND (RID)
GO
ALTER TABLE MATCH CHECK CONSTRAINT FK_MATCH_ROUND
GO
ALTER TABLE ROUND  WITH CHECK ADD  CONSTRAINT FK_ROUND_TOURNAMENT FOREIGN KEY(TID)
REFERENCES TOURNAMENT (TID)
GO
ALTER TABLE ROUND CHECK CONSTRAINT FK_ROUND_TOURNAMENT
GO
ALTER TABLE TOURNAMENT  WITH CHECK ADD  CONSTRAINT FK_TOURNAMENT_PLATFORM FOREIGN KEY(PID)
REFERENCES PLATFORM (PID)
GO
ALTER TABLE TOURNAMENT CHECK CONSTRAINT FK_TOURNAMENT_PLATFORM
GO
ALTER TABLE TOURNAMENT  WITH CHECK ADD  CONSTRAINT FK_TOURNAMENT_USERS FOREIGN KEY(ORGANIZATOR)
REFERENCES USERS (UID)
GO
ALTER TABLE TOURNAMENT CHECK CONSTRAINT FK_TOURNAMENT_USERS
GO
ALTER TABLE TOURNAMENT_PLAYERS  WITH CHECK ADD  CONSTRAINT FK_TOURNAMENT_PLAYERS_TOURNAMENT FOREIGN KEY(TID)
REFERENCES TOURNAMENT (TID)
GO
ALTER TABLE TOURNAMENT_PLAYERS CHECK CONSTRAINT FK_TOURNAMENT_PLAYERS_TOURNAMENT
GO
/****** Object:  StoredProcedure SP_CHANGE_PASSWORD    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_CHANGE_PASSWORD
(@UID NVARCHAR(250), @PASSWORD NVARCHAR(20))
AS
	UPDATE USERS SET PASSWORD = @PASSWORD WHERE UID = @UID
GO
/****** Object:  StoredProcedure SP_DELETE_TOURNAMENT    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_DELETE_TOURNAMENT
(@TID INT)
AS
	DELETE m
	FROM MATCH m
	INNER JOIN ROUND on m.RID = ROUND.RID
	INNER JOIN TOURNAMENT on ROUND.TID = TOURNAMENT.TID
	WHERE TOURNAMENT.TID = @TID
	DELETE FROM ROUND WHERE TID = @TID
	DELETE FROM TOURNAMENT_PLAYERS WHERE TID = @TID
	DELETE FROM TOURNAMENT WHERE TID = @TID
GO
/****** Object:  StoredProcedure SP_GETGANADOR_TOURNAMENT    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_GETGANADOR_TOURNAMENT
(@TID INT)
AS
	DECLARE @GANADOR INT;
	SET @GANADOR = (SELECT 
						CASE 
							WHEN RBLUE > RRED THEN TBLUE
							ELSE TRED
						END AS Ganador
					FROM MATCH
					WHERE RID IN (
						SELECT RID
						FROM ROUND
						WHERE TID = @TID AND NAME = 'Final'
					))
	SELECT * FROM V_PLAYERS_TOURNAMENT WHERE TEAM = @GANADOR AND TID = @TID
GO
/****** Object:  StoredProcedure SP_GETTOTAL_TOURNAMENTS_WON    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_GETTOTAL_TOURNAMENTS_WON
(@UID NVARCHAR(250))
AS
	SELECT COUNT(*) AS TotalWins
	FROM MATCH M
	JOIN ROUND R ON M.RID = R.RID AND R.NAME = 'Final'
	JOIN TOURNAMENT_PLAYERS TP ON M.TRED = TP.TID OR M.TBLUE = TP.TID
	WHERE TP.UID = @UID
	AND (
		(M.TRED = TP.TID AND M.RRED > M.RBLUE)
		OR (M.TBLUE = TP.TID AND M.RBLUE > M.RRED)
	)
GO
/****** Object:  StoredProcedure SP_INSCRIPTION_PLAYER_TEAMALE    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_INSCRIPTION_PLAYER_TEAMALE
(@TID INT, @UID NVARCHAR(250))
AS
 DECLARE @TEAM INT;
 DECLARE @TEAMALE INT;

 -- Obtener la cantidad de equipos disponibles
  DECLARE @num_equipos INT = (SELECT PLAYERS/5 FROM TOURNAMENT WHERE TID = @TID);
  
  -- Obtener lista de equipos que no estén llenos
  WITH TEAMS_DISPONIBLES AS (
    SELECT TEAM, COUNT(UID) AS NUM_JUGADORES
    FROM TOURNAMENT_PLAYERS
	WHERE TID = @TID
    GROUP BY TEAM
    HAVING COUNT(UID) < 5
  )
  
  -- Seleccionar un equipo aleatorio de la lista de equipos disponibles
  SELECT TOP 1 @TEAM = TEAM
  FROM TEAMS_DISPONIBLES
  ORDER BY NEWID();

  IF @TEAM IS NULL
  BEGIN
    IF (SELECT COUNT(*) FROM TOURNAMENT_PLAYERS WHERE TID=@TID) < @num_equipos
    BEGIN
	  SET @TEAMALE = (SELECT ABS(ROUND(RAND() * (@num_equipos - 1) + 1, 0)))
	  INSERT INTO TOURNAMENT_PLAYERS VALUES(@TID, @UID,@TEAMALE)		
    END
    ELSE
    BEGIN
      RAISERROR('No hay equipos disponibles', 16, 1);
      RETURN;
    END
  END;

  -- Insertar el jugador en el equipo seleccionado
  INSERT INTO TOURNAMENT_PLAYERS VALUES(@TID, @UID,@TEAM)
GO
/****** Object:  StoredProcedure SP_UPDATE_USER    Script Date: 22/03/2023 22:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_UPDATE_USER
(@UID NVARCHAR(250), @NAME NVARCHAR(50), @TAG NVARCHAR(50), @IMAGESMALL NVARCHAR(250), @IMAGELARGE NVARCHAR(250), @RANK NVARCHAR(25))
AS
	UPDATE USERS SET NAME = @NAME, TAG = @TAG, IMAGE_SMALL = @IMAGESMALL, IMAGE_LARGE = @IMAGELARGE, RANK = @RANK
	WHERE UID = @UID;
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_PLAYER_TOURNAMENT]     ******/

  CREATE PROCEDURE SP_DELETE_PLAYER_TOURNAMENT
  (@TID int, @UID NVARCHAR(250))
  AS
	  delete from TOURNAMENT_PLAYERS where tid = @TID and uid = @UID

	  /****** Object:  StoredProcedure [dbo].[SP_INSERT_NEXT_MATCHES]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_INSERT_NEXT_MATCHES
(@RID INT)
AS
	DECLARE @num_matches_no_winner int;
	DECLARE @next_round int;
	DECLARE @i INT = 1
	DECLARE @next_mid int;
	DECLARE @date DateTime;


	SET @next_round = @RID + 1
	SET @date = (SELECT DATE FROM ROUND WHERE RID = @next_round)

	SELECT TBLUE AS winner
	INTO #winners
	FROM MATCH
	WHERE RID = @RID AND RBLUE > RRED
	UNION ALL
	SELECT TRED AS winner
	FROM MATCH
	WHERE RID = @RID AND RBLUE < RRED;

	WHILE @i <= (SELECT COUNT(*) FROM #winners)/2
	BEGIN
		SET @next_mid = (SELECT MAX(MID)+1 FROM MATCH)

		
		DECLARE @W1 INT = (SELECT winner FROM (SELECT winner, ROW_NUMBER() OVER (ORDER BY winner) AS RowNum FROM #winners) AS WinnersWithRowNum WHERE RowNum = (2 * @i - 1))
		DECLARE @W2 INT  = (SELECT winner FROM (SELECT winner, ROW_NUMBER() OVER (ORDER BY winner) AS RowNum FROM #winners) AS WinnersWithRowNum WHERE RowNum = (2 * @i))
		
		INSERT INTO MATCH (MID, TBLUE, TRED, RBLUE, RRED, DATE, RID) 
		VALUES (@next_mid,@W1, @W2, 0, 0, @date,@next_round);

		SET @i = @i + 1
	END
	DROP TABLE #winners;
