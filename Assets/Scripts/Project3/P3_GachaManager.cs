using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P3_GachaManager : MonoBehaviour
{
    const int CHARACTER_NUM = 32;

    GameObject gachaPerformInstance;
    Image performImage;
    Text performName;
    Image performBack;

    // Start is called before the first frame update
    void Start()
    {
        gachaPerformInstance = GameObject.Find("UIParts_Perform_Project3");
        performImage = gachaPerformInstance.transform.Find("Image").GetComponent<Image>();
        performName = gachaPerformInstance.transform.Find("Name").GetComponent<Text>();
        performBack = gachaPerformInstance.transform.Find("Back").GetComponent<Image>();
        gachaPerformInstance.SetActive(false);
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

    public void DrawPerform(string gachaResult)
    {
        string[] gachaResultArray = gachaResult.Split(",");

        gachaPerformInstance.SetActive(true);
        FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(int.Parse(gachaResultArray[0]));
        performImage.sprite = Resources.Load<Sprite>(fixCharaData.tachiePath);
        performName.text = fixCharaData.name;
    }

    public void OnClickGachaPerformBack()
    {
        gachaPerformInstance.SetActive(false);
    }

    public void OnClickResetCharaHasFlag()
    {
        int userId = UserApplication.userDataManager.GetUserId();
        UserApplication.phpConnectManager.CallClearHasCharaFlag(userId);
    }
}
