using UnityEngine;
using System.Collections;

public class CollisionEnemy : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            print("Colision con pelota, debo desaparecer");

            GameObject.Find("Manager").GetComponent<SoundManager>().LoadAudioFile("cry");

            GameObject.Find("Manager").SendMessage("SpawnEnemy");
            GameObject.Find("Manager").SendMessage("BallRetrieved");

            Destroy(this.gameObject);
        }

    }
}
