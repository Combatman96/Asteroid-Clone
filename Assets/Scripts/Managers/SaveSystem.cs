using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem 
{
    public static void InitHighScore(List<HighScore> highScoresList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.dat";
    
        HighScoreList datalist = new HighScoreList(highScoresList);

        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, datalist);
        stream.Close();
    }
   public static void SaveHighScore(string name, int score)
   {
        HighScore data = new HighScore(name, score);  
        HighScoreList dataList = LoadHighScoreList();
        dataList.AddTolist(data);       

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.dat";    

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(stream, dataList);
        stream.Close();
   }

   public static HighScoreList LoadHighScoreList()
   {
        string path = Application.persistentDataPath + "/highscore.dat";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            HighScoreList datalist = formatter.Deserialize(stream) as HighScoreList;
            stream.Close();
            return datalist;
        }
        else
        {
            Debug.Log("ERROR loading hightscore in " + path);
            return null;
        }
   }
}
