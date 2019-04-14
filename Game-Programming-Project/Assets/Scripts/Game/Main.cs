﻿using System;
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
            Utils.ClearLogConsole();
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            LootDropData data;
            data = Resources.Load<LootDropData>("Loot Drop Data/Low Drop Chance");
            data.DropItem(transform.position);
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
                    Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
                    Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                    _clearConsoleMethod = logEntries.GetMethod("Clear");
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
