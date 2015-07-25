using UnityEngine;
using System.Collections;

public class CollisionCharacter : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Enemy")) {
            print("Colision con enemigo, restar vida");
        }

        if (other.name == "Ball")
        {
            print("Pelota recuperada");
            GameObject.Find("Manager").SendMessage("BallRetrieved");
        }

        if (other.name.Contains("End")) {
            print("Final del nivel, cambiar camara");
            GameObject.Find("Manager").SendMessage("SetLevelEnd");
        }
    }
}
