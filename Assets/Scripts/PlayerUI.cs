using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI ecoCoinCountText;
    public TextMeshProUGUI turnCountText;
    public GameObject needDirectionWarning;

    // Start is called before the first frame update
    void Start()
    {
        needDirectionWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ecoCoinCountText.text = GameLogic.gameLogic.ecoCoins.ToString() + "$";
        turnCountText.text = "TURN: "+GameLogic.gameLogic.turnCount.ToString();
    }

    public void WarningPopup()
    {
        needDirectionWarning.SetActive(true);
        Invoke("HideWarning", 4);
    }

    public void HideWarning()
    {
        needDirectionWarning.SetActive(false);

    }

}
