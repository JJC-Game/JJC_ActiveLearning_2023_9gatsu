using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class P3_UserDataManager : BaseUserDataManager
{
    public enum UserDataColumn
    {
        ID,
        NAME,
        HASCHARAFLAG,
        NUM
    };

    int userId = 0;

    new protected void Awake()
    {
        base.Awake();
        userId = 1;
    }

    void Start()
    {
        UserApplication.phpConnectManager.CallUserData(userId);
    }

    public override int GetUserId()
    {
        return userId;
    }

    public override void SetUserData(string inputText)
    {
        string[] textArray = inputText.Split(",");
        if (textArray.Length != (int)UserDataColumn.NUM)
        {
            Debug.LogAssertion("not match UserData");
            return;
        }

        int inputTextUserId = int.Parse(textArray[(int)UserDataColumn.ID]);
        if (userId != inputTextUserId)
        {
            Debug.LogAssertion("取得したデータのユーザIDが、このアカウントのユーザIDと異なっています");
            return;
        }

        userName = textArray[(int)UserDataColumn.NAME];
        string inputTextHasCharaFlag = textArray[(int)UserDataColumn.HASCHARAFLAG];
        uint hasCharaFlag = uint.Parse(inputTextHasCharaFlag);

        for (int charaId = 0; charaId < DefineParam.CHARA_NUM; charaId++)
        {
            // 所持フラグを元にして、所持しているかどうかを確認する.
            bool isNotHaveChara = false;
            // シフト演算子を使って、二進数で確認する.
            // 当該のビットの値のANDの結果が0なら、フラグが立っていない = 所持していない.
            // 当該のビットの値のANDの結果が0ではないなら、フラグが立っている = 所持している.
            hasChara[charaId] = ((hasCharaFlag & (1 << charaId)) != 0);
        }

        UserApplication.charaGridRenderer.RefreshGrid();
    }
}
