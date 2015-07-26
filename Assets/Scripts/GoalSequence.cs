using UnityEngine;
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
    bool bossMoves;

    //Valores numericos
    int arrayIndex;
    int correctAnswers;

    //Posicion del mouse
    Vector3 mouseClickPosition;

	// Use this for initialization
	void Start () {
        keyArray = new string[6];
        capturedSequenceArray = new string[6];
        arrayIndex = 0;

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

        }

        if (canShootBall) {
            if (Input.GetMouseButtonDown(0)) {
                CheckMousePosition();
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
            sequenceText.text = "¡Ahora haz click para disparar!";

            bossMoves = false;
        }
        else if (correctAnswers < 6)
        {
            sequenceTitle.text = "¡Este será un tiro difícil!";
            sequenceText.text = "¡Ahora haz click para disparar!";

            bossMoves = true;
        }
    }

    /// <summary>
    /// Revisa la posición actual del Mouse
    /// </summary>
    void CheckMousePosition() {
        Vector3 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        mouseClickPosition = mousePos;

        print("Posición actual del mouse: " + mouseClickPosition);

        canShootBall = false;
        isBallShot = true;
    }

    /// <summary>
    /// Dispara la pelota
    /// </summary>
    void ShootBall() {

        if (!bossMoves)
        {
            print("Jefe no se mueve");
            if (ball.position.x <= 118.0f)//118 posicion de la porteria
            {
                ball.position = new Vector3(ball.position.x + 0.3f,
                                            ball.position.y,
                                            ball.position.z);
            }

            if (ball.position.y <= mouseClickPosition.y)
            {
                ball.position = new Vector3(ball.position.x,
                                            ball.position.y + 0.1f,
                                            ball.position.z);
            }

            if (ball.position.z <= mouseClickPosition.z)
            {
                ball.position = new Vector3(ball.position.x,
                                            ball.position.y,
                                            ball.position.z + 0.1f);
            }

            if (ball.position.x >= 118.0f &&
                ball.position.y >= mouseClickPosition.y &&
                ball.position.z >= mouseClickPosition.z)
            {
                isBallShot = false;
                print("Pelota culmino su trayectoria, jefe estatico");
            }
        }
        else if (bossMoves)
        {
            print("Jefe se mueve");

            if (ball.position.x <= 113.0f)//113 posicion del jefe
            {
                ball.position = new Vector3(ball.position.x + 0.1f,
                                            ball.position.y,
                                            ball.position.z);
            }

            if (ball.position.y <= mouseClickPosition.y)
            {
                ball.position = new Vector3(ball.position.x,
                                            ball.position.y + 0.1f,
                                            ball.position.z);
            }

            if (ball.position.z <= mouseClickPosition.z)
            {
                ball.position = new Vector3(ball.position.x,
                                            ball.position.y,
                                            ball.position.z + 0.1f);
            }

            if (ball.position.x >= 113.0f && 
                ball.position.y >= mouseClickPosition.y && 
                ball.position.z >= mouseClickPosition.z)
            {
                isBallShot = false;
                print("Pelota culmino su trayectoria, jefe movil");
            }
        }
    }

    /// <summary>
    /// Mueve el jefe para que detenga la pelota
    /// </summary>
    void MoveBoss() {
        if (bossMoves)
        {
            if (boss.position.y <= mouseClickPosition.y)
            {
                boss.position = new Vector3(boss.position.x,
                                            boss.position.y + 0.2f,
                                            boss.position.z);
            }

            if (boss.position.z <= mouseClickPosition.z)
            {
                boss.position = new Vector3(boss.position.x,
                                            boss.position.y,
                                            boss.position.z + 0.2f);
            }
        }
    }
}
