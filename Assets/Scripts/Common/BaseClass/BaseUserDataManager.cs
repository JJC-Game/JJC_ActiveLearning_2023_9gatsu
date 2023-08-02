using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUserDataManager : MonoBehaviour
{
    public string userName = "PLAYER";
    protected bool[] hasChara;
    protected int[] currentGachaResult_CharaId;

    // Start is called before the first frame update
    protected void Awake()
    {
        hasChara = new bool[DefineParam.CHARA_NUM];
        for (int i = 0; i < DefineParam.CHARA_NUM; i++)
        {
            hasChara[i] = false;
        }
        currentGachaResult_CharaId = new int[DefineParam.GACHA_GET_CHARA_MAX];
        for (int i = 0; i < DefineParam.GACHA_GET_CHARA_MAX; i++)
        {
            currentGachaResult_CharaId[i] = DefineParam.INVALIC_CHARA_ID;
        }

    }

    public bool GetCharaHave(int charaId)
    {
        return hasChara[charaId];
    }

    public int GetCurrentGachaResult(int resultIndex)
    {
        return currentGachaResult_CharaId[resultIndex];
    }

    public void SetCharaHave(int charaId)
    {
        hasChara[charaId] = true;
    }

    virtual public int GetUserId()
    {
        return 0;
    }

    virtual public void SetUserData(string inputText)
    {

    }
}
