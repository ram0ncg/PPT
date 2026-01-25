using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public int scene;
    [NonSerialized] public int playerElection = -1;
    [NonSerialized] public int machineElection;

    public static int playerPoints;
    public static int machinePoints;
    public int maxRound = 3;
    public int round;

    private TextMeshProUGUI text;
    
    private GameObject pptButtons;
    public float winCon;

    public CanvasManager cm;
    public Dictionary<int, string> ppt = new Dictionary<int, string>
    {
        { 0,   "Piedra" },
        { 1,  "Papel" },
        { 2,  "Tijera" },
    };
    private void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        switch (scene)
        {
            case 0:
                break;
            case 1:
                round = 1;
                playerPoints = 0;
                machinePoints = 0;
                winCon = maxRound / 2f;  winCon += 0.5f;
                text = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();  
                pptButtons = GameObject.Find("PPT");
                pptButtons.SetActive(false);
                StartCoroutine(Game());
                break;
            case 2:
                break;
        }
    }
    public void RoundExecution()
    {
        bool win = false;
        bool tie = false;
        bool end = false;
        if (playerElection != -1) //El jugador ha elejido
        {
            machineElection = UnityEngine.Random.Range(0, 3);
            if (playerElection != machineElection)
            {
                switch (playerElection)
                {
                    case 0:
                        if (machineElection == 1)
                        {
                            machinePoints++;
                        }
                        else
                        {
                            playerPoints++;
                            win = true;
                        }
                        break;
                    case 1:
                        if (machineElection == 0)
                        {
                            playerPoints++;
                            win = true;
                        }
                        else
                        {
                            machinePoints++;
                        }
                        break;
                    case 2:
                        if (machineElection == 0)
                        {
                            machinePoints++;
                        }
                        else
                        {
                            playerPoints++;
                            win = true;
                        }
                        break;
                }
            }
            else
            {
                tie = true;
            }
        }
        if (playerPoints >= winCon || machinePoints >= winCon) //Si se alcanza la cantidad de rondas maximas para ganar.
        {
            end = true;
        }
        StartCoroutine(RoundResult(tie, win, end, machineElection, playerElection)); 
        
    }
    IEnumerator RoundResult(bool tie, bool win, bool end, int mE, int pE) //Muestar por pantalla el resultado de la ronda.
    {
        pptButtons.SetActive(false);
        round++;
        string t = "La maquina uso: " + ppt[mE] + " ---- El jugador uso: " + ppt[pE];
        if (end)
        {
            t += playerPoints >= winCon ? "\nEL JUGADOR" : "\nLA MAQUINA";
            t += " GANA LA PARTIDA\n" + playerPoints + " - " + machinePoints;
            text.text = t;
            yield break;
        }
        else
        {
            if (tie)
            {
                t += "\nEMPATE!\nSe jugara otra ronda.";
            }
            else
            {
                t += win ? "\n\nGana el jugador!" : "\n\nLa maquina gana!";
            }
            text.text = t;
            yield return new WaitForSeconds(2.5f);
            text.text = playerPoints + " - " + machinePoints;
            yield return new WaitForSeconds(3f);
            StartCoroutine(Game());
        }
        
    }
    IEnumerator Game()
    {
        playerElection = -1;
        if (round == 1)
        {
            text.text = "BIENVENIDO AL JUEGO PIEDRA-PAPEL-TIJERA\n Mejor de " + maxRound;
            yield return new WaitForSeconds(2.5f);
        }       
        text.text = "RONDA: " + round;
        yield return new WaitForSeconds(2.5f);
        text.text = "ELIGE:";
        pptButtons.SetActive(true);
    }
}
