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
    int bottle = 0;
    int bottle2 = 0;
    [SerializeField] GameObject dangerous;
    private float timerSinceDamage;
    public void Update()
    {
        timerSinceDamage += Time.deltaTime;
        if (live <= 0)
        {
            NextLevel(3);
        }
        if (bottle >= 5)
        {
            NextLevel(2);//next level
        }
        if (bottle2 >= 3)
        {
            NextLevel(2);//next level
        }
        ResetLive();
        Danger();
    }

    public void Damage(int dmg)
    {
        if (!godMode && live >= 0)
        {
            live -= dmg;
            timerSinceDamage = 0;
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
    public void Bottle(int value)
    {
        bottle += value;
    }
    public void Bottle2(int value)
    {
        bottle2 += value;
    }
    void RestartLevel(string nscene)
    {
        SceneManager.LoadScene(nscene);
    }
    void NextLevel(int num)
    {
        SceneManager.LoadScene(num);
    }
    void Danger()
    {
        if (live <= 20)
        {
            dangerous.SetActive(true);
        }
        else
        {
            dangerous.SetActive(false);
        }
    }
    void ResetLive()
    {
        if (timerSinceDamage >= 3 && live < 100)
        {
            live += .01f;
        }
    }
}
