using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject canvasMainMenu, canvasOptions, canvasCheats, canvasCredits;
    public void ChangeScene(int nscene)
    {
        SceneManager.LoadScene(nscene);
    }
    public void ChangeOptionCanvas()
    {
        canvasOptions.SetActive(true);
        canvasMainMenu.SetActive(false);
    }
    public void ChangeCheatsCanvas()
    {
        canvasCheats.SetActive(true);
        canvasMainMenu.SetActive(false);
    }
    public void ChangeCreditsCanvas()
    {
        canvasCredits.SetActive(true);
        canvasMainMenu.SetActive(false);
    }
    public void BackOptions()
    {
        canvasOptions.SetActive(false);
        canvasMainMenu.SetActive(true);
    }
    public void BackCheats()
    {
        canvasCheats.SetActive(false);
        canvasMainMenu.SetActive(true);
    }
    public void BackCredits()
    {
        canvasCredits.SetActive(false);
        canvasMainMenu.SetActive(true);
    }
}
