using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    public void FindSaveManagerSetSaveFilePath(string saveNumber)
    {
        GameObject.FindWithTag("SaveManager").GetComponent<SaveData>().setSaveFilePath(saveNumber);
    }

    public void FindSaveManagerLoad()
    {
        GameObject.FindWithTag("SaveManager").GetComponent<SaveData>().LoadGame();
    }

    public void FindSaveManagerSave()
    {
        GameObject.FindWithTag("SaveManager").GetComponent<SaveData>().SaveGame();
    }
}
