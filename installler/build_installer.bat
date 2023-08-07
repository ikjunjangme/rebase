@ECHO OFF
SETLOCAL

SET "sourcedir=..\dist"

C:\Users\jaelo\Utils\nsis-binary-7140-3\makensisw.exe NKAI_Gateway_Installer.nsi
taskkill /IM  "makensisw.exe" /F'

GOTO :EOF