using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization;
using static UnityEngine.Rendering.DebugUI;

public class StartName : MonoBehaviour
{
    public LocalizedString heroName;
    public TMPro.TMP_Text nameText;


    private void Awake()
    {
        (heroName["heroname"] as StringVariable).Value = SaveData.instance.playerData.HeroName;
        nameText.text = heroName.GetLocalizedString().ToUpper();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
