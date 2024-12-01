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

            DecisionListWrapper decisionWrapper;

            if (File.Exists(fileName))
            {
                string existingJson = File.ReadAllText(fileName);
                decisionWrapper = JsonUtility.FromJson<DecisionListWrapper>(existingJson);

                decisionWrapper.decisions.Add(newDecision);
            }
            else
            {
                decisionWrapper = new DecisionListWrapper
                {
                    participantID = GetHighestFileNumber() + 1,
                    decisions = new List<Decision>()
                };
                decisionWrapper.decisions.Add(newDecision);
            }

            string updatedJson = JsonUtility.ToJson(decisionWrapper, true);
            File.WriteAllText(fileName, updatedJson);
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error handling JSON file: {ex.Message}");
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
        public int participantID;
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
