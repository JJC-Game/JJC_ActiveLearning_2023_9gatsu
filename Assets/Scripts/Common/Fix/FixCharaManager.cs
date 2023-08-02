using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FixCharaManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
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
        fixCharaDataList = new List<FixCharaData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                FixCharaData currentRow = new FixCharaData();
                currentRow.id = int.Parse(lineArray[0]);
                currentRow.name = lineArray[1];
                currentRow.tachiePath = lineArray[2];
                currentRow.gridPath = lineArray[3];

                fixCharaDataList.Add(currentRow);
            }
        }
    }

    public FixCharaData GetFixCharaData(int charaId)
    {
        if (charaId >= GetFixCharaNum())
        {
            return null;
        }
        return fixCharaDataList[charaId];
    }

    public int GetFixCharaNum()
    {
        return fixCharaDataList.Count;
    }

    public int GetCharaIdFromCharaName(string charaName)
    {
        for (int i = 0; i < GetFixCharaNum(); i++)
        {
            FixCharaData data = GetFixCharaData(i);
            if (data.name.Equals(charaName))
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
            Debug.Log(data.id + "," + data.name + "," + data.tachiePath + "," + data.gridPath);
        }
    }

    public class FixCharaData
    {
        public int id;
        public string name;
        public string tachiePath;
        public string gridPath;
    }

    List<FixCharaData> fixCharaDataList;
    public const int INVALID_ID = -1;
}
