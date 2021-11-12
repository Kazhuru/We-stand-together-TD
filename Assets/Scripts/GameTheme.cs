using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTheme : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float musicVolume = 1f;    

    private static GameTheme instance;
    public static GameTheme Instance { get { return instance; } }

    private AudioSource audioSource;

    private void Awake()
    {
        int musicThemeCounter = FindObjectsOfType<GameTheme>().Length;
        if (musicThemeCounter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        audioSource.volume = musicVolume;
    }
}
