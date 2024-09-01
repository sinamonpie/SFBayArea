using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Contents
{
    public string Title;
    public string Description;
}

[Serializable]
public class Language
{
    public Contents KorText;
    public Contents EngText;
}

[Serializable]
public class Data
{
    public string id;
    public Language contents;
    public string location;
}

[Serializable]
public class DataList : Loader<string, Data>
{
    public List<Data> data = new List<Data>();

    public Dictionary<string, Data> MakeDict()
    {
        Dictionary<string, Data> dict = new Dictionary<string, Data>();
        foreach(Data d in data)
        {
            if(!dict.ContainsValue(d))
            {
                dict.Add(d.id, d);
            }
        }
        return dict;
    }
}

public interface Loader<Key, Value>
{
    public Dictionary<Key, Value> MakeDict();
}

public class DescriptionManager : MonoBehaviour
{
    public static DescriptionManager instance = null;

    public TMP_Text titleTxt;
    public TMP_Text detailTxt;
    public TMP_Text locationTxt;

    [SerializeField]
    public Dictionary<string, Data> dataDict = new Dictionary<string, Data>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Json������ �Ľ�
        dataDict = LoadJson<DataList, string, Data>("3DModel").MakeDict();

        //ID ���� Resource ������ json ���� Ȯ���Ͻø� �ǰ� �ʿ��Ͻø� json���Ͽ��� ID ���� ���ϽŴ�� �����ϼż� ���ŵ� �˴ϴ�.
        //ID���� �ϴ� ������� �����ϰ� �־�����ϴ�.
        //���� �����Ͻø� �ɰ� �����ϴ�.
        //SetDescription("CableCar");
    }

    Load LoadJson<Load, Key, Value>(string path) where Load : Loader<Key, Value>
    {
        TextAsset textData = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Load>(textData.text);
    }

    public void SetDescription(string id)
    {
        if(GameManager.instance.laguageType == LaguageType.Korea)
        {
            titleTxt.SetText(dataDict[id].contents.KorText.Title);
            detailTxt.SetText(dataDict[id].contents.KorText.Description);
        }
        else
        {
            titleTxt.SetText(dataDict[id].contents.EngText.Title);
            detailTxt.SetText(dataDict[id].contents.EngText.Description);
        }
        locationTxt.SetText(dataDict[id].location);
    }
}
