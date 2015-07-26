using UnityEngine;
using System.Collections;

public class MenuOptions : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject credits;
    public GameObject howTo;

    /// <summary>
    /// Inicia el juego
    /// </summary>
    void StartGame() {
        Application.LoadLevel(1);
    }

    /// <summary>
    /// Mostrar los creditos
    /// </summary>
    void ShowCredits() {
        mainMenu.SetActive(false);
        howTo.SetActive(false);
        credits.SetActive(true);
    }

    /// <summary>
    /// Salir del juego
    /// </summary>
    void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Como jugar
    /// </summary>
    void HowToPlay() {
        credits.SetActive(false);
        mainMenu.SetActive(false);
        howTo.SetActive(true);
    }

    /// <summary>
    /// Regresa al menu principal
    /// </summary>
    void ReturnToMain() {
        credits.SetActive(false);
        howTo.SetActive(false);
        mainMenu.SetActive(true);
    }
}
