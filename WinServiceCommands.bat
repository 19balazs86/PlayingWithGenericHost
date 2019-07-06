sc create MyWorkerService binPath="c:\svc\PlayingWithGenericHost.exe"
pause

sc START MyWorkerService
pause

sc STOP MyWorkerService
pause

sc DELETE MyWorkerService
pause