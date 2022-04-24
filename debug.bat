@echo off
dotnet build src\Unicorn.Umbraco.InvoiceManager\Unicorn.Umbraco.InvoiceManager.csproj --configuration Debug /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=c:/nuget