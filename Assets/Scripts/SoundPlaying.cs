using UnityEngine;
using System.Collections;

public class SoundPlaying : MonoBehaviour {

    bool isPlaying = true;

	// Update is called once per frame
	void Update () {
        if (!this.GetComponent<AudioSource>().isPlaying) {
            isPlaying = false;
        }

        if (!isPlaying) {
            Destroy(this.gameObject);
        }
	}
}
