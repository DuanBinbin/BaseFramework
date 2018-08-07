using UnityEngine;
using System.Collections;
using CoreFramework;
#region Global UI

public class N_Broadcast_ShowMeshModeChanged : Simple_Msg
{
}

public class N_Broadcast_SetOnline : Bool_Base_Msg
{
}
public class N_Broadcast_GetLocalFiles : Simple_Msg
{
}

public class N_Broadcast_DeleteRemoteFile : Message
{
    public bool _Success = true;   
}

public class N_Broadcast_ChangeLanguage : Simple_Msg
{
    public int language;
}


public class N_Broadcast_ChangeDeviceOrientation : Simple_Msg
{
    public DeviceOrientation _Orientation;
}

public class N_Broadcast_ChangeScreenOrientation : Simple_Msg
{
    public ScreenOrientation _Orientation;
}

public class N_Broadcast_UISceneLoadComplete : Simple_Msg
{
}

#endregion
