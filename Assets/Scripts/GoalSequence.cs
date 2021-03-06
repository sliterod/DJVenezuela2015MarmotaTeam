﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalSequence : MonoBehaviour {

    public GameObject sequencePanel;
    public Text sequenceText;
    public Text sequenceTitle;
    public Transform sequenceCheck;
    public Transform ball;
    public Transform boss;

    //Arreglos de secuencia
    string[] keyArray;
    string[] capturedSequenceArray;

    //Booleans
    bool canCaptureSequence;
    bool canShootBall;
    bool isBallShot;

    //Valores numericos
    int arrayIndex;
    int correctAnswers;

    //Posicion del mouse
    Vector3 mouseClickPosition;

    //Posicion de balon
    Vector3 leftShotOk;
    Vector3 rightShotOk;
    Vector3 leftShotMiss;
    Vector3 rightShotMiss;

    //Vectores finales
    Vector3 ballFinalPosition;
    Vector3 bossPosition;

    //Animaciones
    Animations animations;

    // Use this for initialization
    void Start () {
        keyArray = new string[6];
        capturedSequenceArray = new string[6];
        arrayIndex = 0;

        leftShotOk = new Vector3(116.0f,0.0f,5.0f);
        leftShotMiss = new Vector3(111.0f, 0.0f, 5.0f);

        rightShotOk = new Vector3(116.0f, 0.0f, -5.0f);
        rightShotMiss = new Vector3(111.0f, 0.0f, -5.0f);

        animations = this.GetComponent<Animations>();

        GenerateSequence();
    }

    void Update() {
        if (canCaptureSequence) {

            if (Input.GetKeyDown(KeyCode.Z)) {
                CaptureSequence("Z");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                CaptureSequence("X");
            }

            animations.BossFirstAppearAnimation();

        }

        if (canShootBall) {
            if (Input.GetMouseButtonDown(0)) {//Disparar a la izquierda
                CheckMouseClickedSide(0);
            }
            else if (Input.GetMouseButtonDown(1)) {//Derecha
                CheckMouseClickedSide(1);
            }
        }

        if (isBallShot) {
            ShootBall();
            MoveBoss();
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
            sequenceText.text = sequenceText.text + keyArray[i] + "   ";
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

            if (arrayIndex == capturedSequenceArray.Length) {
                print("Revisando secuencia...");
                canCaptureSequence = false;
                canShootBall = true;

                print("Respuestas correctas: " + correctAnswers);
                DeactivateSequencePanel();
            }
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

    /// <summary>
    /// Desactiva el panel de secuencia
    /// </summary>
    void DeactivateSequencePanel() {
        sequenceCheck.gameObject.SetActive(false);

        if (correctAnswers == 6)
        {
            sequenceTitle.text = "¡Has visto a través de su defensa!";
            sequenceText.text = "¡Presiona click derecho o izquierdo para disparar!";

            this.GetComponent<SoundManager>().LoadAudioFile("cry");

            StartCoroutine(DeactivateCoroutine());

        }
        else if (correctAnswers < 6)
        {
            sequenceTitle.text = "¡Este será un tiro difícil!";
            sequenceText.text = "¡Ahora haz click para disparar!";

            this.GetComponent<SoundManager>().LoadAudioFile("laugh");

            StartCoroutine(DeactivateCoroutine());
        }
    }

    /// <summary>
    /// Desactiva el panel de secuencia
    /// </summary>
    /// <returns>La cantidad de segundos que debe esperar para ejecutar la accion</returns>
    IEnumerator DeactivateCoroutine() {
        yield return new WaitForSeconds(1.0f);

        sequencePanel.SetActive(false);
    }

    /// <summary>
    /// Revisa a que boton se le ha hecho click
    /// </summary>
    void CheckMouseClickedSide(int mouseButton) {

        if (mouseButton == 0)
        {//Izquierdo

            if (correctAnswers == 6)
            {
                ballFinalPosition = leftShotOk;
                bossPosition = rightShotOk;

                animations.CharacterShotAnimation(true, true);
            }
            else if (correctAnswers != 6)
            {
                ballFinalPosition = leftShotMiss;
                bossPosition = leftShotMiss;

                animations.CharacterShotAnimation(true, false);
            }
        }
        else if (mouseButton == 1)
        {//Derecho
            if (correctAnswers == 6)
            {
                ballFinalPosition = rightShotOk;
                bossPosition = leftShotOk;

                animations.CharacterShotAnimation(true, true);
            }
            else if (correctAnswers != 6)
            {
                ballFinalPosition = rightShotMiss;
                bossPosition = rightShotMiss;

                animations.CharacterShotAnimation(true, false);
            }
        }

        canShootBall = false;
        isBallShot = true;

    }

    /// <summary>
    /// Dispara la pelota
    /// </summary>
    void ShootBall() {
        
        if (ball.position.x <= ballFinalPosition.x)
        {
            ball.position = new Vector3(ball.position.x + 0.3f,
                                        ball.position.y,
                                        ball.position.z);
        }

        if (ball.position.y <= ballFinalPosition.y)
        {
            ball.position = new Vector3(ball.position.x,
                                        ball.position.y + 0.1f,
                                        ball.position.z);
        }

        if (ballFinalPosition.z > 0) {
            if (ball.position.z <= ballFinalPosition.z)
            {
                ball.position = new Vector3(ball.position.x,
                                            ball.position.y,
                                            ball.position.z + 0.1f);
            }
        }
        else if (ballFinalPosition.z < 0) {
            if (ball.position.z >= ballFinalPosition.z)
            {
                ball.position = new Vector3(ball.position.x,
                                            ball.position.y,
                                            ball.position.z - 0.1f);
            }
        }
        

        if (ball.position.x >= ballFinalPosition.x &&
            ball.position.y >= ballFinalPosition.y)
        {
            isBallShot = false;

            if (correctAnswers == 6)
            {
                animations.BossJumpDefeatedAnimation();
            }
            else if (correctAnswers != 6)
            {
                animations.BossTauntAnimation();
            }

            StartCoroutine(ActivatePanel());
        }
    }

    /// <summary>
    /// Mueve el jefe para que detenga o no la pelota
    /// </summary>
    void MoveBoss() {

        animations.BossJumpDeflectAnimation();

        if (boss.position.y <= bossPosition.y)
        {
            boss.position = new Vector3(boss.position.x,
                                        boss.position.y + 0.2f,
                                        boss.position.z);
        }

        if (bossPosition.z > 0.0f) {
            if (boss.position.z <= bossPosition.z)
            {
                boss.position = new Vector3(boss.position.x,
                                            boss.position.y,
                                            boss.position.z + 0.2f);
            }
        }
        else if (bossPosition.z < 0.0f) {
            if (boss.position.z >= bossPosition.z)
            {
                boss.position = new Vector3(boss.position.x,
                                            boss.position.y,
                                            boss.position.z - 0.2f);
            }
        }
        
    }


    IEnumerator ActivatePanel() {
        yield return new WaitForSeconds(3.0f);

        ActivateVictoryDefeatPanel();
    }

    /// <summary>
    /// Activa el panel correspondiente a la victoria o derrota
    /// </summary>
    void ActivateVictoryDefeatPanel()
    {
        if (correctAnswers == 6) {
            this.GetComponent<GamePanels>().ActivateVictoryPanel();
        }
        else if (correctAnswers != 6) {
            this.GetComponent<GamePanels>().ActivateDefeatPanel();
        }

    }
}
