using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FixCharaManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Load();
        DB_Disp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        string path = "FixData/CharaFixData";

        TextAsset csvFile;
        m_fixCharaDataList = new List<FixCharaData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                FixCharaData currentRow = new FixCharaData();
                currentRow.m_id = int.Parse(lineArray[0]);
                currentRow.m_name = lineArray[1];
                currentRow.m_tachiePath = lineArray[2];
                currentRow.m_gridPath = lineArray[3];

                m_fixCharaDataList.Add(currentRow);
            }
        }
    }

    public FixCharaData GetFixCharaData(int charaId)
    {
        if (charaId >= GetFixCharaNum())
        {
            return null;
        }
        return m_fixCharaDataList[charaId];
    }

    public int GetFixCharaNum()
    {
        return m_fixCharaDataList.Count;
    }

    public int GetCharaIdFromCharaName(string charaName)
    {
        for (int i = 0; i < GetFixCharaNum(); i++)
        {
            FixCharaData data = GetFixCharaData(i);
            if (data.m_name.Equals(charaName))
            {
                return i;
            }
        }
        return INVALID_ID;
    }

    public bool IsValidCharaId(int charaId)
    {
        return 0 <= charaId && charaId < GetFixCharaNum();
    }

    public void DB_Disp()
    {
        for (int i = 0; i < GetFixCharaNum(); i++)
        {
            FixCharaData data = GetFixCharaData(i);
            Debug.Log(data.m_id + "," + data.m_name + "," + data.m_tachiePath + "," + data.m_gridPath);
        }
    }

    public class FixCharaData
    {
        public int m_id;
        public string m_name;
        public string m_tachiePath;
        public string m_gridPath;
    }

    List<FixCharaData> m_fixCharaDataList;
    public const int INVALID_ID = -1;
}
