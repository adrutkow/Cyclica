using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI ecoCoinCountText;
    public TextMeshProUGUI turnCountText;
    public GameObject needDirectionWarning;
    public TextMeshProUGUI winText;
    bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        needDirectionWarning.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogic.gameLogic.ecoCoins >= 1000) win = true;

        ecoCoinCountText.text = GameLogic.gameLogic.ecoCoins.ToString() + "$";
        turnCountText.text = "TURN: "+GameLogic.gameLogic.turnCount.ToString();
        winText.gameObject.SetActive(win);
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
