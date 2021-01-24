using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts;
using System.IO;
using FullSerializer;

public class DemoEditor : EditorWindow
{
    int  prevWidth = 0;
    int  prevHeight = 0;
    int selectedIndex = 0;

    Level currenLevel;

    Vector2 scrollPos;

    [MenuItem("Window/Demo Editor/Editor")]
    static void Init()
    {
        var window = GetWindow(typeof(DemoEditor));
        window.titleContent = new GUIContent("Demo Editor");
    }

    private void OnEnable()
    {
        currenLevel = new Level();
    }

    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("SAVE", GUILayout.Width(50)))
            {
                SaveToJson();

            }
            if (GUILayout.Button("OPEN", GUILayout.Width(50)))
            {
                OpenJson();
            }
            GUILayout.EndHorizontal();
        } // Save button

        DrawMatrix();


        GUILayout.EndScrollView();
    }

    private void SaveToJson()
    {
#if UNITY_EDITOR
        string path = $"{Application.dataPath} /Resources/Levels/{currenLevel.levelId}.json";
        SaveJsonFile(path, currenLevel);
        AssetDatabase.Refresh();
#endif
    }

    private void SaveJsonFile<T> (string path, T data) where T : class
    {
        fsData fsData;

        var serializer = new fsSerializer();
        serializer.TrySerialize(data, out fsData).AssertSuccessWithoutWarnings();

        var file = new StreamWriter(path);
        var json = fsJsonPrinter.PrettyJson(fsData);
        file.WriteLine(json);
        file.Close();
    }

    private void OpenJson()
    {
        var path = EditorUtility.OpenFilePanel("Open level", Application.dataPath + "/Resources/Levels", "json");
        if (!string.IsNullOrEmpty(path))
        {
            currenLevel = LoadJsonFile<Level>(path);
        }
    }

    private T LoadJsonFile<T> (string path) where T : class
    {
        if (!File.Exists(path))
        {
            return null;
        }

        var file = new StreamReader(path);
        var fileContents = file.ReadToEnd();
        var data = fsJsonParser.Parse(fileContents);

        object deserialized = null;

        var serializer = new fsSerializer();
        serializer.TryDeserialize(data,typeof(T), ref deserialized).AssertSuccessWithoutWarnings();
        file.Close();
        return deserialized as T;
    }

    void DrawMatrix() 
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level Id", GUILayout.Width(EditorGUIUtility.labelWidth));
        currenLevel.levelId = EditorGUILayout.IntField(currenLevel.levelId, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        prevWidth = currenLevel.Width;
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Width", GUILayout.Width(EditorGUIUtility.labelWidth));
        currenLevel.Width = EditorGUILayout.IntField(currenLevel.Width, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        prevHeight = currenLevel.Height;
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Height", GUILayout.Width(EditorGUIUtility.labelWidth));
        currenLevel.Height = EditorGUILayout.IntField(currenLevel.Height, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Type of Riser", GUILayout.Width(EditorGUIUtility.labelWidth));
        selectedIndex = GUILayout.SelectionGrid(selectedIndex, new[] { "None", "Riser" }, 2, GUILayout.Width(150));
        GUILayout.EndHorizontal();

        if (currenLevel.Width != prevWidth || currenLevel.Height != prevHeight)
        {
            prevHeight = currenLevel.Height;
            prevWidth = currenLevel.Width;

            currenLevel.Tiles = new List<Tile>();

            for (int i = 0; i < currenLevel.Height; i++)
            {
                for (int j = 0; j < currenLevel.Width; j++)
                {
                    currenLevel.Tiles.Add(new Tile() { type = Type.None });
                }
            }
        }

        for (int i = 0; i < currenLevel.Height; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < currenLevel.Width; j++)
            {
                var index = currenLevel.Width * i + j;
                try
                {
                    if (currenLevel.Tiles.Count >= index && currenLevel.Tiles[index] != null)
                    {
                        DrawButton(i, j, index);
                    }
                }
                catch 
                {
                    //Debug.Log($"index {index} - {i}-{j}");
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    void DrawButton(int i, int j , int index)
    {
        if (currenLevel.Tiles[index].type != Type.None)
        {
            if (GUILayout.Button($"{i}-{j}", GUILayout.Width(40), GUILayout.Height(40)))
            {
                currenLevel.Tiles[index].type = (Type)selectedIndex;
            }
        }
        else
        {
            if (GUILayout.Button("", GUILayout.Width(40), GUILayout.Height(40)))
            {
                currenLevel.Tiles[index].type = (Type)selectedIndex;
            }
        }
    }
}
