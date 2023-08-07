;Include Modern UI
!include "MUI2.nsh"

;--------------------------------
; define const
!define EXE_NAME "nk-api-gateway"
!define APP_NAME "NKAIGateway"

!define APP_SERVICE_NAME "NKAIGateway"

!define NSSM_NAME "nssm"

!define HELP_LINK "www.nextk.co.kr"
!define PRODUCT_VERSION "1.0.0.0"

!define MANUFACTURER "NextK"
!define ROOT_PATH "..\"

!define APP_PATH "${ROOT_PATH}\dist"
!define SERVICE_PATH "${ROOT_PATH}\dist"

!define INSTALL_UTILS_PATH "C:\Install_Utils"

!define NSSM_PATH "${INSTALL_UTILS_PATH}\nssm-2.24\win64"
!define MUI_ICON "${INSTALL_UTILS_PATH}\logo\logo.ico"
!define MUI_UNICON "${INSTALL_UTILS_PATH}\logo\logo.ico"
!define MUI_WELCOMEFINISHPAGE_BITMAP "${INSTALL_UTILS_PATH}\logo\welcome.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "${INSTALL_UTILS_PATH}\logo\welcome.bmp"

;Pre define
;--------------------------------

;--------------------------------
;General
Name ${APP_NAME}
InstallDir "$PROGRAMFILES\NextK\${APP_NAME}"

RequestExecutionLevel admin
Unicode True
;--------------------------------

;!define MUI_HEADERIMAGE
;!define MUI_HEADERIMAGE_BITMAP "path\to\InstallerLogo.bmp"
;!define MUI_HEADERIMAGE_RIGHT

;--------------------------------
;Pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
 
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH
;--------------------------------
;Languages
!insertmacro MUI_LANGUAGE "English"
;--------------------------------

; Pages
Section -Prerequisites
  SetOutPath $INSTDIR\Prerequisites
  
  MessageBox MB_YESNO "If visual studio 2015 - 2019 redistributable package is not installed, please proceed with the installation." /SD IDYES IDNO endActiveSync
	File ".\Prerequisites\VC_redist.x64.exe"
	ExecWait '"$INSTDIR\Prerequisites\VC_redist.x64.exe"'
    Goto endActiveSync
  endActiveSync:
SectionEnd

;install sections
Section "${APP_NAME}(required)" NextkApiGateway
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  File ${MUI_ICON}
  
  File ${APP_PATH}\msvcp140d.dll
  File ${APP_PATH}\ucrtbased.dll
  File ${APP_PATH}\vcruntime140_1d.dll
  File ${APP_PATH}\vcruntime140d.dll
  
  ; exe requeirement
  File ${APP_PATH}\${EXE_NAME}.exe
  ; exe requeirement
  
  File ${APP_PATH}\*.json
  
  ;;dlls
  File ${APP_PATH}\msvcp140d.dll
  File ${APP_PATH}\ucrtbased.dll
  File ${APP_PATH}\vcruntime140_1d.dll
  File ${APP_PATH}\vcruntime140d.dll
  ;; dlls
  
  File /a /r ${NSSM_PATH}\nssm.exe
  
  ;; remove preeqeuqisties
  RMDir /r "$INSTDIR\Prerequisites"
  ;; remove preeqeuqisties
   
  ;; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\${APP_NAME} "Install_Dir" "$INSTDIR"
  ;;
  
  ;;This is to fix the pop-up window alignment that is different for each manufacturer.
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows" "MenuDropAlignment" "0"
  ;; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "DisplayName" "${APP_NAME}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "HelpLink" "${HELP_LINK}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "Publisher" "${MANUFACTURER}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "DisplayIcon" "$INSTDIR\logo.ico"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}" "NoRepair" 1
  WriteUninstaller "$INSTDIR\uninstall.exe" 
  
  ExecWait '$INSTDIR\${NSSM_NAME}.exe install ${APP_SERVICE_NAME} "$INSTDIR\${EXE_NAME}.exe"'
  ExecWait '"sc.exe" start ${APP_SERVICE_NAME}'
SectionEnd

; Optional section (can be disabled by the user)
Section "Create Desktop Shortcuts"
  SetOutPath $INSTDIR
  CreateShortCut "$DESKTOP\${APP_NAME}.lnk" "$INSTDIR\${EXE_NAME}.exe"
SectionEnd

Section "Uninstall"    
  ExecWait '"sc.exe" stop ${APP_SERVICE_NAME}'
  ExecWait "$INSTDIR\${NSSM_NAME}.exe remove ${APP_SERVICE_NAME} confirm"

  ExecWait 'taskkill /IM  "${EXE_NAME}.exe" /F'
  ExecWait 'taskkill /IM  "nginx.exe" /F'  
  
  Delete "$DESKTOP\${APP_NAME}.lnk"
  
  ;; Remove shortcuts, if any
  Delete "$SMPROGRAMS\${APP_NAME}\*.*"
  
  ;; Remove directories used
  RMDir  "$INSTDIR\log"
  RMDir  "$SMPROGRAMS\${APP_NAME}"
  RMDir /r "$INSTDIR"
  RMDir /r $INSTDIR
  
  RMDir /r $LOCALAPPDATA\${APP_NAME}
  
  ;; Remove files and uninstaller
  Delete $INSTDIR\${EXE_NAME}.exe
  Delete $INSTDIR\*.exe
  Delete $INSTDIR\log\*.*
  Delete $INSTDIR\*.json
  Delete $INSTDIR\*.dll
  Delete $INSTDIR\*
  Delete $INSTDIR\*.*
  
  RMDir "$INSTDIR"
  
  RMDir $INSTDIR
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}"
  DeleteRegKey HKLM SOFTWARE\${APP_NAME}
SectionEnd
