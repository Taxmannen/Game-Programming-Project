using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene("Main");
            Time.timeScale = 1;
            if (Application.isEditor) Utils.ClearLogConsole();
        }
    }

    public static class Utils
    {
        static MethodInfo _clearConsoleMethod;
        static MethodInfo ClearConsoleMethod
        {
            get
            {
                if (_clearConsoleMethod == null)
                {
                    #if UNITY_EDITOR
                        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
                        Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                        _clearConsoleMethod = logEntries.GetMethod("Clear");
                    #endif
                }
                return _clearConsoleMethod;
            }
        }

        public static void ClearLogConsole()
        {
            ClearConsoleMethod.Invoke(new object(), null);
        }
    }
}