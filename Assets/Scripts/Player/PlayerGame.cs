using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGame : MonoBehaviour
{
    SceneController sceneController;
    [SerializeField] public float live = 100;
    [SerializeField] bool godMode = false;
    [SerializeField] float time_goodMode = 1f;
    public int bottle1 = 0;
    public int bottle2 = 0;
    [SerializeField] GameObject dangerous;
    private float timerSinceDamage;
    public void Update()
    {
        timerSinceDamage += Time.deltaTime;
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
                sceneController.CheckLose();
            }
        }
    }
    IEnumerator God()
    {
        godMode = true;
        yield return new WaitForSeconds(time_goodMode);
        godMode = false;
    }
    public void Bottle(int value)
    {
        bottle1 += value;
    }
    public void Bottle2(int value)
    {
        bottle2 += value;
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
