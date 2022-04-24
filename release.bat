@echo off

dotnet build src\Unicorn.Umbraco.InvoiceManager\Unicorn.Umbraco.InvoiceManager.csproj --configuration Release /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=../../releases/nuget