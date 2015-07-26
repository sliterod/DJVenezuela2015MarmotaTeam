using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    //Boolean de comportamiento
    bool mustJump;
    bool mustRun;
    bool mustDash;
    bool enemyHasBall;

    //Variables de salto
    bool isJumping;
    bool isFalling;
    float jumpSpeed;

    //Pelota
    Transform ball;

    //Animaciones
    Animation enemy;

	// Use this for initialization
	void Start () {
        jumpSpeed = 0.1f;
        enemy = this.GetComponent<Animation>();

        ChooseBehaviour();
    }
	
	// Update is called once per frame
	void Update () {
        if (mustRun) {
            Run();
        }

        if (mustJump) {
            Jump();
        }

        if (mustDash) {
            Dash();
        }
	}

    /// <summary>
    /// Comportamiento de carrera
    /// </summary>
    void Run() {
        print("Mi comportamiento es correr" + this.name);
        
        //Animacion de carrera
        EnemyRunAnimation();
    }

    /// <summary>
    /// Comportamiento de salto del enemigo
    /// </summary>
    void Jump() {
        print("Mi comportamiento es saltar" + this.name);
        //Animación de salto

        //Rutina de salto
        if (isJumping && !isFalling && this.transform.position.y < 3.0f)
        {
            Vector3 newPositionCharacter;
            

            newPositionCharacter = new Vector3(this.transform.position.x,
                                               this.transform.position.y + jumpSpeed,
                                               this.transform.position.z);

            this.transform.position = newPositionCharacter;

            if (enemyHasBall)
            {
                Vector3 newPositionBall;
                newPositionBall = new Vector3(ball.position.x,
                                              ball.position.y + jumpSpeed,
                                              ball.position.z);
                ball.position = newPositionBall;
            }

        }
        else if (isJumping && !isFalling && this.transform.position.y >= 3.0f)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            Vector3 newPosition;
            

            if (this.transform.position.y - jumpSpeed > -1.0f)
            {
                newPosition = new Vector3(this.transform.position.x,
                                          this.transform.position.y - jumpSpeed,
                                          this.transform.position.z);

                this.transform.position = newPosition;

                if (enemyHasBall) {

                    Vector3 newPositionBall;
                    newPositionBall = new Vector3(ball.position.x,
                                              ball.position.y - jumpSpeed,
                                              ball.position.z);

                    ball.position = newPositionBall;
                }
                

            }
            else if (this.transform.position.y - jumpSpeed <= -1.0f)
            {
                isJumping = true;
                isFalling = false;

                this.transform.position = new Vector3(this.transform.position.x,
                                                      -1.0f,
                                                      this.transform.position.z);

                if (enemyHasBall)
                {
                    ball.position = new Vector3(ball.position.x,
                                                -1.7f,
                                                ball.position.z);
                }
            }

        }
    }

    /// <summary>
    /// Comportamiento de barrida del enemigo
    /// </summary>
    void Dash() {
        print("Mi comportamiento es deslizarme");
        //Animacion de dash


        //Collider de dash
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = true;
    }

    /// <summary>
    /// Cambia el comportamiento de forma automatica si se recibe el balon
    /// </summary>
    void ResetBehaviour() {
        
        //Cambia a modo de carrera
        mustRun = true;
        mustDash = false;
        mustJump = false;
        enemyHasBall = true;

        //Pelota
        ball = GameObject.Find("Ball").transform;

        //Posicion
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                   -1.0f,
                                                   0.0f);

        //Colliders
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<CapsuleCollider>().enabled = false;
    }

    /// <summary>
    /// Escoge el comportamiento del enemigo
    /// </summary>
    void ChooseBehaviour() {
        float result;

        result = Random.Range(0.0f, 1.0f);

        if (result >= 0.0 && result < 0.33f)//Run
        {
            mustRun = true;
        }
        else if (result >= 0.33 && result < 0.67f)//Jump
        {
            mustJump = true;
            isJumping = true;
        }
        else if (result >= 0.67 && result <= 1.0f)//Dash
        {
            mustDash = true;
        }
    }

    /*
        Enemy
    */
    public void EnemyJumpAnimation()
    {
        enemy.CrossFade("jump");
    }

    public void EnemyRunAnimation()
    {
        enemy.CrossFade("run");
    }

    public void EnemySlideAnimation()
    {
        enemy.CrossFade("slide");
    }

    public void EneymHurtAnimation()
    {
        enemy.CrossFade("hurt");
    }
}
