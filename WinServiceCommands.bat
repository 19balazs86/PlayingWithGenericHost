sc create MyFileService binPath="c:\svc\PlayingWithGenericHost.exe"
pause

sc START MyFileService
pause

sc STOP MyFileService
pause

sc DELETE MyFileService
pause