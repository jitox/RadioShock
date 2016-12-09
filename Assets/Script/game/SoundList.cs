using UnityEngine;
using System.Collections;

public class SoundList : MonoBehaviour
{


    public static SoundList instance = null;
    public AudioSource musicSource;
    public AudioClip levelMusic;
    public AudioClip destruccion1;
   
    
   

    void Awake()
    {
        if (instance == null)
            //Si no es null, la setea a esta.

            instance = this;

        //Si la instancia ya existe
        else if (instance != this)
            //Si no es esta la destruye para que esta sea la unica instancia de SoundManager.
            Destroy(gameObject);

        //Se setea SoundManager como "DontDestroyOnLoad" para que la musica se reproduzca de corrido durante intercambio de level.
        DontDestroyOnLoad(gameObject);
    }

    public void playLevelMusic()
    {
        //AudioSource.PlayClipAtPoint(levelMusic, Camera.main.transform.position);
        musicSource.clip = levelMusic;
        musicSource.loop = true;
        musicSource.Play();

    }
  
   
   
    public void playDestruccion1()
    {
        AudioSource.PlayClipAtPoint(destruccion1, Camera.main.transform.position);
    }
    public void stopMusic()
    {
        musicSource.Stop();
    }
  
    
    
  
  

}
