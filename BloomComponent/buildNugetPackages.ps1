rm *.nupkg
nuget pack .\BloomComponent.nuspec -IncludeReferencedProjects -Prop Configuration=Release 
nuget push *.nupkg -Source https://www.nuget.org/api/v2/package