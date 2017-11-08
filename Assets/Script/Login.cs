using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms.GameCenter;

public class Login : MonoBehaviour {

    // Use this for initialization
    void Start() {
#if UNITY_ANDROID
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        }
        );


#endif
#if UNITY_IOS
               
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        }
        );
#endif
    }

    // Update is called once per frame
    void Update () {
	
	}
}
