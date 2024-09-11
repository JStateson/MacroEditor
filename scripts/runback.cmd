rmdir /s /Q TempBU
mkdir TempBU
cd TempBU
xcopy ..\..\bin\x64\Release\*.txt .
xcopy ..\..\bin\x64\Release\LOCALIMAGEFILE*.png .
del MyH*.*
tar -cvzf raw.tar *.txt
del *.txt

