function ThrowOnError($message) {
    $exitCode = $LASTEXITCODE
    if ($exitCode -ne 0) {
        throw $message
    }
}

New-Item -ItemType Directory -Force -Path ./test-results

dotnet add ./src/CruxEngine/Tests/CruxEngine.Core.Unit.Tests package JetBrains.dotCover.CommandLineTools.linux-x64 --version 2023.3.3 --package-directory ./nuget

dotnet sonarscanner begin -o:"ensyinc" -k:"EnsyInc_CruxEngine" -d:sonar.host.url="https://sonarcloud.io" -d:sonar.token="$env:SONAR_TOKEN" -d:sonar.cs.dotcover.reportsPaths="./code-coverage/unit-tests/coverage.html" -d:sonar.coverage.exclusions="**/Tests/**,**/code-coverage/**" -d:sonar.exclusions="**/.vs/**,**/*.sln,**/code-coverage/**"
ThrowOnError "Failed to start Sonar Scanner session"

dotnet build ./src/CruxEngine/Tests/CruxEngine.Core.Unit.Tests/CruxEngine.Core.Unit.Tests.csproj
ThrowOnError "Failed to build"

./nuget/jetbrains.dotcover.commandlinetools.linux-x64/2023.3.3/tools/dotCover.sh dotnet --Filters="-:module=Humanizer;-:module=vstest.console" --Output="./test-results/coverage.dcvr" -- test ./src/CruxEngine/CruxEngine.sln --no-restore --no-build
ThrowOnError "Failed to run tests"

./nuget/jetbrains.dotcover.commandlinetools.linux-x64/2023.3.3/tools/dotCover.sh report --Source="./test-results/coverage.dcvr" --Output="./code-coverage/unit-tests/coverage.html" --ReportType="HTML" --SourcesSearchPaths="./src/CruxEngine/**" --ExcludeFileMasks="./src/CruxEngine/Tests/**"
ThrowOnError "Failed to generate code coverage report"

dotnet sonarscanner end -d:sonar.token="$env:SONAR_TOKEN"
ThrowOnError "Failed to end Sonar Scanner session"
