@echo off
dotnet build src\InvoiceManager\InvoiceManager.csproj --configuration Debug /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=c:/nuget