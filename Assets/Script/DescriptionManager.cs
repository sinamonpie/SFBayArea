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

        //Json데이터 파싱
        dataDict = LoadJson<DataList, string, Data>("3DModel").MakeDict();

        //ID 값은 Resource 폴더에 json 파일 확인하시면 되고 필요하시면 json파일에서 ID 값은 편하신대로 수정하셔서 쓰셔도 됩니다.
        //ID값은 일단 영문명과 동일하게 넣어놨습니다.
        //글자 세팅하시면 될거 같습니다.
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
