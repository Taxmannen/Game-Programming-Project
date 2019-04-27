﻿using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public int frameRate;
    public bool lockedFrameRate;

    private void Start()
    {
        if (lockedFrameRate) QualitySettings.vSyncCount = 0;
        else                 QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = frameRate;    
    }

    private void Update()
    {
        if (Input.GetButtonDown("Restart")) LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetButtonDown("Menu")) LoadScene("Menu");
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        if (Application.isEditor) Utils.ClearLogConsole();
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