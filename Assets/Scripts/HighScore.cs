using System.Collections.Generic;

[System.Serializable]
public class HighScore 
{
    public string playerName;
    public int score;

    public HighScore(string newName, int newScore)
    {
        this.playerName = newName;
        this.score = newScore;
    }

    public HighScore(){}

}

[System.Serializable]
public class HighScoreList 
{
    private List<HighScore> highScoresList;

    public HighScoreList(List<HighScore> l)
    {
        this.highScoresList = l;
    }


    public void AddTolist(HighScore hs)
    {
        this.highScoresList.Add(hs);
        ListReduction();
    }

    private void ListReduction()
    {
        for(int i = 0; i < highScoresList.Count - 1; i++)
        {
            for(int j = i+1;  j < highScoresList.Count; j++)
            {
                if(highScoresList[i].score < highScoresList[j].score)
                {
                    HighScore tmp = highScoresList[i];
                    highScoresList[i] = highScoresList[j];
                    highScoresList[j] = tmp; 
                }
            }
        }
        if(highScoresList.Count > 8)
        {
            highScoresList.RemoveRange(8, highScoresList.Count - 8);
        }
    }

    public List<HighScore> getHightScoreList()
    {
        return highScoresList;
    }

}
