using UnityEngine;
using System.IO;

public class LogWriter : MonoBehaviour
{
    private string logFilePath;

    void Awake()
    {
        // ログファイルのパスを設定
        // Application.persistentDataPathはAndroidでは内部ストレージのアプリ固有の領域を指します。
        logFilePath = Path.Combine(Application.persistentDataPath, "app_log.txt");

        // 過去のログファイルを削除（任意）
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }

        // ログメッセージの受信イベントにカスタムメソッドを登録
        // このイベントは、Debug.Log/LogError/LogWarningが呼ばれるたびに発生します。
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        // アプリ終了時にイベントの登録を解除
        Application.logMessageReceived -= HandleLog;
    }

    // ログメッセージが届いたときに呼ばれるメソッド
    private void HandleLog(string logText, string stackTrace, LogType type)
    {
        // 出力形式を定義: [タイプ] [時刻] メッセージ\nスタックトレース
        string logEntry = $"[{type}] [{System.DateTime.Now:yyyy-MM-dd HH:mm:ss}] {logText}\n{stackTrace}\n";

        // ファイルに追記
        try
        {
            File.AppendAllText(logFilePath, logEntry);
        }
        catch (System.Exception e)
        {
            // ファイル書き込みエラーが発生した場合のデバッグログ
            Debug.LogError($"Failed to write log to file: {e.Message}");
        }
    }

    // ログファイルのパスをDebug.Logで出力するメソッド (確認用)
    public void PrintLogPath()
    {
        Debug.Log($"Log File Path: {logFilePath}");
    }
}
