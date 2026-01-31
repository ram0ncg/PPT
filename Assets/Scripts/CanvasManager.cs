using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameManager gm;
    public int playerElection;
    public Image player;
    public Image machine;
    public Sprite scissors;
    public Sprite paper;
    public Sprite rock;
    public Dictionary<int, Sprite> pptS;
    void Start()
    {
        switch (gm.scene) {
            case 0:
                break;
            case 1:
                pptS = new Dictionary<int, Sprite>
                {
                    { 0,  rock },
                    { 1,  paper },
                    { 2,  scissors },
                };
                break;
            case 2:
                break;
        } 
    }
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChangeSprite(bool p, int id)
    {
        if (p)
        {
            player.sprite = pptS[id];
        }
        else
        {
            machine.sprite = pptS[id];
        }
    }
    public void Election(int id)
    {
        gm.playerElection = id;
        gm.RoundExecution();
       
    }
}
