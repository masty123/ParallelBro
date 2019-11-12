using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Photon.Pun;
using Photon.Realtime;

public class FacebookInstance : MonoBehaviour
{
    public MenuManager menuManager;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            FacebookLogin();
        }
    }


    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FacebookLogin();
        }
        else
        {
            Debug.Log("Failed to initialize the Facebook SDK");
        }
    }

    private void FacebookLogin()
    {
        if (FB.IsLoggedIn)
        {
            OnFacebookLoggedIn();
        }
        else
        {
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            OnFacebookLoggedIn();
        }
        else
        {
            Debug.LogErrorFormat("Error in Facebook login {0}", result.Error);
        }
    }

    private void OnFacebookLoggedIn()
    {
        // AccessToken class will have session details
        string aToken = AccessToken.CurrentAccessToken.TokenString;
        string facebookId = AccessToken.CurrentAccessToken.UserId;
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Facebook;
        PhotonNetwork.AuthValues.UserId = facebookId; // alternatively set by server
        PhotonNetwork.AuthValues.AddAuthParameter("token", aToken);
        FetchFBProfile();
        menuManager.LoginScreen.SetActive(false);
        menuManager.HandleLobbyScreen();
    }

    private void FetchFBProfile()
    {
        FB.API("/me?fields=first_name,last_name", HttpMethod.GET, FetchProfileCallback, new Dictionary<string, string>() { });
    }

    private void FetchProfileCallback(IGraphResult result)
    {
        Dictionary<string, object> FBUserDetails = (Dictionary<string, object>)result.ResultDictionary;
        Debug.Log("Profile: first name: " + FBUserDetails["first_name"]);
        Debug.Log("Profile: last name: " + FBUserDetails["last_name"]);
        Debug.Log("Profile: id: " + FBUserDetails["id"]);
        UserData.GetInstance().SetUsername(FBUserDetails["first_name"] + " " + FBUserDetails["last_name"]);
        UserData.GetInstance().SetUserId(FBUserDetails["id"].ToString());

        menuManager.UserNameText.text = UserData.GetInstance().GetUsername();
        PhotonNetwork.NickName = UserData.GetInstance().GetUsername();
    }
}
