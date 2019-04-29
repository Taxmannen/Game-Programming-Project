using UnityEngine;

public static class MyUtils
{
    public static GameObject[] gameObjects;

    public static string RemoveSpaceFromString(string str)
    {
        str = str.Replace(" ", "");
        return str;
    }

    public static float RemoveDecimals(float number, int amountOfDecimals)
    {
        float newNumber = float.Parse(number.ToString("f" + amountOfDecimals.ToString()));
        return newNumber;
    }

    public static void PauseGame(bool state)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in gameObjects)
        {
            MonoBehaviour[] scripts = g.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour m in scripts) m.enabled = !state;   
        }

        if (state) Time.timeScale = 0;
        else       Time.timeScale = 1;
    }
}