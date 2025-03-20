=====================================================================================================================================
=====================================================================================================================================
=====================================================================================================================================

1. install nugget packages: "squirrel.windows" и "NuGet.CommandLine"

=====================================================================================================================================
=====================================================================================================================================
=====================================================================================================================================

2. Добавлен файл ReleaseSpec.nuspec
Он нужен для формирования .nupkg
Местоположение: ...\SquirrelUpdateExample\SquirrelUpdateExample\ReleaseSpec.nuspec

=====================================================================================================================================
=====================================================================================================================================
=====================================================================================================================================

2. В ...\SquirrelUpdateExample\SquirrelUpdateExample\SquirrelUpdateExample.csproj
добавлен уникальнй Target


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

=====================================================================================================================================

Фактически это 2 скрипта, которые выполняются после успешной сборки релиза (выбрать тип сборки - Release)

1) C:\Users\aderyagin\source\repos\SquirrelUpdateExample\packages\NuGet.CommandLine.6.13.2\tools\NuGet.exe pack C:\Users\aderyagin\source\repos\SquirrelUpdateExample\SquirrelUpdateExample\ReleaseSpec.nuspec -Version 1.2.0.0 -Properties Configuration=Release -OutputDirectory C:\Users\aderyagin\source\repos\SquirrelUpdateExample\Deployment\GeneratedNugets
Этот скрипт создаёт папку ...\SquirrelUpdateExample\Deployment\GeneratedNugets\
и помещает в неё файл SquirrelUpdateExample.{version}.nupkg

2) C:\Users\aderyagin\source\repos\SquirrelUpdateExample\packages\squirrel.windows.2.0.1\tools\Squirrel.exe --releasify C:\Users\aderyagin\source\repos\SquirrelUpdateExample\Deployment\GeneratedNugets\SquirrelUpdateExample.1.2.0.nupkg --releaseDir=C:\Users\aderyagin\source\repos\SquirrelUpdateExample\Deployment\Releases

Этот скрипт создаёт папку ...\SquirrelUpdateExample\Deployment\Releases\{version}\
И на основе файла SquirrelUpdateExample.{version}.nupkg
Созлает файлы для обновления

=====================================================================================================================================

Текущая версия вериза берётся из AssemblyInfo.cs

=====================================================================================================================================

Что бы выгрузить релиз нужно зайти на 
https://github.com/DeryaginAlex/SquirrelUpdateExample/releases/
нажать "Draft a new release"
Заполнить теги, описание и добавить сформированные файлы

Полученные файлы нужно 