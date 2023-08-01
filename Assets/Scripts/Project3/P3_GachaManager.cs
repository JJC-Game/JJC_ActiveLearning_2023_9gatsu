﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P3_GachaManager : MonoBehaviour
{
    [SerializeField]
    int gachaPickCharacterId;

    const int CHARACTER_NUM = 32;

    GameObject gachaPerformInstance;
    Image performImage;
    Text performName;
    Image performBack;

    // Start is called before the first frame update
    void Start()
    {
        gachaPickCharacterId = 0;
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
        GameObject obj = GameObject.Find("UserDataManager");
        if (obj != null)
        {
            P3_UserDataManager userDataManager = obj.GetComponent<P3_UserDataManager>();
            userDataManager.CallPlayGacha(int.Parse(userDataManager.GetCurrentUserId()));
        }
    }

    public void DrawPerform(string gachaResult)
    {
        string[] gachaResultArray = gachaResult.Split(",");

        gachaPerformInstance.SetActive(true);
        FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(int.Parse(gachaResultArray[0]));
        performImage.sprite = Resources.Load<Sprite>(fixCharaData.m_tachiePath);
        performName.text = fixCharaData.m_name;
    }

    public void OnClickGachaPerformBack()
    {
        gachaPerformInstance.SetActive(false);
    }
}