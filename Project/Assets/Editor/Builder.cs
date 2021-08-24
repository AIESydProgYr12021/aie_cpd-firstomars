using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Threading;

public class Builder
{
    public static void Build()
    {
        string assetFolderPath = Application.dataPath;
        string pcFileName = assetFolderPath + "/../Builds/pc/MyGame.exe";
        string webFileName = assetFolderPath + "/../Builds/web/";
        string androidFileName = assetFolderPath + "/../Builds/android/MyGame.apk";

        var scenes = EditorBuildSettings.scenes;
        var levels = scenes.Select(z => z.path).ToArray();

        //webgl
        Debug.Log("Starting WebGL Build");
        BuildPipeline.BuildPlayer(levels, webFileName, BuildTarget.WebGL, BuildOptions.None);
        Debug.Log("Finished WebGL Build");

        //windows
        Debug.Log("Starting Windows Build");
        BuildPipeline.BuildPlayer(levels, pcFileName, BuildTarget.StandaloneWindows, BuildOptions.None);
        Debug.Log("Finished Windows Build");

        //android
        Debug.Log("Starting Android Build");
        BuildPipeline.BuildPlayer(levels, androidFileName, BuildTarget.Android, BuildOptions.None);
        Debug.Log("Finished Android Build");

        Thread.Sleep(2000);
    }
}
