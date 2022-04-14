using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;
    [SerializeField] float templateHeight = 20f;

    private List<HighScore> highScoresEntryList;
    private List<Transform> highScoreEntryTransformList;

    private void Awake() 
    {
        entryTemplate.gameObject.SetActive(false);

        if(SaveSystem.LoadHighScoreList() == null)
        {
            List<HighScore> t = new List<HighScore>{
                new HighScore("Tommy", 21),
                new HighScore("James", 32),
                new HighScore("Lucas", 18)
            };
            SaveSystem.InitHighScore(t);
        }
        
        if(SaveSystem.LoadHighScoreList() != null)
        {
            highScoresEntryList = SaveSystem.LoadHighScoreList().getHightScoreList();

            

            highScoreEntryTransformList = new List<Transform>();
            foreach(HighScore  highScoreEntry in highScoresEntryList)
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
            }
        }

    }


    private void CreateHighScoreEntryTransform(HighScore highScoreEntry, Transform container, List<Transform> transformsList)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0 , -templateHeight * transformsList.Count);
        entryTransform.gameObject.SetActive(true);

        TextMeshProUGUI posText =  entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreText =  entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nameText =  entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>();
            
        int rank = transformsList.Count + 1;
        posText.text = "" + rank;
        scoreText.text = "" + highScoreEntry.score;
        nameText.text = highScoreEntry.playerName;

        transformsList.Add(entryTransform);
        
    }
}
