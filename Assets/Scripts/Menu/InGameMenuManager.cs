using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject canvasPauseMenu, canvasOptions, canvasCheats;
    [SerializeField] AudioSource audioInGame, audioMenu;
    public void ChangeScene(int nscene)
    {
        SceneManager.LoadScene(nscene);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
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
        canvasPauseMenu.SetActive(false);
        audioMenu.Stop();
        audioInGame.Play();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}