using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class P2_GachaManager : MonoBehaviour
{
    [SerializeField]
    int gachaPickCharacterId;

    GameObject gachaPerformInstance;
    Image performImage;
    Text performName;
    Image performBack;

    // Start is called before the first frame update
    void Start()
    {
        gachaPickCharacterId = 0;
        gachaPerformInstance = GameObject.Find("UIParts_Perform_Project2");
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
        gachaPickCharacterId = Random.Range(0, DefineParam.CHARA_NUM);

        Debug.Log(gachaPickCharacterId.ToString());

        DrawPerform();

        UserApplication.userDataManager.SetCharaHave(gachaPickCharacterId);
        UserApplication.charaGridRenderer.RefreshGrid();
    }

    public void DrawPerform()
    {
        gachaPerformInstance.SetActive(true);
        FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(gachaPickCharacterId);
        performImage.sprite = Resources.Load<Sprite>(fixCharaData.tachiePath);
        performName.text = fixCharaData.name;
    }

    public void OnClickGachaPerformBack()
    {
        gachaPerformInstance.SetActive(false);
    }
}
