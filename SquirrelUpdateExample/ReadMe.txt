1. install nugget packages:
squirrel.windows
NuGet.CommandLine

Объязательно установить через Package Manager Console

2. Новый файл ReleaseSpec.nuspec
...\SquirrelUpdateExample\SquirrelUpdateExample
он нужен что бы создать .nupkg

3. В ...\SquirrelUpdateExample\SquirrelUpdateExample\SquirrelUpdateExample.csproj
добавлен уникальнй Target
благодаря нему будет создан .nupkg (сбилдить проект в режими Release)

Тут просто 2 скрипта, которые выполняются после успешной сборки релиза.
Что бы они работали нужно скачать nuget.exe и прописать его в $PATH


<Target Name="AfterBuild">
  <ItemGroup>
    <NugetTools Include="$(SolutionDir)packages\NuGet.CommandLine.*\tools\NuGet.exe" />
    <SquirrelTools Include="$(SolutionDir)packages\squirrel.windows.*\tools\Squirrel.exe" />
  </ItemGroup>
  <Copy SourceFiles="@(SquirrelTools->'%(FullPath)')" DestinationFiles="$(OutDir)..\Update.exe" Condition="!Exists('$(OutDir)..\Update.exe')" />
  <GetAssemblyIdentity AssemblyFiles="$(TargetPath)">
    <Output TaskParameter="Assemblies" ItemName="assemblyInfo" />
  </GetAssemblyIdentity>
  <PropertyGroup>
    <Version>$([System.Version]::Parse(%(assemblyInfo.Version)).ToString(4))</Version>
    <NuspecFile>$(SolutionDir)SquirrelUpdateExample\ReleaseSpec.nuspec</NuspecFile>
  </PropertyGroup>
  <XmlPeek XmlInputPath="$(NuspecFile)" Query="/package/metadata/id/text()">
    <Output TaskParameter="Result" ItemName="ID" />
  </XmlPeek>
  <Exec Condition=" '$(Configuration)' == 'Release'" Command="@(NugetTools->'%(FullPath)') pack $(NuspecFile) -Version $(Version) -Properties Configuration=Release -OutputDirectory $(SolutionDir)Deployment\GeneratedNugets" />
  <Exec Condition=" '$(Configuration)' == 'Release'" Command="@(SquirrelTools->'%(FullPath)') --releasify $(SolutionDir)Deployment\GeneratedNugets\@(ID).$(Version).nupkg --releaseDir=$(SolutionDir)Deployment\Releases" />
</Target>

4. После этого открываем Package Manager Console
Squirrel --releasify C:\Users\aderyagin\source\repos\SquirrelUpdateExample\SquirrelUpdateExample\SquirrelUpdateExample.0.0.0.nupkg

