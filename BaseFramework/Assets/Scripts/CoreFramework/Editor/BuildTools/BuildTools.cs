using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class BuildTools
{
    #region android Sign
    public static string keystoreName
    {
        get
        {
            string s = PlayerPrefs.GetString("keystoreName");
            return s;
        }
        set
        {
            PlayerPrefs.SetString("keystoreName", value);
            PlayerPrefs.Save();
        }
    }
    public static string keystorePass
    {
        get
        {
            string s = PlayerPrefs.GetString("keystorePass");
            return s;
        }
        set
        {
            PlayerPrefs.SetString("keystorePass", value);
            PlayerPrefs.Save();
        }
    }
    public static string keyaliasName
    {
        get
        {
            string s = PlayerPrefs.GetString("keyaliasName");
            return s;
        }
        set
        {
            PlayerPrefs.SetString("keyaliasName", value);
            PlayerPrefs.Save();
        }
    }
    public static string keyaliasPass
    {
        get
        {
            string s = PlayerPrefs.GetString("keyaliasPass");
            return s;
        }
        set
        {
            PlayerPrefs.SetString("keyaliasPass", value);
            PlayerPrefs.Save();
        }
    }

    public static void SetSign(string keystore = "", string keystorepw = "", string keyalias = "", string keyaliaspw = "")
    {
        if (keystore.IsNullOrEmpty())
        {
            keystore = Application.dataPath + "/../public.keystore";
        }
        if (keystorepw.IsNullOrEmpty())
        {
            keystorepw = "12345678";
        }
        if (keyalias.IsNullOrEmpty())
        {
            keyalias = "pisoftware";
        }
        if (keyaliaspw.IsNullOrEmpty())
        {
            keyaliaspw = "12345678";
        }

        keystoreName = keystore;
        keystorePass = keystorepw;
        keyaliasName = keyalias;
        keyaliasPass = keyaliaspw;

        PlayerSettings.Android.keystoreName = BuildTools.keystoreName;
        PlayerSettings.Android.keystorePass = BuildTools.keystorePass;
        PlayerSettings.Android.keyaliasName = BuildTools.keyaliasName;
        PlayerSettings.Android.keyaliasPass = BuildTools.keyaliasPass;
    }
    #endregion




    [MenuItem("BuildTools/SDK GRADLE")]
    public static void BuildConfig_SDK()
    {
        BuildConfig_SDKTRIM();
    }
    public static void BuildConfig_SDKTRIM()
    {
        string srcPath = Application.dataPath + "/../../Design/Library/Hardware/Series/SDK/Plugins";
        string dstPath = Application.dataPath + "/Plugins";
        EmptyFolder(dstPath);
       
        CopyDirectory(srcPath, dstPath, false);

        dstPath = Application.dataPath + "/ThirdParty/UmengGameAnalytics";
        EmptyFolder(dstPath);


    }


   
    public static void BuildConfig_SDKAddLogo()
    {
        string srcPath = Application.dataPath + "/../../Design/Library/Hardware/Series/SDK/UI";
        string dstPath = Application.dataPath + "/ArtRes/2D/PIUI/UI";
        CopyDirectory(srcPath, dstPath, false);      
    }

    [MenuItem("BuildTools/USB ONEDEVICE")]
    public static void BuildConfig_USBWEBCAMERA()
    {
        BuildConfig_USB();
        List<EditorBuildSettingsScene> scenelist = new List<EditorBuildSettingsScene>();
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppEntry.unity", true));
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppMain_USB_ONEDEVICE.unity", true));
        EditorBuildSettings.scenes = scenelist.ToArray();

        AssetBundleMenu.RemoveAllSymbol();
        AssetBundleMenu.AddSymbol("USBONEDEVICE");
        PlayerSettings.productName = "小魅VROne";
    }
    [MenuItem("BuildTools/USB TEST")]
    public static void BuildConfig_USBTEST()
    {
        BuildConfig_USB();

        List<EditorBuildSettingsScene> scenelist = new List<EditorBuildSettingsScene>();
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppEntry.unity", true));
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppMain_USB_TEST.unity", true));
        EditorBuildSettings.scenes = scenelist.ToArray();
       
        AssetBundleMenu.RemoveAllSymbol();
        AssetBundleMenu.AddSymbol("USBTEST");
        PlayerSettings.productName = "小魅VR测试";
    }


    [MenuItem("BuildTools/USBOVERSEAS")]
    public static void BuildConfig_USBOverSeas()
    {
        BuildConfig_USB();

        AssetBundleMenu.RemoveAllSymbol();
        AssetBundleMenu.AddSymbol("OVERSEAS");


        PlayerSettings.productName = "Xmei";
        PlayerSettings.bundleVersion = AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_F");
        PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_CODE_F"));
        PlayerSettings.iOS.buildNumber = AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_CODE_F");
    }

    [MenuItem("BuildTools/USB")]
    public static void BuildConfig_USB()
    {
        string dv = "USB";
        string bundleid = "com.pi.usb";

        List<EditorBuildSettingsScene> scenelist = new List<EditorBuildSettingsScene>();
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppEntry.unity", true));
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppMain_USB.unity", true));
        EditorBuildSettings.scenes = scenelist.ToArray();
#if UNITY_5_6
        PlayerSettings.applicationIdentifier = bundleid;
#else
        PlayerSettings.bundleIdentifier = bundleid;
#endif
        PlayerSettings.productName = "小魅VR";
        //DeviceServiceSelector.SetDeviceService(dv);

        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;        

        CopyUsedFont(dv);
        CopyPictures(dv);

        SetSign("", "", bundleid, "");

        AssetBundleMenu.RemoveAllSymbol();

        PlayerSettings.bundleVersion = AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION");
        PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_CODE"));
        PlayerSettings.iOS.buildNumber = AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_CODE");
    }

    [MenuItem("BuildTools/USB_Gradle")]
    public static void BuildConfig_USBForGradle()
    {
        string dv = "USB";
        string bundleid = "com.pi.usb";
        string path = "D:/AndroidProj/USB";
        string TopFolder = Application.dataPath;
        TopFolder = TopFolder.Replace("Assets", "");

        List<EditorBuildSettingsScene> scenelist = new List<EditorBuildSettingsScene>();
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppEntry.unity", true));
        scenelist.Add(new EditorBuildSettingsScene("Assets/Scenes/AppMain_USB.unity", true));
        EditorBuildSettings.scenes = scenelist.ToArray();
            
#if UNITY_5_6
        PlayerSettings.applicationIdentifier = bundleid;
#else
        PlayerSettings.bundleIdentifier = bundleid;
#endif

        PlayerSettings.productName = "小魅VR";
        //DeviceServiceSelector.SetDeviceService(dv);

        CopyUsedFont(dv);
        CopyPictures(dv);

        SetSign("", "", bundleid, "");

        // export android project
        if (Directory.Exists(path))
        {     
            AndroidPackage.DeleteFolder(path);       
            //File.Delete(path);
        }
        
        //PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android,ApiCompatibilityLevel.NET_2_0);
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;

        string rt = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes,path,BuildTarget.Android,BuildOptions.AcceptExternalModificationsToPlayer);
        Debug.Log("BuildConfig_USB:BuildPipeline.BuildPlayer = " + rt);

        string projPath = path + "/" + Application.productName + "/";
        string resPath = path + "/unity-android-resources/";

        Debug.Log("sourceDirName : " + projPath);
        Debug.Log("destDirName : " + path + "/unity");
        Directory.Move(projPath, path + "/unity");
        projPath = path + "/unity/";

        CopyDirectory(TopFolder + "Assets/StreamingAssets", projPath + "assets",true);
      

        AssetBundleMenu.RemoveAllSymbol();
        PlayerSettings.bundleVersion = AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION");
        PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_CODE"));
        PlayerSettings.iOS.buildNumber = AppInfoConfig.Instance.GetValueByKey("BUNDLE_VERSION_CODE");
    }

    #region Helper Functions

    static void CopyUsedFont(string dv)
    {
        //to do delete在Resources文件夹下是否为空，有的清空 System.IO.File.Delete删除所有的项，然后才能加载；
        string[] files = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/Font/");
        foreach (string file in files)
        {
            System.IO.File.Delete(file);
        }


        List<string> fontsUsed = new List<string>();
        AddOneFont("EFT_COMMON", ref fontsUsed);
        AddOneFont("EFT_TITLE", ref fontsUsed);
        AddOneFont("EFT_SMALL", ref fontsUsed);


        foreach (string font in fontsUsed)
        {
            string from = Application.dataPath + "/../../Design/Library/" + font + ".ttf";
            string to = Application.dataPath + "/Resources/" + font + ".ttf";

            try
            {
                System.IO.File.Copy(from, to);

            }
            catch (Exception ex)
            {
                Debug.LogError(ex);

            }
        }



    }
    static void AddOneFont(string key, ref List<string> list)
    {
        string s = AppInfoConfig.Instance.GetValueByKey(key);
        
        string font = s.Split('-')[2];
        if (!list.Contains(font))
        {
            list.Add(font);
        }
    }

    static void DeleteFile(string file)
    {
        //去除文件夹和子文件的只读属性
        //去除文件夹的只读属性
        System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
        fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
        //去除文件的只读属性
        System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
        //判断文件夹是否还存在
        if (Directory.Exists(file))
        {
            foreach (string f in Directory.GetFileSystemEntries(file))
            {

                if (!f.Contains(".svn") && !f.Contains(".meta"))
                {
                    if (File.Exists(f))
                    {
                        //如果有子文件删除文件
                        File.Delete(f);
                    }
                    else
                    {
                        //循环递归删除子文件夹 
                        DeleteFile(f);
                    }

                }


            }
            //删除空文件夹 
            try
            {

            }
            catch(Exception e)
            {
                Directory.Delete(file);

            }
        }
    }

    static void CopyPictures(string dv)
    {
        string from = Application.dataPath + "/../../Design/Library/Hardware/Series/" + dv + "/UI";
        string to = Application.dataPath + "/ArtRes/2D/PIUI/UI";
        if (!from.Contains(".svn") && !to.Contains(".svn") && !to.Contains(".meta"))
        {
            if (Directory.Exists(to))
            {
                DeleteFile(to);
            }
            try
            {

                CopyDirectory(from, to, true);

            }
            catch (Exception ex)
            {
                Debug.LogError(ex);

            }
        }


        AssetDatabase.Refresh();
        
    }

    private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
    {
        bool ret = false;
        try
        {
            SourcePath = SourcePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? SourcePath : SourcePath + Path.DirectorySeparatorChar;
            DestinationPath = DestinationPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? DestinationPath : DestinationPath + Path.DirectorySeparatorChar;

            if (Directory.Exists(SourcePath))
            {
                if (Directory.Exists(DestinationPath) == false)
                    Directory.CreateDirectory(DestinationPath);

                foreach (string fls in Directory.GetFiles(SourcePath))
                {
                    FileInfo flinfo = new FileInfo(fls);

                    if(!flinfo.FullName.Contains(".svn"))
                    {
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    else
                    {
                        Debug.Log("SVN Files");
                    }
                }
                foreach (string drs in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo drinfo = new DirectoryInfo(drs);
                    if(!drinfo.FullName.Contains(".svn"))
                    {
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;

                    }
                    else
                    {
                        Debug.Log("SVN Files");
                    }

                }
            }
            ret = true;
        }
        catch (Exception ex)
        {
            ret = false;
        }
        return ret;
    }
    private static void EmptyFolder(string SourcePath)
    {
        DirectoryInfo dir = new DirectoryInfo(SourcePath);
        EmptyFolder(dir);
    }
    private static void EmptyFolder(DirectoryInfo directory)

    {

        foreach (FileInfo file in directory.GetFiles())

        {
            if(file.FullName.Contains(".svn"))
            {

            }
            else
            {
                try
                {
                    file.Delete();

                }
                catch(Exception e)
                {

                }

            }

        }

        foreach (DirectoryInfo subdirectory in directory.GetDirectories())

        {

            EmptyFolder(subdirectory);
            if (subdirectory.FullName.Contains(".svn"))
            {

            }
            else
            {
                try
                {
                    subdirectory.Delete();
                }
                catch (Exception e)
                {

                }
            }

        }

    }
#endregion
}
