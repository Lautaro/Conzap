@ECHO OFF
ECHO This will install Conzap or update if already existing. Make sure Conzap.DLL is in same folder. 
PAUSE
gacutil -u conzap.dll
ECHO uninstalled previous version, next: reinstall current version
PAUSE
gacutil -i conzap.dll
ECHO conzap current version installed
PAUSE