SET rootpath=%~dp0
for /D %%i in (*) do 7z a -tzip "%%i.zip" "%rootpath%%%i/RED" -mx9 -r