using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExportableBallList: MonoBehaviour
{
    public static ExportableBallList instance;
    public List<masterBallStruct> holdingList = new List<masterBallStruct>();
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

        public void addBallsToExporterlist(masterBallStruct ball)
    {
        holdingList.Add(ball);
    }

    public void clearTheExportList()
    {
        holdingList.Clear();
        Destroy(gameObject);
    }
}
