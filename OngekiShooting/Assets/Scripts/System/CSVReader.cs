﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class CSVReader
{
    public static List<EnemySpawnData> ReadEnemySpawnData(string fileName, float y)
    {
        List<EnemySpawnData> data = new List<EnemySpawnData>();

        string filePath = Application.streamingAssetsPath+"/" + fileName + ".csv";
        using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open)))
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();

                var values = line.Split(',');

                float spawntiming = float.Parse(values[0]);
                float x = float.Parse(values[1]);
                float z = float.Parse(values[2]);
                float speed = float.Parse(values[3]);
                EnemyType enemyType = (EnemyType)Enum.Parse(typeof(EnemyType), values[4]);
                MoveType moveType = (MoveType)Enum.Parse(typeof(MoveType), values[5]);

                data.Add(new EnemySpawnData(spawntiming, new Vector3(x, y, z), speed, enemyType, moveType));
            }
        }
        return data;
    }
}
