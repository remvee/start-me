@echo off

for %%F in (%0) do set dirname=%%~dpF
set exec="%dirname%start_me_handler.exe"

echo The following line should say "OK"
%exec% /say-ok
if not %errorLevel% == 0 (
   echo.
   echo !!!!! Run this script as administrator !!!!!
   echo.
   pause
   exit 1
) else (
   echo.
   pause
)

assoc .start_me=start_me
ftype start_me=%exec% "%%1"
if not %errorLevel% == 0 (
   cls
   echo.
   echo !!!!! Run this script as administrator !!!!!
   echo.
   pause
   exit 1
) else (
   cls
   echo Start-me should now be successfully installed.
   echo.
)

pause
