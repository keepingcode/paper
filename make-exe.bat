@rem
@rem Script de compilação do executável para Windows
@rem
@echo off

dotnet publish -c Release -r win10-x64 -f net462
@rem dotnet publish -c Release --no-restore -r win10-x64 -f netcoreapp2.0

pause
