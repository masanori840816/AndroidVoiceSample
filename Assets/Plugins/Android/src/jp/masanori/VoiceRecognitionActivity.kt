package jp.masanori;

import android.app.Activity
import android.util.Log

class VoiceRecognitionActivity {
companion object {
        private const val TAG = "UnityPlugin"

        // UnityのメインActivityを取得して利用する場合の例
        fun showLog(activity: Activity?, message: String) {
            activity?.runOnUiThread {
                Log.d(TAG, "Message from Unity: $message")
                // ここでAndroidのToast表示などUI操作も可能
            }
        }

        // 引数を取って値を返す静的メソッドの例
        @JvmStatic
        fun getAndroidVersion(): String {
            return android.os.Build.VERSION.RELEASE
        }
    }
}