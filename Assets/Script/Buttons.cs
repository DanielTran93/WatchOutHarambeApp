using UnityEngine;
using System.Collections;
using System.IO;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {
    bool isApp;
    public GameObject failLogin;
    public Text soon;
    public string leaderboard;
    float reportTime;
    int reportTimeInt;
    int report;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame

	void Update () {
        
        reportTime = PlayerPrefs.GetFloat("SurvivedTime");
        reportTimeInt = Mathf.RoundToInt(reportTime * 1000);
        PlayerPrefs.SetInt("reportScore", reportTimeInt);
        report = reportTimeInt;

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Leaderboard()
    {
#if UNITY_ANDROID
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
        }
        else
            StartCoroutine(Fail());

#endif
#if UNITY_IOS
        StartCoroutine(Soon());
#endif
    }

    public void Leaderboard2()
    {
#if UNITY_ANDROID
        if (Social.localUser.authenticated)
        {

            Social.ReportScore(report, leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");
                    ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);

                }
                else
                {

                }

            });
            
        }
        else
            StartCoroutine(Fail());
#endif

#if UNITY_IOS
        StartCoroutine(Soon());
#endif

    }

    public void Achievements()
    {
#if UNITY_ANDROID
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
        }
        else
            StartCoroutine(Fail());

#endif
#if UNITY_IOS
        StartCoroutine(Soon());
#endif

    }

    IEnumerator Soon()
    {
        soon.enabled = true;
        yield return new WaitForSeconds(1.5f);
        soon.enabled = false;
    }

    IEnumerator Fail()
    {
        failLogin.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        failLogin.SetActive(false);
    }

    // ---------------------------------open facebook app or browser if no app
    public void Facebook()
    {
        Application.OpenURL("fb://page/649904945175469");
        StartCoroutine(CheckApp());
        isApp = false;
        Social.ReportProgress("CgkIpcOBy4UdEAIQBg", 100.0f, (bool success) => { });
    }

    void OnApplicationPause()
    {
        isApp = true;
    }

    IEnumerator CheckApp()
    {
        // Wait for a time
        yield return new WaitForSeconds(1.5f);

        // If app hasn't launched, default to opening in browser
        if (!isApp)
        {
            Application.OpenURL("https://www.facebook.com/649904945175469");
        }
    }
    //-------------------------------------------

}
