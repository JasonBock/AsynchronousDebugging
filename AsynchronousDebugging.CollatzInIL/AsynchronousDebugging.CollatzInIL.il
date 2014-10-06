// To compile: ilasm AsynchronouseDebugging.CollatzInIL.il /debug

.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern System.Numerics
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly AsynchronousDebugging.CollatzInConsole
{
  .permissionset reqmin
             = {[mscorlib]System.Security.Permissions.SecurityPermissionAttribute = {property bool 'SkipVerification' = bool(true)}}
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}

.module AsynchronousDebugging.CollatzInConsole.exe
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00020003    //  ILONLY 32BITREQUIRED

.class private auto ansi beforefieldinit AsynchronousDebugging.CollatzInConsole.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Code size       44 (0x2c)
    .maxstack  2
    .locals init ([0] valuetype [System.Numerics]System.Numerics.BigInteger 'value')
    IL_0000:  call       void AsynchronousDebugging.CollatzInConsole.Program::GetCallingMethod()
    IL_0005:  ldarg.0
    IL_0006:  brfalse.s  IL_002b

    IL_0008:  ldarg.0
    IL_0009:  ldlen
    IL_000a:  conv.i4
    IL_000b:  ldc.i4.1
    IL_000c:  bne.un.s   IL_002b

    IL_000e:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::get_Zero()
    IL_0013:  stloc.0
    IL_0014:  ldarg.0
    IL_0015:  ldc.i4.0
    IL_0016:  ldelem.ref
    IL_0017:  ldloca.s   'value'
    IL_0019:  call       bool [System.Numerics]System.Numerics.BigInteger::TryParse(string,
                                                                                    valuetype [System.Numerics]System.Numerics.BigInteger&)
    IL_001e:  brfalse.s  IL_002b

    IL_0020:  ldloc.0
    IL_0021:  call       class [mscorlib]System.IO.TextWriter [mscorlib]System.Console::get_Out()
    IL_0026:  call       void AsynchronousDebugging.CollatzInConsole.Program::Collatz(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                      class [mscorlib]System.IO.TextWriter)
    IL_002b:  ret
  } // end of method Program::Main

  .method private hidebysig static void  GetCallingMethod() cil managed noinlining
  {
    // Code size       29 (0x1d)
    .maxstack  2
    .locals init ([0] class [mscorlib]System.Diagnostics.StackFrame frame)
    IL_0000:  ldc.i4.1
    IL_0001:  newobj     instance void [mscorlib]System.Diagnostics.StackFrame::.ctor(int32)
    IL_0006:  stloc.0
    IL_0007:  call       class [mscorlib]System.IO.TextWriter [mscorlib]System.Console::get_Out()
    IL_000c:  ldloc.0
    IL_000d:  callvirt   instance class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Diagnostics.StackFrame::GetMethod()
    IL_0012:  callvirt   instance string [mscorlib]System.Reflection.MemberInfo::get_Name()
    IL_0017:  callvirt   instance void [mscorlib]System.IO.TextWriter::WriteLine(string)
    IL_001c:  ret
  } // end of method Program::GetCallingMethod

  .method public hidebysig static void  Collatz(valuetype [System.Numerics]System.Numerics.BigInteger 'value',
                                                class [mscorlib]System.IO.TextWriter writer) cil managed
  {
    // Code size       96 (0x60)
    .maxstack  2
    .locals init ([0] valuetype [System.Numerics]System.Numerics.BigInteger nextValue)
    IL_0000:  ldarg.1
    IL_0001:  ldarga.s   'value'
    IL_0003:  constrained. [System.Numerics]System.Numerics.BigInteger
    IL_0009:  callvirt   instance string [mscorlib]System.Object::ToString()
    IL_000e:  callvirt   instance void [mscorlib]System.IO.TextWriter::WriteLine(string)
    IL_0013:  ldarg.0
    IL_0014:  ldc.i4.1
    IL_0015:  conv.i8
    IL_0016:  call       bool [System.Numerics]System.Numerics.BigInteger::op_GreaterThan(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                          int64)
    IL_001b:  brfalse.s  IL_005f

    IL_001d:  ldarg.0
    IL_001e:  ldc.i4.2
    IL_001f:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Implicit(int32)
    IL_0024:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Modulus(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                                                                       valuetype [System.Numerics]System.Numerics.BigInteger)
    IL_0029:  ldc.i4.0
    IL_002a:  conv.i8
    IL_002b:  call       bool [System.Numerics]System.Numerics.BigInteger::op_Equality(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                       int64)
    IL_0030:  brtrue.s   IL_004b

    IL_0032:  ldc.i4.3
    IL_0033:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Implicit(int32)
    IL_0038:  ldarg.0
    IL_0039:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Multiply(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                                                                        valuetype [System.Numerics]System.Numerics.BigInteger)
    IL_003e:  ldc.i4.1
    IL_003f:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Implicit(int32)
    IL_0044:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Addition(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                                                                        valuetype [System.Numerics]System.Numerics.BigInteger)
    IL_0049:  br.s       IL_0057

    IL_004b:  ldarg.0
    IL_004c:  ldc.i4.2
    IL_004d:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Implicit(int32)
    IL_0052:  call       valuetype [System.Numerics]System.Numerics.BigInteger [System.Numerics]System.Numerics.BigInteger::op_Division(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                                                                        valuetype [System.Numerics]System.Numerics.BigInteger)
    IL_0057:  stloc.0
    IL_0058:  ldloc.0
    IL_0059:  ldarg.1
	tail.
    IL_005a:  call       void AsynchronousDebugging.CollatzInConsole.Program::Collatz(valuetype [System.Numerics]System.Numerics.BigInteger,
                                                                                      class [mscorlib]System.IO.TextWriter)
    IL_005f:  ret
  } // end of method Program::Collatz

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class AsynchronousDebugging.CollatzInConsole.Program
