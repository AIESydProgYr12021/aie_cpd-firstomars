@echo off

pause

REM : Default install directory for unity via UnityHub
REM : If building on school computers - this directory will need to be edited
SET UnityDir="C:\Program Files\Unity\Hub\Editor\2020.3.12f1\Editor\Unity.exe"



IF exist %UnityDir% (
	echo "Unity Found, Please wait while we attempt to build, this may take a while"
	%UnityDir% -batchmode -logFile -quit -projectPath "%cd%" -executeMethod Builder.Build
	echo "Build Complete"
	goto :endofscript
)

REM : Unity has been installed in a different location, or it does not exist
echo "Unity Not found, please update this build script"


:endofscript
pause