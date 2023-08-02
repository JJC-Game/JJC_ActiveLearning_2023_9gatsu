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
        string phpFileName = "GetUserDataFromUserId.php?userId=" + userId.ToString();
        CallPHPConnection(phpFileName, () => RefreshUserDataUI());
    }
    private void RefreshUserDataUI()
    {
        UserApplication.userDataManager.SetUserData(phpConnectResultText);
    }

    override public void CallPlayGacha(int userId)
    {
        calledUserId = userId;
        string phpFileName = "PlayGacha.php?userId=" + userId.ToString();
        CallPHPConnection(phpFileName, () => RefreshGachaPerformUI());
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

    override public void CallReserCharaHasFlag(int userId)
    {
        calledUserId = userId;
        string phpFileName = "ResetHasCharaFlag.php?userId=" + userId.ToString();
        CallPHPConnection(phpFileName, () => CallbackReserHasCharaFlag());
    }

    private void CallbackReserHasCharaFlag()
    {
        CallUserData(calledUserId);
    }

}
