using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class StoreJsonData : MonoBehaviour
{
    public string filePath = "";

    void Awake()
    {
        try
        {
            filePath = System.IO.Directory.GetCurrentDirectory() + "/JsonData";
            Debug.Log(filePath);
            if (!Directory.Exists(filePath))
            {
                Debug.Log("Creating JsonData folder");
                Directory.CreateDirectory(filePath);
            }
        }
        catch (IOException ex)
        {
            Debug.LogError(ex.Message);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StoreData();
        }
    }

    public void StoreData()
    {
        try
        {
            Debug.Log(filePath);
            int highestNumber = GetHighestFileNumber() + 1;
            string fileName = Path.Combine(filePath, $"data{highestNumber}.json");

            Debug.Log($"File created: {fileName}");
            string sampleData = "{\"message\": \"This is sample data\"}";
            File.WriteAllText(fileName, sampleData);
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error creating JSON file: {ex.Message}");
        }
    }

    private int GetHighestFileNumber()
    {
        int highestNumber = 0;

        // Check all files in the directory
        if (Directory.Exists(filePath))
        {
            string[] files = Directory.GetFiles(filePath, "data*.json");

            foreach (string file in files)
            {
                // Extract the number part from the file name
                string fileName = Path.GetFileNameWithoutExtension(file); // e.g., "data1"
                if (fileName.StartsWith("data"))
                {
                    string numberPart = fileName.Substring(4); // Extract "1" from "data1"
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
