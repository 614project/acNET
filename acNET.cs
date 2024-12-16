/*
 * acNET
 * 
 * C# API Library for solved.ac
 * 
 * copyright 614project.
 */

#if DEBUG
/*
 * 미리 정의된 IsExternalInit가 없다는 오류 <<< vs2022 버그
 * https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined
 */
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}
#endif