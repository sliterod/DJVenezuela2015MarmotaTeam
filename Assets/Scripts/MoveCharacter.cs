﻿using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour {

    //Personaje
    public Transform character;
    public Transform ball;

    //Valores
    public float speed;
    public float jumpSpeed;
    public float ballSpeed;

    //Booleans
    bool isJumping;
    bool isFalling;
    bool isDashing;
    bool ballShot;
    bool isBallFalling;
    bool isBallLost;
    bool isLevelEnd;
    bool isGameOver;
    bool isPausedGame;

    //Posiciones
    float crosshairPositionX;
    float crosshairPositionY;

    //Camara
    public GameObject perspectiveCamera;

    //Animations
    Animations animations;

    // Use this for initialization
    void Awake () {
        animations = this.GetComponent<Animations>();

        Time.timeScale = 1.0f;
	}

    void Start() {
        RunAnimation();
    }

    // Update is called once per frame
    void Update() {

        if (!isLevelEnd && !isPausedGame) {
            ChangeCharacterPosition();
            ChangeBallPosition();

            Shoot();
            Dash();
            Jump();
        }
    }

    /// <summary>
    /// Animacion de correr del personaje
    /// </summary>
    void RunAnimation() {
        animations.CharacterRunAnimation();
    }

    /// <summary>
    /// Animacion de salto
    /// </summary>
    void JumpUpAnimation() {
        animations.CharacterJumpUpAnimation();
    }

    /// <summary>
    /// Animacion de espera
    /// </summary>
    void IdleAnimation() {
        animations.CharacterIdleAnimation();
    }

    /// <summary>
    /// Mueve el personaje de posicion
    /// </summary>
    void ChangeCharacterPosition()
    {
        Vector3 newPosition;

        if (!isGameOver)
        {
            newPosition = new Vector3(character.position.x + speed,
                                      character.position.y,
                                      character.position.z);

            character.position = newPosition;
        }
    }

    /// <summary>
    /// Mueve la pelota de posicion
    /// </summary>
    void ChangeBallPosition() {
        Vector3 newPosition;

        if (!isBallLost && !isGameOver)
        {
            newPosition = new Vector3(ball.position.x + speed,
                                       ball.position.y,
                                       ball.position.z);

            ball.position = newPosition;
        }

    }

    /// <summary>
    /// Dispara la pelota
    /// </summary>
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !ballShot && !isBallLost && !isJumping)
        {

            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            crosshairPositionX = mousePos.x;
            crosshairPositionY = mousePos.y;

            animations.CharacterShotAnimation(false, false);
            this.GetComponent<SoundManager>().LoadAudioFile("kick");

            ballShot = true;
            ball.GetComponent<SphereCollider>().enabled = true;
            ball.gameObject.SendMessage("stop");
            
        }

        if (ballShot)
        {

            //Vector3 newPosition;

            if (ball.position.x < (crosshairPositionX + 100.0f))
            {
                float yPos;

                yPos = Mathf.Abs(crosshairPositionY / 10.0f);

                //if ((ball.position.y < crosshairPositionY) && !isBallFalling)
                //{
                    ball.position = new Vector3(ball.position.x + ballSpeed,
                                                ball.position.y + yPos,
                                                ball.position.z);

                    
                //}
                /*else if ((ball.position.y >= crosshairPositionY) && (ball.position.x >= crosshairPositionX))
                {
                    if (!isBallFalling)
                        isBallFalling = true;
                }

                if (isBallFalling) {

                    if (ball.position.y > -2.5f)
                    {
                        ball.position = new Vector3(ball.position.x + ballSpeed,
                                                    ball.position.y - 0.05f,
                                                    ball.position.z);
                    }
                    else if (ball.position.y <= -2.5f) {
                        isBallFalling = false;
                        ballShot= false;
                    }
                }*/

            }
            else if (ball.position.x < (crosshairPositionX + 100.0f))
            {
                ballShot = false;
            }
        }
    }

    /// <summary>
    /// El personaje se desliza por el suelo
    /// </summary>
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing && isBallLost) {
            isDashing = true;
            this.GetComponent<SoundManager>().LoadAudioFile("dash");

            animations.CharacterDashAnimation();

            ActivateDashingCollider(true);
            StartCoroutine(ReturnToStanding());
        }
    }

    void ActivateDashingCollider(bool state) {
        if (state)
        {
            character.GetComponent<BoxCollider>().enabled = false;
            character.GetComponent<CapsuleCollider>().enabled = true;
        }
        else {
            character.GetComponent<BoxCollider>().enabled = true;
            character.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    IEnumerator ReturnToStanding() {
        yield return new WaitForSeconds (2.0f);

        ActivateDashingCollider(false);
        isDashing = false;
    }

    /// <summary>
    /// Indica que la pelota se ha perdido
    /// </summary>
    void SetBallLost() {
        ballShot = false;
        isBallLost = true;
    }

    /// <summary>
    /// Indica que la pelota ha sido recuperada
    /// </summary>
    void BallRetrieved() {

        Vector3 newPosition;

        newPosition = new Vector3(character.position.x + 1.5f,
                                  -1.7f,
                                  ball.position.z);

        ball.position = newPosition;
        ball.gameObject.SendMessage("changeDirection");
        ball.gameObject.SendMessage("resume");

        isBallLost = false;
        ballShot = false;

        ball.GetComponent<SphereCollider>().enabled = false;
    }

    /// <summary>
    /// Hace saltar al personaje
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isJumping && character.position.y == -2.0f)
        {
            isJumping = true;
            this.GetComponent<SoundManager>().LoadAudioFile("jump");
            JumpUpAnimation();
        }

        if (isJumping && !isFalling && character.position.y < 3.0f)
        {
            Vector3 newPositionCharacter;
            
            newPositionCharacter = new Vector3(character.position.x,
                                               character.position.y + jumpSpeed,
                                               character.position.z);
            
            character.position = newPositionCharacter;
            

            if (!isBallLost) {
                Vector3 newPositionBall;
                newPositionBall = new Vector3(ball.position.x,
                                          ball.position.y + jumpSpeed,
                                          ball.position.z);

                ball.position = newPositionBall;
            }

        }
        else if (isJumping && !isFalling && character.position.y >= 3.0f)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            Vector3 newPosition;
            

            if (character.position.y - jumpSpeed > -2.0f)
            {
                newPosition = new Vector3(character.position.x,
                                          character.position.y - jumpSpeed,
                                          character.position.z);
                
                character.position = newPosition;
                

                if (!isBallLost) {

                    Vector3 newPositionBall;

                    newPositionBall = new Vector3(ball.position.x,
                                              ball.position.y - jumpSpeed,
                                              ball.position.z);

                    ball.position = newPositionBall;
                }
                
            }
            else if (character.position.y - jumpSpeed <= -2.0f)
            {
                isJumping = false;
                isFalling = false;

                character.position = new Vector3(character.position.x,
                                                 -2.0f,
                                                 character.position.z);

                if (!isBallLost)
                {
                    ball.position = new Vector3(ball.position.x,
                                                -1.7f,
                                                ball.position.z);
                }
            }

        }
    }

    /// <summary>
    /// Indica que es el final del nivel para luego ejecutar el cambio de cámara
    /// </summary>
    void SetLevelEnd() {
        isLevelEnd = true;

        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
        perspectiveCamera.SetActive(true);


        GameObject.Find("CloudsNearArray").SendMessage("StopClouds");
        GameObject.Find("CloudsFarArray").SendMessage("StopClouds");

        GameObject.Find("Ball").SendMessage("stop");
        GameObject.Find("Enemies").SetActive(false);

        IdleAnimation();

        StartCoroutine(ShowFinalSequencePanel());
    }

    /// <summary>
    /// Inicia un timer para activar la ventana de la secuencia final
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowFinalSequencePanel() {
        yield return new WaitForSeconds(1.0f);

        this.SendMessage("ActivateSequencePanel");
        this.SendMessage("DeactivateCrossHair");
    }

    /// <summary>
    /// Coloca el juego en modo game over
    /// </summary>
    void SetGameOver() {
        isGameOver = true;
        animations.CharacterHurtAnimation();
        this.GetComponent<GamePanels>().ActivateDefeatPanel();
    }

    /// <summary>
    /// Coloca el juego en pausa
    /// </summary>
    /// <param name="state">Estado del juego</param>
    void SetPause(bool state) {
        isPausedGame = state;
    }
}
