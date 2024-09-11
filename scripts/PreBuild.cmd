rmdir /S /Q %1temp
rem that scratch spellcheck file may be left open 
rem and it stops the xcopy from working
rem start /wait throws error into a new window
start /wait C:\Windows\System32\taskkill.exe /f /im WINWORD.exe /t
cmd c exit 0