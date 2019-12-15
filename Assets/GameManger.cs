using System;
using System.Collections;
using System.Collections.Generic;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    public PanelRenderer loginScreen;
    public PanelRenderer registerScreen;

    void OnEnable()
    {
        loginScreen.postUxmlReload =  BindLoginScreen;
        registerScreen.postUxmlReload = BindRegisterScreen;
    }
    void Update()
    {
        
    }

    void Start()
    {
        GoToLoginMenu();
    }

    private IEnumerable<Object> BindLoginScreen()
    {
        var root = loginScreen.visualTree;

        var usernameTextField = root.Q<TextField>("username");
        var passwordTextField = root.Q<TextField>("password");

        var loginButton = root.Q<Button>("login");
        if (loginButton != null)
        {
            loginButton.clickable.clicked += () =>
            {
                Debug.Log("Login clicked!\n");
                Debug.Log("Username: " + usernameTextField.text);
                Debug.Log("Password: " + passwordTextField.text);

                
            };
        }

        var registerButton = root.Q<Button>("register");
        if (registerButton != null)
        {
            registerButton.clickable.clicked += () =>
            {
                Debug.Log("Register clicked!\n");
                Debug.Log("Username: " + usernameTextField.text);
                Debug.Log("Password: " + passwordTextField.text);

                GoToRegisterMenu();


            };
        }


        return null;
    }

    private IEnumerable<Object> BindRegisterScreen()
    {
        var root = registerScreen.visualTree;

        var usernameTextField = root.Q<TextField>("username");
        var passwordTextField = root.Q<TextField>("password");
        var passwordRepeatTextField = root.Q<TextField>("password-repeat");
        var errorInfoLabel = root.Q<Label>("error-info");

        var registerButton = root.Q<Button>("register");
        if (registerButton != null)
        {
            registerButton.clickable.clicked += () =>
            {
                if (passwordTextField.text == passwordRepeatTextField.text)
                {
                    errorInfoLabel.text = "";
                    Debug.Log("Register clicked!\n");
                    Debug.Log("Username: " + usernameTextField.text);
                    Debug.Log("Password: " + passwordTextField.text);
                    Debug.Log("Password repeat: " + passwordRepeatTextField.text);
                }
                else
                {
                    errorInfoLabel.text = "Passwords must match.";
                }

            };
        }

        return null;
    }

    void SetScreenEnableState(PanelRenderer screen, bool state)
    {
        if (state)
        {
            screen.visualTree.style.display = DisplayStyle.Flex;
            screen.enabled = true;
            screen.gameObject.GetComponent<UIElementsEventSystem>().enabled = true;
        }
        else
        {
            screen.visualTree.style.display = DisplayStyle.None;
            screen.enabled = false;
            screen.gameObject.GetComponent<UIElementsEventSystem>().enabled = false;
        }
    }

    private void GoToLoginMenu()
    {
        SetScreenEnableState(loginScreen, true);
        SetScreenEnableState(registerScreen, false);
    }
    private void GoToRegisterMenu()
    {
        SetScreenEnableState(loginScreen, false);
        SetScreenEnableState(registerScreen, true);
    }



}
