using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class StoreJsonData : MonoBehaviour
{
    private string filePath = "";
    private string fileName = "";

    private List<Decision> _DecisionDataList = new List<Decision>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        try
        {
            filePath = System.IO.Directory.GetCurrentDirectory() + "/JsonData";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            
            int highestNumber = GetHighestFileNumber() + 1;
            fileName = Path.Combine(filePath, $"data{highestNumber}.json");
        }
        catch (IOException ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StoreData("Scene1", "Decision1", "Option1");
        }
    }

    public void StoreData(string scene, string decisionName, string selectedDecision)
    {
        try
        {
            Decision newDecision = new Decision
            {
                scene = scene,
                DecisionName = decisionName,
                SelectedDecision = selectedDecision
            };

            _DecisionDataList.Add(newDecision);

            string decisionJson = JsonUtility.ToJson(new DecisionListWrapper { decisions = _DecisionDataList }, true);

            if (!string.IsNullOrEmpty(fileName))
            {
                File.WriteAllText(fileName, decisionJson);
            }
            else
            {
                Debug.LogError("File name is not set. Cannot save data.");
            }
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error creating JSON file: {ex.Message}");
        }
    }

    [System.Serializable]
    public class Decision
    {
        public string scene;
        public string DecisionName;
        public string SelectedDecision;
    }

    [System.Serializable]
    public class DecisionListWrapper
    {
        public List<Decision> decisions;
    }

    private int GetHighestFileNumber()
    {
        int highestNumber = 0;

        if (Directory.Exists(filePath))
        {
            string[] files = Directory.GetFiles(filePath, "data*.json");

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (fileName.StartsWith("data"))
                {
                    string numberPart = fileName.Substring(4);
                    if (int.TryParse(numberPart, out int fileNumber))
                    {
                        highestNumber = Mathf.Max(highestNumber, fileNumber);
                    }
                }
            }
        }

        return highestNumber;
    }
}
