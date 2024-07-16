using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScript : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();

        if(UnityServices.State == ServicesInitializationState.Initialized)
        {

            AuthenticationService.Instance.SignedIn += OnSignedIn;

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            AuthenticationService.Instance.SignedIn += OnSignedIn;

            if(AuthenticationService.Instance.IsSignedIn)
            {
                string userName = PlayerPrefs.GetString("Username");
                if(userName == "")
                {
                    userName = "Player";
                    PlayerPrefs.SetString("Username", userName);
                }

                SceneManager.LoadSceneAsync("MenuScene");
            }
        }
    }

    private void OnSignedIn()
    {
        Debug.Log($"Player Id: {AuthenticationService.Instance.PlayerId}");
        Debug.Log($"Token: {AuthenticationService.Instance.AccessToken}");

    }

}
