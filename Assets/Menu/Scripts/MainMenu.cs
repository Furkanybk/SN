using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip sound;
    public List<Button> Buttons = new List<Button>(); 
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public void PlayGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    } 

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;

        foreach (var item in Buttons)
        {
            item.onClick.AddListener(() => PlaySound());
        } 
    }

    void PlaySound()
    {
        source.PlayOneShot(sound);
    }
}
