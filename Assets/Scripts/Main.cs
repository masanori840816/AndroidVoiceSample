using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{   
    private const string PluginClassName = "jp.masanori.VoiceRecognitionActivity";
    [SerializeField]
    private TMP_Text _text;
    private void Start()
    {
        CallAndroidFunction();
        
    }


    public void CallAndroidFunction()
    {
        // Androidプラットフォームでのみ実行
        #if UNITY_ANDROID && !UNITY_EDITOR
        
        // 呼び出すプラグインクラスを取得
        using (var pluginClass = new AndroidJavaClass(PluginClassName))
        {
            // 静的メソッドの呼び出し
            string androidVersion = pluginClass.CallStatic<string>("getAndroidVersion");
            _text.text = ("Android Version: " + androidVersion);

            // 現在のUnityActivityを取得して、Activityが必要なメソッドを呼び出す例
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                
                // Activityを引数に渡すメソッドの呼び出し (この場合は戻り値なし)
                pluginClass.CallStatic("showLog", currentActivity, "Hello from Unity C#!");
            }
        }
        
        #endif
    }

}
