using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mision : MonoBehaviour
{
    GameObject player;
    TextMeshProUGUI textMeshProUGUI;
    Scene currentScene;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        if (textMeshProUGUI == null)
            Debug.Log("Error text mesh pro");
    }
    private void Update()
    {
        if (currentScene.name == "Tutorial")
        {
            textMeshProUGUI.text = "Botellas energia:" + player.gameObject.GetComponent<PlayerGame>().bottle1.ToString() + "/5";
        }
        else if(currentScene.name == "Level 1")
        {
            textMeshProUGUI.text = "Botellas energia:" + player.gameObject.GetComponent<PlayerGame>().bottle1.ToString() + "/5\n" 
                + "Botellas salud: " + player.gameObject.GetComponent<PlayerGame>().bottle2.ToString() + "/ 3";
        }
        else if (currentScene.name == "Level 2")
        {
            textMeshProUGUI.text = "Botellas energia:" + player.gameObject.GetComponent<PlayerGame>().bottle1.ToString() + "/10\n"
                + "Botellas salud:" + player.gameObject.GetComponent<PlayerGame>().bottle2.ToString() + "/5";
        }
    }
}
