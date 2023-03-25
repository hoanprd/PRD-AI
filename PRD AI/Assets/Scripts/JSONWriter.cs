using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JSONWriter : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string api_key;
        public string organization;
    }

    public Data myData = new Data();

    void Start()
    {
        WriteData();
    }

    public void WriteData()
    {
        string strOutput = JsonUtility.ToJson(myData);
        string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        File.WriteAllText(userPath + "/auth.json", strOutput);
    }
}
