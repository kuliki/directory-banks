@echo off
call __.cmd

pushd %PROJECT_DIR%
%NUGET_EXE% spec -Force
move /Y %PROJECT_NUSPEC_FILE% %CURRENT_DIR_FROM_PROJECT_DIR%\%BLANK_NUSPEC_FILE%
popd
