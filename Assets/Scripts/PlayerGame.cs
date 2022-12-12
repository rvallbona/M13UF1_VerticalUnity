using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGame : MonoBehaviour
{
    [SerializeField] public float live = 100;
    [SerializeField] bool godMode = false;
    [SerializeField] float time_goodMode = 1f;
    [SerializeField] int bottle = 0;
    public void Update()
    {
        if (live <= 0 || bottle >= 5)
        {
            RestartLevel("Tutorial");
        }
        Debug.Log(live);
        Debug.Log(bottle);
    }

    public void Damage(int dmg)
    {
        if (!godMode && live >= 0)
        {
            live -= dmg;
            StartCoroutine(God());
            if (live <= 0)
            {
                GameOver();
            }
        }
    }
    void GameOver()
    {
        Debug.Log("GameOver");
        Time.timeScale = 0;
    }
    IEnumerator God()
    {
        godMode = true;
        yield return new WaitForSeconds(time_goodMode);
        godMode = false;
    }
    public void Coin(int value)
    {
        bottle += value;
    }
    void RestartLevel(string nscene)
    {
        SceneManager.LoadScene(nscene);
    }
}
