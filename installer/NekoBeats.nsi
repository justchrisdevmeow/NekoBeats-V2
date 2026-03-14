!include "MUI2.nsh"

; Name and file
Name "NekoBeats"
OutFile "NekoBeats-2.3-installer.exe"
InstallDir "$PROGRAMFILES\NekoBeats"

; MUI Settings
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

; Installer settings
SetCompress force
SetDatablockOptimize on
CRCCheck on
XPStyle on

; Banner images
!define MUI_WELCOMEFINISHPAGE_BITMAP "installer/NekoBeatsBanner.bmp"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "installer/NekoBeatsHeader.bmp"
!define MUI_HEADERIMAGE_HEIGHT 57
!define MUI_HEADERIMAGE_RIGHT

; Installation
Section "Install"
  SetOutPath "$INSTDIR"
  
  File "..\bin\Release\net8.0-windows\win-x64\publish\NekoBeats.exe"
  File "..\bin\Release\net8.0-windows\win-x64\publish\*.dll"
  File "NekoBeatsLogo.ico"
  File "NekoBeatsLogo.png"
  
  CreateDirectory "$SMPROGRAMS\NekoBeats"
  CreateShortCut "$SMPROGRAMS\NekoBeats\NekoBeats.lnk" "$INSTDIR\NekoBeats.exe" "" "$INSTDIR\NekoBeatsLogo.ico"
  CreateShortCut "$SMPROGRAMS\NekoBeats\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
  CreateShortCut "$DESKTOP\NekoBeats.lnk" "$INSTDIR\NekoBeats.exe" "" "$INSTDIR\NekoBeatsLogo.ico"
SectionEnd

; Uninstaller
Section "Uninstall"
  Delete "$INSTDIR\NekoBeats.exe"
  Delete "$INSTDIR\*.dll"
  Delete "$INSTDIR\NekoBeatsLogo.ico"
  Delete "$INSTDIR\NekoBeatsLogo.png"
  Delete "$INSTDIR\Uninstall.exe"
  RMDir "$INSTDIR"
  
  Delete "$SMPROGRAMS\NekoBeats\NekoBeats.lnk"
  Delete "$SMPROGRAMS\NekoBeats\Uninstall.lnk"
  RMDir "$SMPROGRAMS\NekoBeats"
  Delete "$DESKTOP\NekoBeats.lnk"
SectionEnd
