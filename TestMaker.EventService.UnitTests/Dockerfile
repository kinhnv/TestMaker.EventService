#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TestMaker.EventService.UnitTests/TestMaker.EventService.UnitTests.csproj"
WORKDIR "/src/TestMaker.EventService.UnitTests"