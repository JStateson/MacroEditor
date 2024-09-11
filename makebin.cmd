rem $(SolutionDir) $(TargetDir)  $(TargetName) $(TargetPath) is % 1..4
rem   \"scripts"      release\     name of app     path to app
rem replace %2 with %2DataFiles\ and %1 is where the main folders as such as sources, scripts, etc
rem
mkdir %1temp
mkdir %1temp\DataFiles
set ARC=binaries-for-testing.tar
del %1%ARC%
set SRC=%2
set IS_64=%SRC:~-12,-9%
if %IS_64% == x64 (
rem
rem the release has the latest as that is where editing is to be done
rem there might be new document files in sources that need to be pushed out
replace /A %1sources\*.docx %2DataFiles
replace /A %1sources\*.html %2DataFiles
replace /U /S %2DataFiles\*.docx %1sources
replace /U /S %2DataFiles\*.html %1sources
rem
set PGM=%2%364.exe
xcopy %2*agil*.dll %1temp
rem
rem copy entire folder after deleteing the one in debug
rmdir /s /q %2..\Debug\DataFiles
xcopy /E  /I /Q /Y %2DataFiles %2..\Debug\DataFiles
xcopy /E  /I /Q /Y %2DataFiles %1temp\DataFiles
rem
xcopy %4 %1temp
xcopy %userprofile%\Downloads\macros.html %1temp\DataFiles
cd %1
tar -z -cf %1%ARC%  temp
) else (
set PGM=%2%332.exe
)
cd ..
rmdir /S /Q %1temp