
.DEFAULT_GOAL := help

help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m   %s\n", $$1, $$2}'

nuget-updates: ## Check for NuGet updates
	@echo "Checking for NuGet updates"

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