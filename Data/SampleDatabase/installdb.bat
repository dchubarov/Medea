@echo off
xcopy entity.db "%USERPROFILE%\Application Data\Gyro Software\Medea\"
xcopy /S blobs\*.* "%USERPROFILE%\Application Data\Gyro Software\Medea\blobs\"