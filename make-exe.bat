@rem
@rem Script de compilação do executável para Windows
@rem
@echo off

dotnet publish -c Debug --no-restore -r win10-x64
pause
