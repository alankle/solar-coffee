(cd ./SolarCoffee.Data) -and (dotnet ef --startup-project ../SolarCoffee.web/ migrations add $Args[0])-and (cd ..)




