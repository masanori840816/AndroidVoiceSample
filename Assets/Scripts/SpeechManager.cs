using TMPro;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{   
    private const string PluginClassName = "jp.masanori.VoiceRecognitionActivity";
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private TMP_Text _message;
    private void Start()
    {
        CallAndroidFunction();
        
    }


    public void CallAndroidFunction()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        
        using (var pluginClass = new AndroidJavaClass(PluginClassName))
        {
            string androidVersion = pluginClass.CallStatic<string>("getAndroidVersion");
            _text.text = ("Android Version: " + androidVersion);

            pluginClass.CallStatic("showLog", "Hello from Unity C#!");
        }
        
        #endif
    }
    public void OnRecognitionResult(string recognizedText)
    {
        if (string.IsNullOrEmpty(recognizedText))
        {
            _message.text = "認識結果: No Match";
        }
        else
        {
            _message.text = "認識結果: " + recognizedText;
        }
    }
    public void OnRecognitionError(string errorMessage)
    {
        _message.text = "音声認識エラー: " + errorMessage;
    }
}
