using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public Transform enemyParent;
    public Transform ball;

    float lastEnemyPositionX;
    int enemyCounter;

    void Start() {
        lastEnemyPositionX = enemyParent.FindChild("Enemy2").localPosition.x;
        enemyCounter = 2;
    }

    /// <summary>
    /// Le coloca la pelota al enemigo
    /// </summary>
    void SetBallToEnemy() {

        Vector3 enemyPosition;
        Vector3 newBallPosition;
        string enemyName;

        enemyName = "Enemy" + enemyCounter.ToString();

        enemyPosition = enemyParent.FindChild(enemyName).position;
        newBallPosition = new Vector3 (enemyPosition.x - 1.5f,
                                       -1.7f,
                                       enemyPosition.z);

        ball.position = newBallPosition;
        ball.gameObject.SendMessage("changeDirection");
        ball.gameObject.SendMessage("resume");

        GameObject.Find(enemyName).SendMessage("ResetBehaviour");
    }

    /// <summary>
    /// Hace que el nuevo enemigo nazca
    /// </summary>
    void SpawnEnemy() {
        GameObject newEnemy;

        newEnemy = Instantiate(Resources.Load("Enemy", typeof(GameObject))) as GameObject;
        lastEnemyPositionX = lastEnemyPositionX + PositionGenerator();
        enemyCounter += 1;

        newEnemy.name = "Enemy"+enemyCounter.ToString();
        
        newEnemy.transform.parent = enemyParent;
        newEnemy.transform.localPosition = new Vector3 (lastEnemyPositionX,
                                                        -1.0f,
                                                        0.0f);
        
    }

    /// <summary>
    /// Genera una posicion en X al random entre 25 y 35 
    /// </summary>
    /// <returns>El resultado de la generacion de caracteres</returns>
    float PositionGenerator() {
        float result;

        result = Random.Range(25.0f, 35.0f);

        return result;
    }
}
