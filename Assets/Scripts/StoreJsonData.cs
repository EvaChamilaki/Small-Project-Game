using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class StoreJsonData : MonoBehaviour
{
    public GameObject participantIDInputField;
    public GameObject errorParticipantExists;
    public GameObject endScreen;
    private string participantID;
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
        }
        catch (IOException ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void createFile()
    {
        participantID = participantIDInputField.GetComponent<TMPro.TMP_InputField>().text;
        
        fileName = Path.Combine(filePath, $"data{participantID}.json");

        if (File.Exists(fileName))
        {
            StartCoroutine(ShowError());
        }
        else
        {
            endScreen.SetActive(true);
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
                    participantIDVal = participantID,
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
        public string participantIDVal;
        public List<Decision> decisions;
    }

    private IEnumerator ShowError()
    {
        errorParticipantExists.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        errorParticipantExists.SetActive(false);
    }
}
