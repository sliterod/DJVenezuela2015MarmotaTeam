using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    public Transform target;
    bool levelHasEnded;
    bool canRotate;
    bool canChangePosition;

	// Update is called once per frame
	void Update () {

        if (!levelHasEnded)
        {
            transform.position = new Vector3(target.position.x + 7.0f,
                                             transform.position.y,
                                             transform.position.z);
        }

        if (canRotate) {
            ChangeCameraRotation();
        }

        if (canChangePosition) {
            ChangeCameraPosition();
        }
	}

    /// <summary>
    /// Indica que se ha terminado el nivel
    /// </summary>
    void SetLevelEnd() {
        levelHasEnded = true;
        canRotate = true;
        canChangePosition = true;
    }

    /// <summary>
    /// Cambia la posicion de la camara una vez se ha alcanzado el final del nivel
    /// </summary>
    void ChangeCameraPosition() {

        Transform camera;

        camera = this.transform;

        if (camera.position.z - 0.01f >= 0.0f)
        {
            camera.position = new Vector3(96.0f,//camera.position.x,
                                          camera.position.y,
                                          camera.position.z - 0.01f);
        }
        else if (camera.position.z - 0.01f < 0.0f)
        {
            camera.position = new Vector3(96.0f,//camera.position.x,
                                          camera.position.y,
                                          0.0f);

            canChangePosition = false;
        }

    }

    /// <summary>
    /// Cambia la rotacion de la camara una vez se ha alcanzado el final del nivel
    /// </summary>
    void ChangeCameraRotation()
    {
        Transform camera;
        
        camera = this.transform;
       


        if (camera.rotation.y + 0.05f <= 0.75f)
        {
            camera.rotation = new Quaternion(camera.rotation.x,
                                             camera.rotation.y + 0.05f,
                                             camera.rotation.z,
                                             camera.rotation.w);
        }
        else if (camera.rotation.y + 0.05f > 0.75f)
        {
            canRotate = false;
        }

    }
}
