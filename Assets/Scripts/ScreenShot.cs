using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// エディターでだけ動くスクリーンショットを作成するクラス
/// </summary>
[TypeInfoBox("スクリーンショットを撮影するクラス。プレイモード中にSキーで撮影を行える。保存先は基本的にAssetと同じ階層のScreenShotsフォルダの中。プレイモード中でも下のフィールドに入力するとファイル名やディレクトリに変更を行える。")]
public class ScreenShot : MonoBehaviour
{
    [SerializeField, Tooltip("ファイルの頭につく文字")]
    private string imageTitle = string.Empty;

    [SerializeField, Tooltip("保存先のディレクトリ")]
    private string path = "ScreenShots/";
    

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
#if UNITY_EDITOR
        GameObject obj = new GameObject("ScrrenShooter",typeof(ScreenShot));
        DontDestroyOnLoad(obj);
#endif
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShootScreen();
        }
        
#endif
    }

    /// <summary>
    /// 現在表示されている画面の撮影を行う
    /// </summary>
    private void ShootScreen()
    {
        string fileName = imageTitle + GetTimeStamp() + ".png";
        
        ScreenCapture.CaptureScreenshot(path + fileName);
    }

    private string GetTimeStamp()
    {
        string time = DateTime.Now.ToString("yyyyMMddHHmmss");
        return time;
    }
}