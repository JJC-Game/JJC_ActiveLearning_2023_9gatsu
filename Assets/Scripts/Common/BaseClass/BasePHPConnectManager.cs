using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class BasePHPConnectManager : MonoBehaviour
{
    protected string serverURL = "http://18.178.60.234/students/active_larning/";
    protected string userPHPFolderPath = "user00/PHP/";
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
    }

    public void CallError()
    {
        Debug.Assert(false);
    }

    protected IEnumerator UrlAccess(string url, UnityAction callbackFunc = null, UnityAction errorCallbackFunc = null)
    {
        WWWForm form = new WWWForm();
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("error:" + www.error);
                Debug.Log("errorURL:" + url);
                if (errorCallbackFunc != null)
                {
                    errorCallbackFunc();
                }
                isConnecting = false;
                yield break;
            }
            Debug.Log("resultText:" + www.text);

            phpConnectResultText = www.text;

            isConnecting = false;
            if (callbackFunc != null)
            {
                callbackFunc();
            }
        }
    }

    abstract public void CallUserData(int userId);
    abstract public void CallPlayGacha(int userId);
    abstract public void CallReserCharaHasFlag(int userId);

}
