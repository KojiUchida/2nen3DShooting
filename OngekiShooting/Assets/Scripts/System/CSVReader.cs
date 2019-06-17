using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static  class CSVReader
{
    public static List<EnemySpawnData> Read(string fileName)
    {
        List<EnemySpawnData> data = new List<EnemySpawnData>();

        string filePath = Application.streamingAssetsPath + fileName + ".csv";
        using (StreamReader stream = new StreamReader(File.Open(filePath, FileMode.Open)))
        {

        }
        return data;
    }
}
