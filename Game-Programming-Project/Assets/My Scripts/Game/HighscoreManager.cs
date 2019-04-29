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

    private readonly float maxHighscores = 6;
    private float rank;

    private void Awake()
    {
        //ClearHighscore(level.ToString(), true);
        //CreateTestHighscore();

        if (scorePrefab != null) HighscoreSetup();
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
        //Sorterar listan med högsta ranken först
        for (int i = 0; i < highscores.Count; i++)
        {
            for (int j = i + 1; j < highscores.Count; j++)
            {
                if (highscores[j].score < highscores[i].score)
                {
                    Highscore tmp = highscores[i];
                    highscores[i] = highscores[j];
                    highscores[j] = tmp;
                }
            }
        }

        //Tar bort eventuella överblivna rekord
        if (highscores.Count > maxHighscores)
        {
            for (int i = (highscores.Count - 1);  i >= maxHighscores; i--)
            {
                highscores.RemoveAt(i);
            }
        }
    }

    protected void SaveNewHighscore(float score, string name, string level)
    {
        Highscore highscore = new Highscore { score = score, name = name };
        string jSonString = PlayerPrefs.GetString(level);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jSonString);
        if (highscores == null) highscores = new Highscores(); //ifall objektet är null så får man error

        highscores.highscoreList.Add(highscore);

        SortHighscoreList(highscores.highscoreList); //Sorterar och tar bort eventuella överblivna rekord.

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(level, json);
        PlayerPrefs.Save();
    }

    private void CreateHighscoreEntry(Highscore highScoreEntry)
    {
        rank++;

        Transform current = Instantiate(scorePrefab, container);
        Color color = rank % 2 == 0 ? new Color(0, 0, 0, 0.2f) : new Color(255, 255, 255, 0.2f);
        current.GetComponent<Image>().color = color;
        current.Find("Rank Text").GetComponent<TextMeshProUGUI>().text = rank.ToString();
        current.Find("Score Text").GetComponent<TextMeshProUGUI>().text = highScoreEntry.score.ToString();
        current.Find("Name Text").GetComponent<TextMeshProUGUI>().text = highScoreEntry.name;
    }

    private void ClearHighscore(string key, bool deleteAll = false)
    {
        if (deleteAll) PlayerPrefs.DeleteAll();
        else           PlayerPrefs.DeleteKey(key);
    }

    private void CreateTestHighscore()
    {
        SaveNewHighscore(10, "AAA", level.ToString());
        SaveNewHighscore(25, "BBB", level.ToString());
        SaveNewHighscore(100, "CCC", level.ToString());
        SaveNewHighscore(708, "DDD", level.ToString());
        SaveNewHighscore(747, "EEE", level.ToString());
        SaveNewHighscore(13, "FFF", level.ToString());
        SaveNewHighscore(10000, "GGG", level.ToString());
        SaveNewHighscore(65, "HHH", level.ToString());
        SaveNewHighscore(8999, "III", level.ToString());
        SaveNewHighscore(2, "JJJ", level.ToString());
    }
}


public class Highscores
{
    public List<Highscore> highscoreList = new List<Highscore>();
}

[System.Serializable]
public struct Highscore
{
    public float score;
    public string name;
}