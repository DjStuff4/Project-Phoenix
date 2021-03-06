; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{21EB058E-2477-4929-9FAA-1D92ABA7AD74}
AppName=DjStuff's Novetus Expansion Pack: Phoenix Edition
AppVersion=2.1
;AppVerName=DjStuff's Novetus Expansion Pack: Phoenix Edition 2.1
AppPublisher=DjStuff's Projects
AppPublisherURL=https://sites.google.com/view/novetusexpansionthegrave
AppSupportURL=https://sites.google.com/view/novetusexpansionthegrave
AppUpdatesURL=https://sites.google.com/view/novetusexpansionthegrave
DefaultDirName={pf}\DjStuff Projects\Novetus 1.1 Ultimate
DefaultGroupName=DjStuff Projects\Novetus Expansion Pack\PreReady
AllowNoIcons=yes
LicenseFile=C:\Users\hunte\OneDrive\Desktop\Expansion Pack\Pre-Ready\PACK EULA.txt
InfoAfterFile=C:\Users\hunte\OneDrive\Desktop\Expansion Pack\Thank You and Credits.txt
OutputDir=C:\Users\hunte\OneDrive\Desktop\Expansion Pack\outputs
OutputBaseFilename=2.1PreReadysetup
SetupIconFile=C:\Users\hunte\OneDrive\Desktop\Expansion Pack\Pre-Ready\NovetusIcon.ico
Compression=lzma
SolidCompression=yes

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\hunte\OneDrive\Desktop\Expansion Pack\Pre-Ready\Novetus.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\hunte\OneDrive\Desktop\Expansion Pack\Pre-Ready\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\DjStuff's Novetus Expansion Pack: Phoenix Edition"; Filename: "{app}\Novetus.exe"
Name: "{group}\{cm:ProgramOnTheWeb,DjStuff's Novetus Expansion Pack: Phoenix Edition}"; Filename: "https://sites.google.com/view/novetusexpansionthegrave"
Name: "{group}\{cm:UninstallProgram,DjStuff's Novetus Expansion Pack: Phoenix Edition}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\DjStuff's Novetus Expansion Pack: Phoenix Edition"; Filename: "{app}\Novetus.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\DjStuff's Novetus Expansion Pack: Phoenix Edition"; Filename: "{app}\Novetus.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\Novetus.exe"; Description: "{cm:LaunchProgram,DjStuff's Novetus Expansion Pack: Phoenix Edition}"; Flags: nowait postinstall skipifsilent
