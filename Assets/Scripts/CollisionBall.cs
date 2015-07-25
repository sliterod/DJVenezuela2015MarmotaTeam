using UnityEngine;
using System.Collections;

public class CollisionBall : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Limit"))
        {
            print("Saliendo del limite, proxima salida es con el enemigo");
            GameObject.Find("Manager").SendMessage("SetBallLost");
            GameObject.Find("Manager").SendMessage("SetBallToEnemy");
        }
    }
}
