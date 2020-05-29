# EPSDK

The custom SDK for the EP.
Uses WPF VB.NET .NET 4.0.

Icon for the BMC is from: http://www.designcontest.com
BMC is based on Bakonbot's Map converter found here: https://github.com/BakonBot/legacy-place-converter
Example config.ini file:
~~~~~~~~ 

[NovetusSDK]
Location=!currentdir!
EXE=Novetus.exe
ARGS=-sdk
Desc=The normal Novetus SDK.

[Place converter]
Location=!currentdir!
EXE=RLPC.EXE
ARGS=
Desc=The place converter used in 1.2.
