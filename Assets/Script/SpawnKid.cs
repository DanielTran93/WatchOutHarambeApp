using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class SpawnKid : MonoBehaviour {

    //Gameobjects
    public GameObject kid;
    public GameObject kid2;
    public GameObject gorilla;
    public GameObject restartButton;
    public GameObject bestTimeImage;
    public GameObject survivedTimeImage;
    public GameObject powerUp;
    public GameObject powerUpImage;
    public Canvas canvas;
    
    public GameObject title;
    public GameObject hiddenPic;
    //Text
    public Text survivedText;
    public Text bestTime;
    public Text powerUpText;
    //Timers
    public float survivedTime;
    public float previousBestTime;
    //power up when picking up banana
    public float powerUpTimer;
    public float humanGorilla;

    //the list of kids to spawn
    List<GameObject> kidType = new List<GameObject>();
    //animator on Gorilla object
    public Animator animator;
    //TAP TO MOVE
    bool startGame;
    public GameObject tapToMoveImage;

    //ad stuff
    private BannerView bannerView;
    private InterstitialAd interstitial;
    // Use this for initialization

    void Start () {
        //ads the gameobjects kid and kid2 to the kidType List
        kidType.Add(kid);
        kidType.Add(kid2);
        //bool for alive for timing purposes
        //obtains the previous best time
        previousBestTime = PlayerPrefs.GetFloat("SurvivedTime");
        //sets best time text based on the previous best time.
        bestTime.text = "" + previousBestTime;
        humanGorilla = 5;
        startGame = false;
        RequestInterstitial();
        RequestBanner();
        //if (Gorilla.tag == "Gorilla" || Gorilla.tag == "HumanGorilla")
        //{
        //    StartCoroutine(Spawn());
        //    startGame = false;
        //}
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount >= 1 & gorilla.tag == "Player")
        {
            startGame = true;
            if (startGame == true)
            {
                
                gorilla.tag = "Gorilla";
                StartCoroutine(Spawn());
                startGame = false;
                tapToMoveImage.SetActive(false);


            }
        }

        if (powerUpTimer >= 20 && gorilla.tag == "Gorilla")
        {
            SpawnPowerUp();
        }

            //When Gorilla is dead
            if (gorilla.tag == "DeadGorilla")
        {
            //method that saves the best time when Gorilla dies.
            SaveBestTime();
            Social.ReportProgress("CgkIpcOBy4UdEAIQAQ", 100.0f, (bool success) => { });
            bestTimeImage.SetActive(true);
            //title.SetActive(true);
            //hashtag.SetActive(true);
            StartCoroutine(ShowImage());
            animator.SetInteger("State", 3);

        }

    }

    void FixedUpdate()
    {
        //checks if Gorilla object tag is Gorilla/alive
        if (gorilla.tag == "Gorilla" || gorilla.tag == "HumanGorilla")
        {
            //if he is then the time keeps going
            survivedTime += Time.deltaTime;
            survivedText.text = Mathf.RoundToInt(survivedTime) + " Seconds";
            powerUpTimer += Time.deltaTime;
            
        }

        if (gorilla.tag == "HumanGorilla")
        {
            //Gorilla.GetComponent<Collider2D>().isTrigger = false;
            humanGorilla -= Time.deltaTime;
            powerUpText.text = "Invulnerable\n" + Mathf.RoundToInt(humanGorilla);
            powerUpImage.SetActive(true);
            Social.ReportProgress("CgkIpcOBy4UdEAIQAw", 100.0f, (bool success) => { });

            if (humanGorilla <= 0 )
            {
                gorilla.GetComponent<Collider2D>().isTrigger = true;
                gorilla.tag = "Gorilla";
                powerUpImage.SetActive(false);
                humanGorilla = 5;
                powerUpText.text = "Invulnerable\n" + "0";

            }
        }

    }


    IEnumerator Spawn ()
    {
        while (gorilla.tag == "Gorilla" || gorilla.tag == "HumanGorilla")
        {
            
            if (survivedTime <= 3)
            {
                yield return new WaitForSeconds(3f);
                
            }
            else
                if (survivedTime > 3 & survivedTime <= 10)
            {
                yield return new WaitForSeconds(2.0f);
                
            }
            else 
                if (survivedTime > 10 & survivedTime <= 15)
            {
                yield return new WaitForSeconds(1.5f);
                
            }
            else
                if (survivedTime > 15 & survivedTime <= 20)
            {
                yield return new WaitForSeconds(1.2f);
                
            }
            else
                if (survivedTime > 20 & survivedTime <= 30)
            {
                yield return new WaitForSeconds(1.0f);
                
            }
            else
                if (survivedTime > 30 & survivedTime <= 60)
            {

                yield return new WaitForSeconds(0.8f);
                Social.ReportProgress("CgkIpcOBy4UdEAIQAg", 100.0f, (bool success) => { });

            }
            if (survivedTime > 60 & survivedTime <= 120)
            {
                Social.ReportProgress("CgkIpcOBy4UdEAIQBA", 100.0f, (bool success) => { });
                yield return new WaitForSeconds(0.5f);

            }
            if (survivedTime > 120 & survivedTime <= 250)
            {
                Social.ReportProgress("CgkIpcOBy4UdEAIQCw", 100.0f, (bool success) => { });
                yield return new WaitForSeconds(0.3f);

            }
            if (survivedTime > 250)
            {
                Social.ReportProgress("CgkIpcOBy4UdEAIQDA", 100.0f, (bool success) => { });
                yield return new WaitForSeconds(0.0f);

            }

            Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(0, Screen.width), Screen.height));
            Vector2 spawnPosition2 = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(0, Screen.width), Screen.height));
            //Vector2 spawnPosition = new Vector2(Random.Range(-Screen.width, Screen.width), transform.position.y);
            Quaternion rotation = Quaternion.identity;
            GameObject kidSpawn1 = Instantiate(kidType[UnityEngine.Random.Range(0, kidType.Count)], spawnPosition, rotation) as GameObject;
            kidSpawn1.transform.SetParent(canvas.transform);
            GameObject kidSpawn2 = Instantiate(kidType[UnityEngine.Random.Range(0, kidType.Count)], spawnPosition2, rotation) as GameObject;
            kidSpawn2.transform.SetParent(canvas.transform);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
        }
    }

    IEnumerator ShowImage ()
    {
        ShowInterstitial();
        gorilla.tag = "Untagged";
        title.SetActive(true);
        
        yield return new WaitForSeconds(15f);
        title.SetActive(false);
        
        hiddenPic.SetActive(true);
        Social.ReportProgress("CgkIpcOBy4UdEAIQBQ", 100.0f, (bool success) => { });
        yield return new WaitForSeconds(4f);
        hiddenPic.SetActive(false);
        title.SetActive(true);
        
    }
    public void SpawnPowerUp()
    {
        
        
            Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height)));
            Quaternion rotation = Quaternion.identity;
            GameObject clone = (GameObject)Instantiate(powerUp, spawnPosition, rotation);
            Destroy(clone,3f);
            powerUpTimer = 0;
        
    }
    public void SaveBestTime()

    {
        
        // checks if the current survived time is greater than previously saved Best time.
        if (survivedTime > previousBestTime)
        {
            //saves the highest survived time
            PlayerPrefs.SetFloat("SurvivedTime", survivedTime);
            bestTime.text = "" + survivedTime;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("GorillaGame");
    }



    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-5748059298645580/3176985152";
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            string adUnitId = "ca-app-pub-5748059298645580/3176985152";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Register for ad events.
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdLoaded += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()

            .SetBirthday(new DateTime(1992, 1, 1))
            .Build();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
        print("HandleAdClosing event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }

    #endregion

    private void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-5748059298645580/6733737159";
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            string adUnitId = "ca-app-pub-5748059298645580/6733737159";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

    private void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            print("Interstitial is not ready yet.");
        }
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion

}

