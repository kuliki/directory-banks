@echo off


rem Путь к nuget (nuget.exe)
set NUGET_EXE=..\..\NuGet\NuGet.exe

rem Относительный путь к каталогу Проекта
set PROJECT_DIR=..\MMS.Directory.Banks.Client

rem Название файла Проекта без расширения
set PROJECT_FILE_WITHOUT_EXT=MMS.Directory.Banks.Client

rem Файл Проекта
set PROJECT_FILE=%PROJECT_FILE_WITHOUT_EXT%.csproj

rem Правильное название файла манифеста для Проекта (соответствующее файлу Проекта)
set PROJECT_NUSPEC_FILE=%PROJECT_FILE_WITHOUT_EXT%.nuspec

rem Относительный путь к текущему каталогу (PackageBuild) из каталога Проекта
set CURRENT_DIR_FROM_PROJECT_DIR=..\MMS.Directory.Banks.Client.PackageBuild

rem Файл манифеста пакета, создаваемый по команде "nuget spec" для файла проекта в текущем каталоге (blankspec.cmd)
set BLANK_NUSPEC_FILE=blank.nuspec

rem Корректно заполненный файл манифеста для создания пакета командой "nuget pack" (pack.cmd)
set PACK_NUSPEC_FILE=package.nuspec

rem Маска новой версии пакета
set PACKAGE=%PROJECT_FILE_WITHOUT_EXT%.1.2.0.0

rem Ключ сервера местоположения пакетов (API key)
set API_KEY=74faaa07-2821-476c-9aec-d71f5a6f97bd

rem Сервер местоположения пакетов для "nuget push" (publish.cmd)
set SOURCE=https://mediamarkt.myget.org/F/default/api/v2/package

rem Сервер местоположения пакетов исходного кода /symbol package/ для "nuget push" (publish.cmd)
set SOURCE_SYM=https://nuget.symbolsource.org/mediamarkt/default