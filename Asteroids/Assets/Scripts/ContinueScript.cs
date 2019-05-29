using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
{
    public Text tempo;
    int i;
    public bool isBossContinue;
    void Start()
    {
        i=10;
        InvokeRepeating("CountDownTime", 1f, 1f);
    }

    void CountDownTime()
    {
        i--;
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire"))
        {
            RestartGame();
        }
        tempo.text = i+"";
        if (i==0)
        {
            if(isBossContinue){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 6);
            }else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }


}
