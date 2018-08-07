using UnityEngine;
using System.Collections;
using CoreFramework;
using System;


#region Main Nav
//得到设备文件列表
public class N2C_SERVICE_INIT_COMPLETE : Simple_Msg
{
}
public class N2C_OfflineError : Simple_Msg
{
}
public class N2C_KeepOnline : Simple_Msg
{
}
public class N2C_ShowRemoteEventView : Simple_Msg
{

}
#endregion


#region FileSystem


#endregion


#region Capture Center


//录相设置回包
public class N2C_RecordSetup_Rsp : State_Base_Msg
{

}
//拍照设置回包
public class N2C_CaptureSetup_Rsp : State_Base_Msg
{

}
//慢摄相设置回包
public class N2C_SlowSetup_Rsp : State_Base_Msg
{

}
#endregion
