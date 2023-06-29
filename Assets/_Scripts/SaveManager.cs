using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class managing the save system. It needs an instance of InventoryManager to work.
/// Save names are supposed to look like this: save001.json, save002.json, etc.
/// One run = one save. You can't have multiple saves per run.
/// 
/// Save files are located under My Documents\PotionCraftVR\Saves
/// </summary>
public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryManager;

    private List<string> saveNames = new List<string>();
    private readonly string MyDocumentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

    // Start is called before the first frame update
    void Start()
    {
        // Create save folder if it doesn't exist
        if (!System.IO.Directory.Exists(MyDocumentsPath + "/PotionCraftVR/Saves"))
            System.IO.Directory.CreateDirectory(MyDocumentsPath + "/PotionCraftVR/Saves");

        // Get all save names
        foreach (string saveName in System.IO.Directory.GetFiles(MyDocumentsPath + "/PotionCraftVR/Saves"))
        {
            saveNames.Add(saveName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnApplicationQuit is called when the application is about to quit
    private void OnApplicationQuit()
    {
        // Removes savename from playerprefs to avoid problems when the game is restarted
        PlayerPrefs.DeleteKey("SaveName");
    }

    public void SaveGame()
    {
        // Get save name
        string saveName = PlayerPrefs.GetString("SaveName");

        Debug.Log(saveName);

        // if save name is empty, generate one
        if (string.IsNullOrWhiteSpace(saveName))
        {
            saveName = "save" + (saveNames.Count + 1).ToString("000") + ".json";

            // Also save saveName in PlayerPrefs in case the player continues playing.
            PlayerPrefs.SetString("SaveName", saveName);
        }

        // Save inventory to json then to file
        string json = JsonUtility.ToJson(inventoryManager.GetComponent<InventoryManager>());

        System.IO.File.WriteAllText(MyDocumentsPath + "/PotionCraftVR/Saves/" + saveName, json);
    }

    // Sets the save name in PlayerPrefs
    public void SetSaveNameInPlayerPrefs(string saveName)
    {
        PlayerPrefs.SetString("SaveName", saveName);
    }

    // Loads a savefile
    public void LoadGame()
    {
        // Get save name
        string saveName = PlayerPrefs.GetString("SaveName");

        if (string.IsNullOrWhiteSpace(saveName))
        {
            Debug.Log("Save name is empty, treat the run as a new run.");
            return;
        }

        // Check if the save exists
        if (!System.IO.File.Exists(MyDocumentsPath + "/PotionCraftVR/Saves/" + saveName))
        {
            Debug.LogError("Error: Save file " + saveName + " doesn't exist.");
            return;
        }
        
        // Read savefile, deserialize it and set the data to the existing inventory manager
        string json = System.IO.File.ReadAllText(MyDocumentsPath + "/PotionCraftVR/Saves/" + saveName);
        InventoryManager tempInventory = JsonUtility.FromJson<InventoryManager>(json);
        inventoryManager.GetComponent<InventoryManager>().Inventory = tempInventory.Inventory;
        inventoryManager.GetComponent<InventoryManager>().Recipes = tempInventory.Recipes;
    }
}
