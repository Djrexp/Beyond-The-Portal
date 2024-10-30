using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI acornCountText;
    public TextMeshProUGUI galacticAcornCountText;
    private int acornCount = 0;
    private int galacticAcornCount = 0;

    // Updates the acorn count
    public void UpdateAcornCount(int acorns){
        acornCount += acorns;
        acornCountText.text = acornCount.ToString();
    }

    // Updates the galactic acorn count
    public void UpdateGalacticAcornCount(int galacticAcorns){
        galacticAcornCount += galacticAcorns;
        galacticAcornCountText.text = galacticAcornCount.ToString() + "/1";
    }
}
