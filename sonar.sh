#!/bin/bash

set -euo pipefail

if [[ $TRAVIS_PULL_REQUEST == "false" ]] && [[ $TRAVIS_BRANCH == "master" ]];
then
  dotnet-sonarscanner begin /key:OrderServiceDemo \
    /name:OrderServiceDemo \
    /d:sonar.organization="rightnow-ministries" \
    /d:sonar.host.url="https://sonarcloud.io" \
    /d:sonar.login=$SONAR_LOGIN \
    /d:sonar.cs.opencover.reportsPaths="./tests/OrderServiceDemo.Unit.Tests/coverage.opencover.xml" \
    /d:sonar.coverage.exclusions="./tests/**"
	
	dotnet restore --no-cache
	dotnet build --configuration Debug
	dotnet test ./tests/OrderServiceDemo.Unit.Tests/OrderServiceDemo.Unit.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
  
	dotnet-sonarscanner end /d:sonar.login=$SONAR_LOGIN
else
  dotnet-sonarscanner begin /key:OrderServiceDemo \
    /name:OrderServiceDemo \
    /d:sonar.organization="rightnow-ministries" \
    /d:sonar.host.url="https://sonarcloud.io" \
    /d:sonar.login="cacbf3bb82d9f5f2be12f6e0fc19bcf6c2662f44" \
    /d:sonar.cs.opencover.reportsPaths="./tests/OrderServiceDemo.Unit.Tests/coverage.opencover.xml" \
    /d:sonar.coverage.exclusions="./tests/**" \
    /d:sonar.pullrequest.github.repository=$TRAVIS_REPO_SLUG \
    /d:sonar.pullrequest.base="master" \
    /d:sonar.pullrequest.provider="GitHub" \
    /d:sonar.pullrequest.branch=$TRAVIS_PULL_REQUEST_BRANCH \
    /d:sonar.pullrequest.key=$TRAVIS_PULL_REQUEST

	dotnet restore --no-cache
	dotnet build --configuration Debug
	dotnet test ./tests/OrderServiceDemo.Unit.Tests/OrderServiceDemo.Unit.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
  
	dotnet-sonarscanner end /d:sonar.login="cacbf3bb82d9f5f2be12f6e0fc19bcf6c2662f44"
fi;

