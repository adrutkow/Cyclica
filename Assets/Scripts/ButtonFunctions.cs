using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayTurnButtonPressed()
    {
        GameLogic.gameLogic.TryPlayTurn();
    }

    public void onMenuPlayButtonPressed()
    {
        SceneManager.LoadScene("Level0");
    }

    public void onRestartButtonPressed()
    {
        SceneManager.LoadScene("Level0");
    }
}
