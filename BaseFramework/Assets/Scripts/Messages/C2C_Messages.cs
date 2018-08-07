using UnityEngine;
using System.Collections;
using CoreFramework;
using System.Collections.Generic;



public class C2C_Show_Player : Message
{   
    public int _StartIndex;
}


public class C2C_Play_File : Message
{
    public int _StartIndex;
}
public class C2C_Seek_VideoFile : Message
{

    public float _Progress;
}
public class C2C_Pause_VideoFile : Message
{
    
}
public class C2C_Resume_VideoFile : Message
{
    
}

public class C2C_Stop_VideoFile : Message
{
        
}

public class C2C_Delelte_VideoFile : Message
{
    public string FileName;
    public string FilePath;
}

public class C2C_Request_VideoProgress : Message
{
}
public class C2C_Respons_VideoProgress : Message
{
    public float _Progress;
}
public class C2C_Playing_File_Full_Screen : Message
{
    public bool _IsFullScreen = false;
}


public class C2C_Change_VRMode : Message
{
    public bool _IsFullScreen = true;
    public Color _BgColor = Color.black;
    public Rect _CamSize = new Rect(0, 0, 1, 1);
}
public class C2C_OpenCloseVRView : Message
{
    public bool _IsShow = true;
}
public class C2C_SetSwitchBar_Index : Simple_Msg
{
    public int _Index;
}
public class C2C_OpenCaptureCenterView : Simple_Msg
{
}

public class C2C_OpenConnectionView : Simple_Msg
{
}


public class C2C_OpenCloseGravity : Bool_Base_Msg
{

}
public class C2C_CamDirUpDown : Bool_Base_Msg
{

}
public class C2C_TFCardPullOut : Bool_Base_Msg
{

}

public class C2C_DownlodingFile : Bool_Base_Msg
{

}


public class C2C_AndroidMediaPlayerSizeReady : Message
{
    public int _Width;
    public int _Height;
}

public class C2C_Play_LiveFile : Message
{
    public string LiveUrl;
}

public class C2C_StopPlay_LiveFile : Message
{

}

public class C2C_LiveStream_RTMP_Start : Simple_Msg
{

}

