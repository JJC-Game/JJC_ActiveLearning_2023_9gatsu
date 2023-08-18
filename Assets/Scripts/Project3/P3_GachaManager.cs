using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class P3_GachaManager : MonoBehaviour
{
    const int CHARACTER_NUM = 32;

    Perform_Gacha1 performGacha1;
    Perform_Gacha10 performGacha10;

    private void Awake()
    {
        performGacha1 = new Perform_Gacha1();
        performGacha10 = new Perform_Gacha10();
    }
    // Start is called before the first frame update
    void Start()
    {
        performGacha1.Init();
        performGacha10.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickGachaButton()
    {
        int userId = UserApplication.userDataManager.GetUserId();
        UserApplication.phpConnectManager.CallPlayGacha(userId);
    }

    public void OnClickGacha10Button()
    {
        int userId = UserApplication.userDataManager.GetUserId();
        UserApplication.phpConnectManager.CallPlayGacha10(userId);
    }

    public void DrawPerform(string gachaResult)
    {
        string[] gachaResultArray = gachaResult.Split(",");

        performGacha1.Activate();
        FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(int.Parse(gachaResultArray[0]));
        performGacha1.SetSprite(Resources.Load<Sprite>(fixCharaData.tachiePath));
        performGacha1.SetName(fixCharaData.name);
    }

    public void DrawPerform10(string gachaResult)
    {
        string[] gachaResultArray = gachaResult.Split(",");

        performGacha10.Activate();

        for (int i = 0; i < 10; i++)
        {
            FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(int.Parse(gachaResultArray[i]));
            performGacha10.SetSprite(Resources.Load<Sprite>(fixCharaData.tachiePath), i);
            performGacha10.SetName(fixCharaData.name, i);
        }
    }


    public void OnClickResetCharaHasFlag()
    {
        int userId = UserApplication.userDataManager.GetUserId();
        UserApplication.phpConnectManager.CallClearHasCharaFlag(userId);
    }
}
