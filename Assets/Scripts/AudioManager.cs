using UnityEngine;

[System.Serializable]
public class Sound //questa classe serve per associare una clip audio a un nome un volume e un pitch
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] // Crea uno slider nell'inspector
    public float volume = 0.7f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Con static, creiamo un riferimento statico accessibile da ovunque, senza bisogno di trascinare la reference, quindi vive fuori da qualunque scena

    //questi sono array che conterranno i nostri suoni.
    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake() //è una funzione notevole di unity, come start, ma le cose al suo interno iniziano ANCORA PRIMA di start. Ci serve per il dontdestroyonload
    {
        // CASO 1: Se instance esiste già r non è vuota
        if (instance != null)
        {
            Destroy(gameObject); // Io sono un duplicato, mi distruggo
            return;
        }

        // CASO 2: Io sono il prescelto (o il precedente è morto/nullo)
        instance = this;
        DontDestroyOnLoad(gameObject); //questo ricaricherà l'oggetto nella scena successiva. 
        // Ovviamente funziona anche se torno al menu dove di default c'è un audiomanager.
        // La prima parte dell'if serve a evitare che tornando al menu si creino due audiomanager.
    }


    public void PlayMusic(string name)
    {
        foreach (Sound s in musicSounds)
        {
            if (s.name == name)
            {
                musicSource.clip = s.clip;
                musicSource.volume = s.volume; // Applichiamo il volume scelto
                musicSource.pitch = s.pitch;
                musicSource.Play();
                return;
            }
        }
        Debug.LogWarning("Musica non trovata: " + name);
    }

    public void PlaySFX(string name)
    {
        foreach (Sound s in sfxSounds)
        {
            if (s.name == name)
            {
                sfxSource.pitch = s.pitch;
                sfxSource.PlayOneShot(s.clip, s.volume); //il playoneshot, a differenza del play, accetta audioclip come parametro e permette il sovrapporsi dei suoni
                return;
            }
        }
        Debug.LogWarning("SFX non trovato: " + name);
    }
}