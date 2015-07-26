using UnityEngine;
using System.Collections;

public class GamePanels : MonoBehaviour {

    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject pausePanel;

    //Booleans
    bool defeatPanelOn;
    bool victoryPanelOn;
    bool pauseOn;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseOn) {
                Application.LoadLevel("intro");
            }

            if (victoryPanelOn || defeatPanelOn) {
                Application.LoadLevel("intro");
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pauseOn)
            {
                ActivatePause(false);
            }
            else if (!pauseOn) {
                ActivatePause(true);
            }

            if (victoryPanelOn || defeatPanelOn)
            {
                Application.LoadLevel("main");
            }
        }
    }

    /// <summary>
    /// Activar panel de victoria
    /// </summary>
    public void ActivateVictoryPanel() {
        victoryPanel.SetActive(true);
        ActivatePause(false);
        victoryPanelOn = true;
        defeatPanelOn = false;
    }

    /// <summary>
    /// Activar panel de derrota
    /// </summary>
    public void ActivateDefeatPanel() {
        defeatPanel.SetActive(true);
        ActivatePause(false);
        victoryPanelOn = false;
        defeatPanelOn = true;
    }

    /// <summary>
    /// Activa el panel de pausa
    /// </summary>
    void ActivatePause(bool state) {
        pausePanel.SetActive(state);
        pauseOn = state;

        this.SendMessage("SetPause",state);

        if (state == true) {
            Time.timeScale = 0.0f;
        }
        else if (state == false)
        {
            Time.timeScale = 1.0f;
        }
    }
}
