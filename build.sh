#!/bin/bash

set -e

dotnet restore --no-cache
dotnet build --configuration Debug
dotnet test ./test/OrderServiceDemo.Unit.Tests/OrderServiceDemo.Unit.Tests.csproj
dotnet publish ./src/OrderServiceDemo/OrderServiceDemo.csproj --output ../../publish/output

