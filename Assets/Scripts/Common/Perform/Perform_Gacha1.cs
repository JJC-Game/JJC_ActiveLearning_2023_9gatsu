using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Perform_Gacha1
{
    private GameObject instance;
    private Image image;
    private Text name;
    private Image back;

    public void Init()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Perform/UIParts_Perform_Gacha_1");

        instance = GameObject.Instantiate(prefab);
        RectTransform rectTrans = instance.transform as RectTransform;
        instance.transform.SetParent(GameObject.Find("UIParts_Perform_Anchor").transform);
        rectTrans.anchoredPosition = new Vector2(0, 0);

        image = instance.transform.Find("Image").GetComponent<Image>();
        name = instance.transform.Find("Name").GetComponent<Text>();
        back = instance.transform.Find("Back").GetComponent<Image>();


        // イベントトリガーの設定.
        EventTrigger trigger = back.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => { Deactivate(); });
        trigger.triggers.Add(entry);

        SetActive(false);
    }

    public void SetActive(bool active)
    {
        instance.SetActive(active);
    }

    public void Activate()
    {
        SetActive(true);
    }

    public void Deactivate()
    {
        SetActive(false);
    }

    public void SetName(string inputName)
    {
        name.text = inputName;
    }

    public void SetSprite(Sprite inputSprite)
    {
        image.sprite = inputSprite;
    }
}
