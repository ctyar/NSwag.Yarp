Get-ChildItem -Path '.\artifacts' | Remove-Item -Force -Recurse

dotnet pack src\NSwag.Yarp\NSwag.Yarp.csproj -o artifacts