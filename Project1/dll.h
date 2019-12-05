#ifdef __cplusplus
extern "C" {
#endif

#include <windows.h>
#include <stdio.h>
#include <TlHelp32.h>
#include <sys/types.h> 

#define NT_SUCCESS(x) ((x) >= 0)
#define STATUS_INFO_LENGTH_MISMATCH 0xc0000004

#define SystemHandleInformation 16
#define ObjectBasicInformation 0
#define ObjectNameInformation 1
#define ObjectTypeInformation 2
	
	
__declspec(dllexport) void RunA();
__declspec(dllexport) int Run(char* HandleName);
__declspec(dllexport) void KillProcess(int pid);

#ifdef __cplusplus
}
#endif
