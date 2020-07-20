(cd ./SolarCoffee.Data) -and (dotnet ef --startup-project ../SolarCoffee.web/ migrations add $Args[0])  -and (dotnet ef --startup-project ../SolarCoffee.web/ database update) -and (cd ..)

