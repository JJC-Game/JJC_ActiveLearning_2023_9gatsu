using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaCell : MonoBehaviour
{
    void Awake(){
        nameText = this.transform.Find("CharaName").GetComponent<Text>();
        charaImage = this.transform.Find("CharaImage").GetComponent<Image>();
        notHasCover = this.transform.Find("NotHasCover").GetComponent<Image>();
    }

    public void RefreshCharaImage(int charaId, bool isNotHave){
        if (UserApplication.fixCharaManager == null)
        {
            return;
        }
        if (UserApplication.fixCharaManager.GetFixCharaData(charaId) == null)
        {
            return;
        }
        nameText.text = UserApplication.fixCharaManager.GetFixCharaData(charaId).name;
        nameText.color = new Color(0, 0, 255);
        charaImage.sprite = Resources.Load<Sprite>(UserApplication.fixCharaManager.GetFixCharaData(charaId).tachiePath);
        notHasCover.enabled = isNotHave;
    }

    public void HideCharaImage(){
        nameText.text = "";
        charaImage.sprite = Resources.Load<Sprite>("Textures/Chara/NoChara");
        notHasCover.enabled = false;

    }

    Text nameText;
    Image charaImage;
    Image notHasCover;
}
