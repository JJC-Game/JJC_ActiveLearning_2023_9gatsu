using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static P3_UserDataManager;

public class P3_PHPConnectManager : BasePHPConnectManager
{
    public const string ID_SAVE_KEY = "AL_2023_9GATSU_USER_ID";

    override public void CallUserData(int userId)
    {
        calledUserId = userId;
        string phpFileName = "app_user/" + userId.ToString();
        CallPHPConnection(phpFileName, () => RefreshUserDataUI());
    }
    private void RefreshUserDataUI()
    {
        UserApplication.userDataManager.SetUserData(phpConnectResultText);
    }

    override public void CallPlayGacha(int userId)
    {
        calledUserId = userId;
        string phpFileName = "play_chara_gacha/" + userId.ToString();
        CallPHPConnection(phpFileName, () => RefreshGachaPerformUI());
    }
    override public void CallPlayGacha10(int userId)
    {
        calledUserId = userId;
        string phpFileName = "play_chara_gacha_10/" + userId.ToString();
        CallPHPConnection(phpFileName, () => RefreshGacha10PerformUI());
    }
    private void RefreshGachaPerformUI()
    {
        GameObject obj = GameObject.Find("GachaManager");
        if (obj != null)
        {
            P3_GachaManager gachaManager = obj.GetComponent<P3_GachaManager>();
            gachaManager.DrawPerform(phpConnectResultText);
        }
        CallUserData(calledUserId);
    }

    private void RefreshGacha10PerformUI()
    {
        GameObject obj = GameObject.Find("GachaManager");
        if (obj != null)
        {
            P3_GachaManager gachaManager = obj.GetComponent<P3_GachaManager>();
            gachaManager.DrawPerform10(phpConnectResultText);
        }
        CallUserData(calledUserId);
    }

    override public void CallClearHasCharaFlag(int userId)
    {
        calledUserId = userId;
        string phpFileName = "clear_has_chara_flag/" + userId.ToString();
        CallPHPConnection(phpFileName, () => CallbackClearHasCharaFlag());
    }

    private void CallbackClearHasCharaFlag()
    {
        CallUserData(calledUserId);
    }

}
