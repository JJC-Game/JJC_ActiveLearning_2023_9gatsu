using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

abstract public class BasePHPConnectManager : MonoBehaviour
{
    protected string serverURL = "http://18.178.60.234/students/active_larning/";
    protected string userPHPFolderPath = "user99/";
    protected string phpConnectResultText = "";
    private bool isConnecting = false;
    protected int calledUserId = 0;

    protected void CallPHPConnection(string phpFileName, UnityAction callbackFunc = null)
    {
        if (isConnecting)
        {
            Debug.LogAssertion("別の接続を実行中です.");
            return;
        }
        string url = serverURL + userPHPFolderPath + phpFileName;
        isConnecting = true;
        StartCoroutine(UrlAccess(url, () => callbackFunc(), () => CallError()));

        var request = UnityWebRequest.Get(url);
    }

    public void CallError()
    {
        Debug.Assert(false);
    }

    protected IEnumerator UrlAccess(string url, UnityAction callbackFunc = null, UnityAction errorCallbackFunc = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        // リクエスト送信
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("error:" + request.error);
            Debug.Log("errorURL:" + url);

            if (errorCallbackFunc != null)
            {
                errorCallbackFunc();
            }
            isConnecting = false;
            yield break;
        }
        else
        {
            // 結果をテキストとして表示します
            Debug.Log("resultText:" + request.downloadHandler.text);

            phpConnectResultText = request.downloadHandler.text;

            isConnecting = false;
            if (callbackFunc != null)
            {
                callbackFunc();
            }
        }
    }

    abstract public void CallUserData(int userId);
    abstract public void CallPlayGacha(int userId);
    abstract public void CallPlayGacha10(int userId);
    abstract public void CallClearHasCharaFlag(int userId);

}
