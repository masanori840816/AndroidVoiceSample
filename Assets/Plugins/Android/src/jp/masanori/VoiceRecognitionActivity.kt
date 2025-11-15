package jp.masanori;

import android.app.Activity
import android.os.Bundle
import android.Manifest
import android.speech.RecognitionListener
import android.speech.SpeechRecognizer
import android.content.Intent
import android.speech.RecognizerIntent
import android.content.Context
import android.os.Handler
import android.os.Looper
import android.util.Log
import com.unity3d.player.UnityPlayer

class VoiceRecognitionActivity private constructor(private val context: Context) {
    private val unityGameObjectName = "SpeechManager"
    private val unityMethodName = "OnRecognitionResult"
    private val unityErrorMethodName = "OnRecognitionError"

    companion object {
        private const val TAG = "UnityPlugin"
        @JvmStatic
        private var instance: VoiceRecognitionActivity? = null
        @JvmStatic
        fun getInstance(): VoiceRecognitionActivity {
            if (instance == null) {
                // UnityPlayer.currentActivity を使用して Context/Activity を取得する
                val currentContext = UnityPlayer.currentActivity
                instance = VoiceRecognitionActivity(currentContext)
            }
            return instance!!
        }

        // showLogも currentActivity を使うように修正
        @JvmStatic
        fun showLog(message: String) {
            Log.d(TAG, "Message from Unity: $message")
            // getInstanceで Context を取得するように修正したので、引数から Context を削除
            val vr = getInstance() 
            vr.sendUnityMessage("Message To Unity $message")
        }

        @JvmStatic
        fun getAndroidVersion(): String {
            return android.os.Build.VERSION.RELEASE
        }
    }
    private fun sendUnityMessage(message: String) {
        UnityPlayer.UnitySendMessage(unityGameObjectName, unityMethodName, message)
        Log.d(TAG, "Sent result to Unity: $message")
    }
    
    private fun sendUnityError(errorMessage: String) {
        UnityPlayer.UnitySendMessage(unityGameObjectName, unityErrorMethodName, errorMessage)
        Log.e(TAG, "Sent error to Unity: $errorMessage")
    }
}