using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour    //TUTORIAL SCENE CONTROLLER
{
    [SerializeField] int numBottles1ToWin, numBottles2ToWin, sceneNumberNext;
    GameObject player;
    PlayerGame game;
    private int bottel1, bottel2;
    private float live;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        game = player.gameObject.GetComponent<PlayerGame>();
    }
    private void Start()
    {
        Data();
    }
    private void Update()
    {
        Data();
        CheckWin();
        CheckLose();
    }
    void Data()
    {
        bottel1 = game.bottle1;
        bottel2 = game.bottle2;
        live = game.live;
    }
    void CheckWin() 
    {
        if (bottel1 >= numBottles1ToWin && bottel2 >= numBottles2ToWin)
        {
            NextScene(sceneNumberNext);
        }
    }
    public void CheckLose()
    {
        if (live <= 0)
        {
            Time.timeScale = 0;
            NextScene(5);
        }
    }
    void NextScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
