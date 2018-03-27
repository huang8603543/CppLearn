using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum eBuildPlatform
{
    Windows = 0,
    Mac,
    IOS,
    Android
}

[ExecuteInEditMode]
public static class AssetBundleBuild
{
    #region OnBuildInterface

    [MenuItem("Assets/平台打包/Windows(非压缩)")]
    public static void OnBuildPlatformWindowsNoCompress()
    {
        BuildPlatform(eBuildPlatform.Windows);
    }

    #endregion

    private static string m_PackageVersion = string.Empty;
    private static string m_LastPackageVersion = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="compressType"></param>
    /// <param name="isMd5"></param>
    /// <param name="isForceAppend"></param>
    static void BuildPlatform(eBuildPlatform platform, int compressType = 0, bool isMd5 = false, bool isForceAppend = false)
    {
        //m_PackageVersion = string.Empty;
        //m_LastPackageVersion = string.Empty;
        List<string> resList = GetResAllDirPath();
    }

    static List<string> GetResAllDirPath()
    {
        return new List<string>();
    }
}
