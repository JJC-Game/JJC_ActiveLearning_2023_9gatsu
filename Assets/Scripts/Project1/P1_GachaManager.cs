using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_GachaManager : MonoBehaviour
{
    [SerializeField]
    int gachaPickCharacterId;

    const int CHARACTER_NUM = 32;

    // Start is called before the first frame update
    void Start()
    {
        gachaPickCharacterId = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        gachaPickCharacterId = Random.Range(0, CHARACTER_NUM);

        Debug.Log(gachaPickCharacterId.ToString());
    }
}
