using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Level { Level1, Level2 };

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private Transform container;
    [SerializeField] private Transform scorePrefab;

    private readonly float maxHighscores = 10;
    private float rank;

    private void Awake()
    {
        ClearHighscore(level.ToString(), true);
        //SaveNewHighscore(747, "Daniel");

        //for (int i = 0; i < 20; i++) SaveNewHighscore(747, "Daniel");

        HighscoreSetup();
    }

    private void HighscoreSetup()
    {
        string jSonString = PlayerPrefs.GetString(level.ToString());

        if (jSonString.Length > 0)
        {
            List<Highscore> highscores = JsonUtility.FromJson<Highscores>(jSonString).highscoreList;

            SortHighscoreList(highscores);
            foreach (Highscore highscoreEntry in highscores)
            {
                CreateHighscoreEntry(highscoreEntry);
            }
        }
    }

    private void SortHighscoreList(List<Highscore> highscores)
    {
        //Sorterar listan med högsta scoret först
        for (int i = 0; i < highscores.Count; i++)
        {
            for (int j = i + 1; j < highscores.Count; j++)
            {
                if (highscores[j].score > highscores[i].score)
                {
                    Highscore tmp = highscores[i];
                    highscores[i] = highscores[j];
                    highscores[j] = tmp;
                }
            }
        }
        
        //Kollar ifall listan innehåller mer highscores än vad som kan visas och tar då bort dessa.
        if (highscores.Count > maxHighscores)
        {
            for (int i = (highscores.Count - 1);  i >= maxHighscores; i--)
            {
                highscores.RemoveAt(i);
            }
        }
    }

    private void SaveNewHighscore(int score, string name)
    {
        Highscore highscore = new Highscore { score = score, name = name };
        string jSonString = PlayerPrefs.GetString(level.ToString());
        Highscores highscores = JsonUtility.FromJson<Highscores>(jSonString);
        if (highscores == null) highscores = new Highscores(); //ifall objektet är null så får man error

        highscores.highscoreList.Add(highscore);

        SortHighscoreList(highscores.highscoreList); //Sorterar och tar bort eventuella överblivna rekord.

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(level.ToString(), json);
        PlayerPrefs.Save();
    }

    private void CreateHighscoreEntry(Highscore highScoreEntry)
    {
        rank++;

        Transform current = Instantiate(scorePrefab, container);
        Color color = rank % 2 == 0 ? new Color(0, 0, 0, 0.2f) : new Color(255, 255, 255, 0.2f);
        current.Find("Rank Text").GetComponent<TextMeshProUGUI>().text = rank.ToString();
        current.Find("Score Text").GetComponent<TextMeshProUGUI>().text = highScoreEntry.score.ToString();
        current.Find("Name Text").GetComponent<TextMeshProUGUI>().text = highScoreEntry.name;
        current.Find("Background").GetComponent<Image>().color = color;
    }

    private void ClearHighscore(string key, bool deleteAll = false)
    {
        PlayerPrefs.DeleteKey(key);
        if (deleteAll) PlayerPrefs.DeleteAll();
    }
}


public class Highscores
{
    public List<Highscore> highscoreList = new List<Highscore>();
}


[System.Serializable]
public class Highscore
{
    public int score;
    public string name;
}