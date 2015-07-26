using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    /// <summary>
    /// Carga el archivo de sonido indicado
    /// </summary>
    /// <param name="name"></param>
    public void LoadAudioFile(string name) {
        AudioSource audioSource;

        audioSource = Instantiate(Resources.Load(name, typeof(GameObject))) as AudioSource;
    }
}
