del /q ..\bin\x64\debug\*macros.txt
xcopy /y ..\bin\x64\release\*macros.txt ..\bin\x64\debug\*macros.txt
copy /y ..\bin\x64\release\signatures.txt ..\bin\x64\debug\signatures.txt
xcopy /y /I ..\bin\x64\release\*.png ..\bin\x64\debug\
xcopy /y /I ..\bin\x64\release\*.rtf ..\bin\x64\debug\