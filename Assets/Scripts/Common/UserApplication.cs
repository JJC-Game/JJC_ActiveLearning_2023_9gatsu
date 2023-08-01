using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserApplication : MonoBehaviour
{
    private void Awake()
    {
        S = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //あ.
    }

    static private UserApplication _S;
    static private UserApplication S
    {
        get
        {
            if (_S == null)
            {
                return null;
            }
            return _S;
        }
        set
        {
            if (_S != null)
            {
                Debug.LogError("_Sは既に設定されています.");
            }
            _S = value;
        }
    }

    [Header("UserApplicationからアクセスできるコンポーネント")]
    public FixCharaManager _fixCharaManager;
    static public FixCharaManager fixCharaManager
    {
        get
        {
            if (S._fixCharaManager == null)
            {
                S._fixCharaManager = GameObject.Find("Common").GetComponent<FixCharaManager>();
            }
            return S._fixCharaManager;
        }
    }
}
