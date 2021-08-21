using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int scores = 0;
    public static int highest_pick = 20;

    public Text scoresCheck;

    bool isScoreAdd = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoresCheck.text = scores.ToString() + "/" + highest_pick.ToString();
        scoresCheck.enabled = false;
    }


    void Update()
    {
        if (isScoreAdd == true){
            scoresCheck.enabled = true;
            StartCoroutine("WaitForSec");
        }
    }


    public void AddScore()
    {
        scores += 1;
        isScoreAdd = true;
        scoresCheck.text = scores.ToString() + "/" + highest_pick.ToString();
        
    }


    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        scoresCheck.enabled = false;
        isScoreAdd = false;
    }
}
