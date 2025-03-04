using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {

    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button CreateNewUserButton;
    public GameObject CreateUser;

    // Use this for initialization
    void Start()
    {
        LoginButton.onClick.AddListener(() =>
        {
           StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));
        });

        CreateNewUserButton.onClick.AddListener(() =>
        {
            CreateUser.SetActive(true);
        });
    }
}
