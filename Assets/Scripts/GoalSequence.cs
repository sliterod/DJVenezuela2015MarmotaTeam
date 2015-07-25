using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalSequence : MonoBehaviour {

    public GameObject sequencePanel;
    public Text sequenceText;
    public Transform sequenceCheck;

    //Arreglos de secuencia
    string[] keyArray;
    string[] capturedSequenceArray;

    //Booleans
    bool canCaptureSequence;

    //Valores numericos
    int arrayIndex;
    int correctAnswers;

	// Use this for initialization
	void Start () {
        keyArray = new string[6];
        capturedSequenceArray = new string[6];
        arrayIndex = 0;

        GenerateSequence();
    }

    void Update() {
        if (canCaptureSequence) {

        }
    }

    /// <summary>
    /// Genera la sequencia que se debe presionar para ganar el juego
    /// </summary>
    void GenerateSequence() {

        float result = 0.0f;

        for (int i = 0; i < keyArray.Length; i++) {
            result = Random.Range(0.0f, 1.0f);

            if (result > 0.0f && result <= 0.5f) {
                keyArray[i] = "Z";
            }
            else if (result > 0.5f && result <= 1.0f) {
                keyArray[i] = "X";
            }
        }

        print("Secuencia: " + keyArray[0] + keyArray[1] + keyArray[2] + keyArray[3] + keyArray[4] + keyArray[5]);
        
    }

    /// <summary>
    /// Activa el panel de sequencias y llena la etiqueta con la secuencia a presionar
    /// </summary>
    void ActivateSequencePanel() {
        sequencePanel.SetActive(true);
        FillSequencePanel();

        canCaptureSequence = true;
    }

    /// <summary>
    /// Llena en la interfaz el panel donde se va a mostrar la secuencia de caracteres a presionar
    /// </summary>
    void FillSequencePanel() {

        for (int i = 0; i < keyArray.Length; i++)
        {
            if (i == 0)
            {
                sequenceText.text = keyArray[i];
            }
            else {
                sequenceText.text = "   " + sequenceText.text + keyArray[i];
            }
            
        }
    }

    /// <summary>
    /// Captura las teclas presionadas por el usuario
    /// </summary>
    /// <param name="key">La tecla presionada</param>
    void CaptureSequence(string key) {

        if (arrayIndex < capturedSequenceArray.Length)
        {
            capturedSequenceArray[arrayIndex] = key;
            CheckSequence();

            arrayIndex += 1;
        }
        else {
            print("Revisando secuencia...");
            canCaptureSequence = false;
        }

    }

    /// <summary>
    /// Revisa que la secuencia esté correcta
    /// </summary>
    void CheckSequence() {

        if (capturedSequenceArray[arrayIndex] == keyArray[arrayIndex])
        {
            sequenceCheck.FindChild("Check" + arrayIndex.ToString()).GetComponent<Image>().color = new Color(0.0f,
                                                                                                             1.0f,
                                                                                                             0.0f,
                                                                                                             1.0f);

            correctAnswers += 1;
        }
        else {
            sequenceCheck.FindChild("Check" + arrayIndex.ToString()).GetComponent<Image>().color = new Color(1.0f, 
                                                                                                             0.0f,
                                                                                                             0.0f,
                                                                                                             1.0f);
        }

    }
}
