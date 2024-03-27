#!/bin/bash

# /filelogger /fileLoggerParameters:Verbosity=diag

rm -rf ~/.nuget/packages/blazor.avatar/2.0.0
# git clean -xfd src tests
# sleep 1
# dotnet build -c $1 src/Blazor.Avatar.csproj
sleep 2

# dotnet pack --no-build -c $1 -o . /p:Version=2.0.0 src/Blazor.Avatar.csproj /fileLoggerParameters:Verbosity=diag
dotnet pack -c $1 -o . /p:Version=2.0.0 src/Blazor.Avatar.csproj /fileLoggerParameters:Verbosity=diag
dotnet add ./.tmp/WebApp/WebApp.csproj package Blazor.Avatar -s ./ --version 2.0.0
