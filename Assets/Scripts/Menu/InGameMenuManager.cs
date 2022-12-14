using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject canvasPauseMenu, canvasOptions, canvasCheats;
    public void ChangeScene(int nscene)
    {
        SceneManager.LoadScene(nscene);
        Time.timeScale = 1;
    }
    public void ChangeOptionCanvas()
    {
        canvasOptions.SetActive(true);
        canvasPauseMenu.SetActive(false);
    }
    public void ChangeCheatsCanvas()
    {
        canvasCheats.SetActive(true);
        canvasPauseMenu.SetActive(false);
    }
    public void BackOptions()
    {
        canvasOptions.SetActive(false);
        canvasPauseMenu.SetActive(true);
    }
    public void BackCheats()
    {
        canvasCheats.SetActive(false);
        canvasPauseMenu.SetActive(true);
    }
    public void BackGame()
    {
        Debug.Log("Resume");
        canvasPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}