using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject def;
    public GameObject opt;
    public string nextScene;
    public AudioClip move;
    public AudioClip sel;
    public AudioSource scamera;
    public LifeList list;
    public void Start()
    {
        PlayerPrefs.SetInt("1UP", 3);
        PlayerPrefs.SetInt("Score", 0);
        for (int i = 0; i < list.lives.Length; i++)
        {
            list.lives[i] = false;
        }
    }
    public void Quit()
    {
        PlaySel();
        Application.Quit();
    }
    public void Options(bool set)
    {
        PlaySel();
        def.SetActive(set);
        opt.SetActive(!set);
    }
    public void Play()
    {
        
        SceneManager.LoadScene(nextScene);
    }
    public void PlayMove()
    {
        scamera.PlayOneShot(move);
    }
    public void PlaySel()
    {
        scamera.PlayOneShot(sel);
    }
}
