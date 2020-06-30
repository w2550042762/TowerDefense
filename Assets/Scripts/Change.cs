using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
    public void OnHomeButtonDown()
    {
        SceneManager.LoadScene(0);
    }
    public void OnGameButtonDown()
    {
        SceneManager.LoadScene(1);
    }
    

    public GameObject button;
    public Sprite image0,image1;
    private AudioSource audio;//定义声音组件
    public AudioClip music;//放置音乐  
    
    void Start()
    {
        
        audio = this.GetComponent<AudioSource>();//得到声音组件
    }

    public void Music()
    {
        audio.clip = music;
        if (audio.isPlaying == false)
        {
            button.GetComponent<Image>().sprite = image1;
            audio.Play();
        }
        else
        {
            button.GetComponent<Image>().sprite = image0;
            audio.Stop();
        }
    }
}
