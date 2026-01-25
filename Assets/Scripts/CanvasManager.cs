using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameManager gm;
    public int playerElection;
    void Start()
    {
        switch (gm.scene) {
            case 0:
                break;
            case 1:
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
    public void Election(int id)
    {
        gm.playerElection = id;
        gm.RoundExecution();
    }
}
