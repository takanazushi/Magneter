using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBGMScript : MonoBehaviour
{
    public static ScoreBGMScript ScoreBGMinstance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (ScoreBGMinstance == null)
        {
            ScoreBGMinstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ScoreScene" || SceneManager.GetActiveScene().name == "ScoreRanking")
        {
            this.gameObject.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "EndlessMonde_ScoreScene" || SceneManager.GetActiveScene().name == "EndlessMode_ScoreRanking")
        {
            this.gameObject.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "ALLRankingScene_A" || SceneManager.GetActiveScene().name == "ALLRankingScene_B")
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
