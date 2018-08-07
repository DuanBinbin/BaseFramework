using UnityEngine;
using System.Collections;
using CoreFramework;
using System;


public class V2V_ShowHideSwitchBar : Bool_Base_Msg
{
}
public class V2V_Show_CaptureCenter : Simple_Msg
{
}
public class V2V_Show_FileSystem : Simple_Msg
{
}
public class V2V_Show_DeviceInfo : Simple_Msg
{
}
public class V2V_Show_ConnectView : Bool_Base_Msg
{
}

public class V2V_Enable_CaptureFunctionBar : Bool_Base_Msg
{
}

public class V2V_FileList_Empty :Bool_Base_Msg
{
               
}

public class V2V_LiveList_Empty : Bool_Base_Msg
{

}

public class V2V_CheckFileList_Empty : Simple_Msg
{
}

public class V2V_Show_LiveCenter : Simple_Msg
{
}

public class V2V_Capture_UIChange : Simple_Msg
{

}

public class V2V_LiveStream_Setting : Simple_Msg
{
    public string ResolutionValue = "";
    public string PrivacyValue = "";
}

public class V2V_liveStream_ShowUser : Bool_Base_Msg
{
    public string UserName;
}

public class V2V_Show_NoviceGuide : Simple_Msg
{

}