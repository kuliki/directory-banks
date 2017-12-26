@echo off
call __.cmd

copy /Y %PACK_NUSPEC_FILE% %PROJECT_DIR%\%PROJECT_NUSPEC_FILE%
pushd %PROJECT_DIR%
%NUGET_EXE% pack %PROJECT_FILE% -OutputDirectory %CURRENT_DIR_FROM_PROJECT_DIR% -Symbols -Build -Prop Configuration=Release -IncludeReferencedProjects
popd
