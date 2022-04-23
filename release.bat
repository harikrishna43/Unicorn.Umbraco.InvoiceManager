@echo off

dotnet build src\InvoiceManager\InvoiceManager.csproj --configuration Release /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=../../releases/nuget