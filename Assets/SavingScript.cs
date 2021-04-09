using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavingScript : MonoBehaviour
{
    public string saveName;
    public bool report;
    [System.Serializable] public struct SavedLists 
    {
        public List<savedInt> savedInts;
        public List<savedFloat> savedFloats;
        public List<savedBool> savedBools;
        public List<savedString> savedStrings;
    }

    [System.Serializable] public struct savedInt
    {
        public string iD;
        public int value;
    }
    [System.Serializable] public struct savedFloat
    {
        public string iD;
        public float value;
    }
    [System.Serializable] public struct savedBool
    {
        public string iD;
        public bool value;
    }
    [System.Serializable] public struct savedString
    {
        public string iD;
        public string value;
    }

    public List<savedInt> intList;
    public List<savedFloat> floatList;
    public List<savedBool> boolList;
    public List<savedString> stringList;


    public void SaveInt(int value, string varName)
    {
        savedInt temp;
        bool found = false;
        if (intList.Count > 0)
        {
            for (int i = 0; i < intList.Count; i++)
            {
                var checkSaved = intList[i];
                if(checkSaved.iD == varName)
                {
                    temp = checkSaved;
                    temp.value = value;
                    intList[i] = temp;
                    found = true;
                }
            }
        }
        if (!found)
        {
            savedInt myVar = new savedInt();
            myVar.iD = varName;
            myVar.value = value;
            intList.Add(myVar);
        }
    }
    public void SaveFloat(float value, string varName)
    {
        savedFloat temp;
        bool found = false;
        if (floatList.Count > 0)
        {
            for (int i = 0; i < floatList.Count; i++)
            {
                var checkSaved = floatList[i];
                if (checkSaved.iD == varName)
                {
                    temp = checkSaved;
                    temp.value = value;
                    floatList[i] = temp;
                    found = true;
                }
            }
        }
        if (!found)
        {
            savedFloat myVar = new savedFloat();
            myVar.iD = varName;
            myVar.value = value;
            floatList.Add(myVar);
        }
    }
    public void SaveBool(bool value, string varName)
    {
        savedBool temp;
        bool found = false;
        if (intList.Count > 0)
        {
            for (int i = 0; i < intList.Count; i++)
            {
                var checkSaved = boolList[i];
                if (checkSaved.iD == varName)
                {
                    temp = checkSaved;
                    temp.value = value;
                    boolList[i] = temp;
                    found = true;
                }
            }
        }
        if (!found)
        {
            savedBool myVar = new savedBool();
            myVar.iD = varName;
            myVar.value = value;
            boolList.Add(myVar);
        }
    }
    public void SaveString(string value, string varName)
    {
        savedString temp;
        bool found = false;
        if (stringList.Count > 0)
        {
            for (int i = 0; i < stringList.Count; i++)
            {
                var checkSaved = stringList[i];
                if (checkSaved.iD == varName)
                {
                    temp = checkSaved;
                    temp.value = value;
                    stringList[i] = temp;
                    found = true;
                }
            }
        }
        if (!found)
        {
            savedString myVar = new savedString();
            myVar.iD = varName;
            myVar.value = value;
            stringList.Add(myVar);
        }
    }



    public int loadInt(string varName, int defaultValue)
    {
        int returnedInt = defaultValue;
        if(intList.Count > 0)
        {
            foreach(savedInt loadedInt in intList)
            {
                if(loadedInt.iD == varName)
                {
                    returnedInt = loadedInt.value;
                    //Debugging
                }
            }
        }
        return returnedInt;
    }
    public float loadFloat(string varName, float defaultValue)
    {
        float returnedFloat = defaultValue;
        if (floatList.Count > 0)
        {
            foreach (savedFloat loadedFloat in floatList)
            {
                if (loadedFloat.iD == varName)
                {
                    returnedFloat = loadedFloat.value;
                    //Debugging
                }
            }
        }
        return returnedFloat;
    }
    public bool loadBool(string varName, bool defaultValue)
    {
        bool returnedBool = defaultValue;
        if (boolList.Count > 0)
        {
            foreach (savedBool loadedBool in boolList)
            {
                if (loadedBool.iD == varName)
                {
                    returnedBool = loadedBool.value;
                    //Debugging
                }
            }
        }
        return returnedBool;
    }
    public string loadString(string varName, string defaultValue)
    {
        string returnedString = defaultValue;
        if (stringList.Count > 0)
        {
            foreach (savedString loadedString in stringList)
            {
                if (loadedString.iD == varName)
                {
                    returnedString = loadedString.value;
                    //Debugging
                }
            }
        }
        return returnedString;
    }


    public SavedLists CrunchToSaveList()
    {
        return new SavedLists()
        {
            savedInts = intList,
            savedFloats = floatList,
            savedBools = boolList,
            savedStrings = stringList
        };
    }
    public void LoadFromList(SavedLists list)
    {
        intList = list.savedInts;
        floatList = list.savedFloats;
        boolList = list.savedBools;
        stringList = list.savedStrings;
    }

    public void SaveToFile()
    {
        using (StreamWriter file = File.CreateText(Application.persistentDataPath + saveName + ".rbbrawl"))
        {
            file.Write(JsonUtility.ToJson(CrunchToSaveList(), true));
        }
    }

    public void LoadFromFile()
    {
        if (File.Exists(Application.persistentDataPath + saveName + ".rbbrawl"))
        {
            using (StreamReader file = File.OpenText(Application.persistentDataPath + saveName + ".rbbrawl"))
            {
                var loadedData = JsonUtility.FromJson<SavingScript.SavedLists>(file.ReadToEnd());
                LoadFromList(loadedData);
            }
        }
    }

    public void DeleteSaveFile()
    {
        if(File.Exists(Application.persistentDataPath + saveName + ".rbbrawl"))
        {
            File.Delete(Application.persistentDataPath + saveName + ".rbbrawl");
        }
    }
}
