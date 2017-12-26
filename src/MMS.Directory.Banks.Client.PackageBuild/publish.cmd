@echo off
call __.cmd

for /F %%I in ('dir /B %PROJECT_FILE_WITHOUT_EXT%.*.nupkg') do (
set PACKAGE_FILE=%%I
goto endoffor
)
:endoffor

if "%PACKAGE_FILE%"=="" (
echo Package file not found.
exit 1
)

set SYMBOL_PACKAGE_FILE=%PACKAGE_FILE:.nupkg=%.symbols.nupkg

%NUGET_EXE% setApiKey %API_KEY% -Source %SOURCE%
%NUGET_EXE% push %PACKAGE_FILE% -Source %SOURCE%
%NUGET_EXE% setApiKey %API_KEY% -Source %SOURCE_SYM%
%NUGET_EXE% push %SYMBOL_PACKAGE_FILE% -Source %SOURCE_SYM%
