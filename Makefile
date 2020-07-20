# Project Variables
PROJECT_NAME ?= SolarCoffe
ORG_NAME ?= SolarCoffe
REPO_NAME ?= SolarCoffe

.PHONY: migrations db

migrations:
	cd SolarCoffee.Data && dotnet ef --startup-project ..\SolarCoffee.Web\ migrations add $(mname) & cd ..
db:
	cd SolarCoffee.Data && dotnet ef --startup-project ..\SolarCoffee.Web\ database update & cd ..
