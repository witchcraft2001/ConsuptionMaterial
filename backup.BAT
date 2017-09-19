rem @echo off
cls
@echo Backup sources v1.1 by Mikhaltchenkov Dmitry
@echo.
setlocal enabledelayedexpansion
set arctype=rar
set arcparams=a -dh -r -x*.*log -x*.rar -x*.pdb -x*.idb -x*.tmp -x*.obj
set arcexe="%ProgramFiles%"\WinRAR\Rar.exe
if exist = %arcexe% goto backup
set arcexe="%ProgramFiles(x86)%"\WinRAR\Rar.exe
if exist = %arcexe% goto backup
set arcexe="%ProgramW6432%"\WinRAR\Rar.exe
if exist = %arcexe% goto backup
set arcparams=a -r -ssw
set arctype=zip
set arcexe="%ProgramFiles%"\7-Zip\7z.exe
if exist = %arcexe% goto backup
set arcexe="%ProgramFiles(x86)%"\7-Zip\7z.exe
if exist = %arcexe% goto backup
set arcexe="%ProgramW6432%"\7-Zip\7z.exe
if exist = %arcexe% goto backup

@echo "Ошибка! Не найден архиватор!!!"
pause 0
goto end
:backup

set hour=%time:~0,2%
if "%hour:~0,1%" == " " set hour=0%hour:~1,1%
set min=%time:~3,2%
if "%min:~0,1%" == " " set min=0%min:~1,1%
set secs=%time:~6,2%
if "%secs:~0,1%" == " " set secs=0%secs:~1,1%

set d=%date:~0,2%
set m=%date:~3,2%
set y=%date:~6,4%
set arch_date=%y%%m%%d%-%hour%%min%%secs%

rem echo %arch_date%

set name=%cd%
pushd ..
rem cd ..
rem @echo !name:%cd%\=!
set arcName=!name:%cd%\=!
popd

if not exist "..\backup" (
	mkdir "..\backup"
)
%arcexe% %arcparams% ..\backup\"%arcName%_"%arch_date%.%arctype% *.*

:end
endlocal
exit