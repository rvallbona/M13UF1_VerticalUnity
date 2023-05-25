using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    private void Awake()
    {
        
    }
    public void ChangeSceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
