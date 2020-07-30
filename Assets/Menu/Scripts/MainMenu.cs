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
    public List<Sprite> Sprites = new List<Sprite>();
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private bool toggle = true;
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

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ToggleSound()
    {
        toggle = !toggle;

        if (toggle)
        {
            Buttons[4].transform.GetChild(0).GetComponent<Image>().sprite = Sprites[0];
            AudioListener.volume = 1f;
        }

        else
        {
            Buttons[4].transform.GetChild(0).GetComponent<Image>().sprite = Sprites[1];
            AudioListener.volume = 0f;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    } 
}
