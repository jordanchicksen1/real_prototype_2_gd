using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class regenManager : MonoBehaviour
{
    public int regen;
    public TextMeshProUGUI regenText;


    public void addRegen()
    {
        regen = regen + 1;
        regenText.text = regen.ToString();
    }

    public void subtractRegen()
    {
        regen = regen - 1;
        regenText.text = regen.ToString();
    }
}
