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

    public BaseUserDataManager _userDataManager;
    static public BaseUserDataManager userDataManager
    {
        get
        {
            if (S._userDataManager == null)
            {
                S._userDataManager = GameObject.Find("UserDataManager").GetComponent<BaseUserDataManager>();
            }
            return S._userDataManager;
        }
    }

    public BaseCharaGridRenderer _charaGridRenderer;
    static public BaseCharaGridRenderer charaGridRenderer
    {
        get
        {
            if (S._charaGridRenderer == null)
            {
                S._charaGridRenderer = GameObject.Find("UIParts_CharaGridRenderer").GetComponent<BaseCharaGridRenderer>();
            }
            return S._charaGridRenderer;
        }
    }

    public BasePHPConnectManager _phpConnectManager;
    static public BasePHPConnectManager phpConnectManager
    {
        get
        {
            if (S._phpConnectManager == null)
            {
                S._phpConnectManager = GameObject.Find("PHPConnectManager").GetComponent<BasePHPConnectManager>();
            }
            return S._phpConnectManager;
        }
    }

}
