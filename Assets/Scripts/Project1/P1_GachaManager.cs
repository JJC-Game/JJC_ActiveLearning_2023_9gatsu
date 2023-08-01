using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1_GachaManager : MonoBehaviour
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
        gachaPerformInstance = GameObject.Find("UIParts_Perform_Project1");
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
        gachaPickCharacterId = Random.Range(0, CHARACTER_NUM);

        Debug.Log(gachaPickCharacterId.ToString());

        DrawPerform();
    }

    public void DrawPerform()
    {
        gachaPerformInstance.SetActive(true);
        FixCharaManager.FixCharaData fixCharaData = UserApplication.fixCharaManager.GetFixCharaData(gachaPickCharacterId);
        performImage.sprite = Resources.Load<Sprite>(fixCharaData.m_tachiePath);
        performName.text = fixCharaData.m_name;
    }

    public void OnClickGachaPerformBack()
    {
        gachaPerformInstance.SetActive(false);
    }
}
