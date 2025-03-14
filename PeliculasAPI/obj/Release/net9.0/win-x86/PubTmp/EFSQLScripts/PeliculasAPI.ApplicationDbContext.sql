﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250228145714_TablaGeneros'
)
BEGIN
    CREATE TABLE [Generos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Generos] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250228145714_TablaGeneros'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250228145714_TablaGeneros', N'9.0.2');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301200148_TablaActores'
)
BEGIN
    CREATE TABLE [Actores] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(150) NOT NULL,
        [FechaNacimiento] datetime2 NOT NULL,
        [Foto] varchar(max) NULL,
        CONSTRAINT [PK_Actores] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301200148_TablaActores'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250301200148_TablaActores', N'9.0.2');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302004828_TablaActoresFotoNull'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actores]') AND [c].[name] = N'Foto');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Actores] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Actores] ALTER COLUMN [Foto] varchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302004828_TablaActoresFotoNull'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250302004828_TablaActoresFotoNull', N'9.0.2');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304111256_TablaCines'
)
BEGIN
    CREATE TABLE [Cines] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(75) NOT NULL,
        [Ubicacion] geography NOT NULL,
        CONSTRAINT [PK_Cines] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304111256_TablaCines'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250304111256_TablaCines', N'9.0.2');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE TABLE [Peliculas] (
        [Id] int NOT NULL IDENTITY,
        [Titulo] nvarchar(300) NOT NULL,
        [Trailer] nvarchar(max) NULL,
        [FechaLanzamiento] datetime2 NOT NULL,
        [Poster] varchar(max) NULL,
        CONSTRAINT [PK_Peliculas] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE TABLE [PeliculasActores] (
        [ActorId] int NOT NULL,
        [PeliculaId] int NOT NULL,
        [Personaje] nvarchar(300) NOT NULL,
        [Orden] int NOT NULL,
        CONSTRAINT [PK_PeliculasActores] PRIMARY KEY ([ActorId], [PeliculaId]),
        CONSTRAINT [FK_PeliculasActores_Actores_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actores] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PeliculasActores_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE TABLE [PeliculasCines] (
        [CineId] int NOT NULL,
        [PeliculaId] int NOT NULL,
        CONSTRAINT [PK_PeliculasCines] PRIMARY KEY ([CineId], [PeliculaId]),
        CONSTRAINT [FK_PeliculasCines_Cines_CineId] FOREIGN KEY ([CineId]) REFERENCES [Cines] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PeliculasCines_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE TABLE [PeliculasGeneros] (
        [GeneroId] int NOT NULL,
        [PeliculaId] int NOT NULL,
        CONSTRAINT [PK_PeliculasGeneros] PRIMARY KEY ([GeneroId], [PeliculaId]),
        CONSTRAINT [FK_PeliculasGeneros_Generos_GeneroId] FOREIGN KEY ([GeneroId]) REFERENCES [Generos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PeliculasGeneros_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE INDEX [IX_PeliculasActores_PeliculaId] ON [PeliculasActores] ([PeliculaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE INDEX [IX_PeliculasCines_PeliculaId] ON [PeliculasCines] ([PeliculaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    CREATE INDEX [IX_PeliculasGeneros_PeliculaId] ON [PeliculasGeneros] ([PeliculaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250304194316_TablasPeliculas'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250304194316_TablasPeliculas', N'9.0.2');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250305185702_UsersSystem'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250305185702_UsersSystem', N'9.0.2');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306140130_TablaRatingsPelicula'
)
BEGIN
    CREATE TABLE [RatingsPeliculas] (
        [Id] int NOT NULL IDENTITY,
        [Puntuacion] int NOT NULL,
        [PeliculaId] int NOT NULL,
        [UsuarioId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_RatingsPeliculas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RatingsPeliculas_AspNetUsers_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_RatingsPeliculas_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306140130_TablaRatingsPelicula'
)
BEGIN
    CREATE INDEX [IX_RatingsPeliculas_PeliculaId] ON [RatingsPeliculas] ([PeliculaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306140130_TablaRatingsPelicula'
)
BEGIN
    CREATE INDEX [IX_RatingsPeliculas_UsuarioId] ON [RatingsPeliculas] ([UsuarioId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250306140130_TablaRatingsPelicula'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250306140130_TablaRatingsPelicula', N'9.0.2');
END;

COMMIT;
GO

