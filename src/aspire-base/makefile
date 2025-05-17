
.DEFAULT_GOAL := help

help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m   %s\n", $$1, $$2}'

nuget-outdated: ## Check for outdated NuGet packages
	@echo "Checking for outdated NuGet packages"
	cd SeatingAPI && dotnet outdated

nuget-update: ## Update NuGet packages
	@echo "Updating NuGet packages"
	cd SeatingAPI && dotnet outdated -u

deploy-local: ## Deploy the app locally
	@echo "Deploying the app locally"
	cd aspire-base.AppHost && dotnet run

provision-azure: ## Provision the app on Azure
	@echo "Provisioning the app on Azure"
	cd aspire-azure.AppHost && azd provision

deploy-azure: ## Deploy the app on Azure
	@echo "Deploying the app on Azure"
	cd aspire-azure.AppHost && azd deploy

down-azure: ## Tear down the app on Azure
	@echo "Tearing down the app on Azure"
	cd aspire-azure.AppHost && azd down

run-testsuite: ## Run the test suite
	@echo "Running the test suite in the SeatingAPI.Tests project"
	cd SeatingAPI.Tests && dotnet test --no-build --verbosity detailed

run-tests: ## Run the tests
	@echo "Running the tests"
	cd aspire-base.AppHost && dotnet test --no-build --verbosity normal
	@echo "Running the tests in the SeatingAPI project"
	cd SeatingAPI && dotnet test --no-build --verbosity normal
	@echo "Running the tests in the SeatingAPI.Tests project"
	cd SeatingAPI.Tests && dotnet test --no-build --verbosity normal