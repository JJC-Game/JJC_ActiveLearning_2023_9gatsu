using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class P3_UserDataManager : MonoBehaviour
{
    enum UserDataColumn
    {
        ID,
        NAME,
        HASCHARAFLAG,
        NUM
    };

    public const string ID_SAVE_KEY = "AL_2023_9GATSU_USER_ID";

    void Awake()
    {
        m_userId = "1";
        CallOnGameStart(() => RefreshUserDataUI(), () => CallError());
    }

    public void CallError()
    {
        Debug.Assert(false);
    }

    public void CallOnGameStart(UnityAction callbackFunc = null, UnityAction errorCallbackFunc = null)
    {
        m_gameStartCallbackFunc = callbackFunc;
        m_gameStartErrorCallbackFunc = errorCallbackFunc;
        OnGameStartGetUserData_1st();
    }

    // ゲーム開始時に、データベースからユーザーデータを取得するための関数.
    // まずは、UserDataの取得.
    private void OnGameStartGetUserData_1st()
    {
        string url = "http://18.178.60.234/students/active_larning/user00/PHP/GetUserDataFromUserId.php?userId=" + m_userId;
        StartCoroutine(GetUserData(url, () => m_gameStartCallbackFunc(), () => m_gameStartErrorCallbackFunc()));
    }

    public void CallUserData(int userId)
    {
        {
            string url = "http://18.178.60.234/students/active_larning/user00/PHP/GetUserDataFromUserId.php?userId=" + userId;
            StartCoroutine(GetUserData(url, () => RefreshUserDataUI()));
        }
    }

    public void CallPlayGacha(int userId)
    {
        string url = "http://18.178.60.234/students/active_larning/user00/PHP/PlayGacha.php?userId=" + userId;

        StartCoroutine(GachaAccess(url, () => RefreshGachaPerformUI()));
    }

    // ユーザデータを取得してきて、メンバ変数に格納する.
    IEnumerator GetUserData(string url, UnityAction callbackFunc = null, UnityAction errorCallbackFunc = null)
    {
        WWWForm form = new WWWForm();
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("error:" + www.error);
                if (errorCallbackFunc != null)
                {
                    errorCallbackFunc();
                }
                yield break;
            }
            Debug.Log("text:" + www.text);

            string resultText = www.text;
            string[] resultTextArray = resultText.Split(",");
            if (resultTextArray.Length != (int)UserDataColumn.NUM)
            {
                if (errorCallbackFunc != null)
                {
                    errorCallbackFunc();
                }
                yield break;
            }

            m_userId = resultTextArray[(int)UserDataColumn.ID];
            m_name = resultTextArray[(int)UserDataColumn.NAME];
            m_hasCharaFlag = resultTextArray[(int)UserDataColumn.HASCHARAFLAG];
            m_isUserDataLoadComplete = true;

            if (callbackFunc != null)
            {
                callbackFunc();
            }
        }
    }

    IEnumerator GachaAccess(string url, UnityAction callbackFunc = null, UnityAction errorCallbackFunc = null)
    {
        WWWForm form = new WWWForm();
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("error:" + www.error);
                yield break;
            }
            Debug.Log("text:" + www.text);

            m_currentGachaResult = www.text;

            if (callbackFunc != null)
            {
                callbackFunc();
            }
        }
    }

    // 単純に当該のURLにアクセスする.
    IEnumerator UrlAccess(string url, UnityAction callbackFunc = null)
    {
        WWWForm form = new WWWForm();
        using (WWW www = new WWW(url, form))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("error:" + www.error);
                yield break;
            }
            Debug.Log("text:" + www.text);

            if (callbackFunc != null)
            {
                callbackFunc();
            }
        }
    }

    private void RefreshUserDataUI()
    {
        Debug.Log(m_userId + "," + m_name + "," + m_hasCharaFlag);
    }

    private void RefreshGachaPerformUI()
    {
        GameObject obj = GameObject.Find("GachaManager");
        if (obj != null)
        {
            P3_GachaManager gachaManager = obj.GetComponent<P3_GachaManager>();
            gachaManager.DrawPerform(m_currentGachaResult);
        }

        RefreshUserDataUI();
    }

    public string GetCurrentUserId()
    {
        return m_userId;
    }

    public string GetUserName()
    {
        return m_name;
    }

    public string GetCurrentHasCharaFlag()
    {
        return m_hasCharaFlag;
    }

    public string GetCurrentGachaResult()
    {
        return m_currentGachaResult;
    }

    // UserDataManagerクラスのメンバ変数の宣言.
    string m_userId = "0";
    string m_name = "";
    string m_hasCharaFlag = "0";
    string m_currentGachaResult = "";
    bool m_isUserDataLoadComplete = false;
    UnityAction m_gameStartCallbackFunc = null;
    UnityAction m_gameStartErrorCallbackFunc = null;

}
