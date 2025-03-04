using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour {

    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button SubmitButton;
    public GameObject DoNotMatch;
    public Web web;

    // Use this for initialization
    void Start()
    {
        SubmitButton.onClick.AddListener(() =>
        {
            if (PasswordInput.text != ConfirmPasswordInput.text)
            {
                Debug.Log("Passwords do not match!");
                StartCoroutine(Wait());
            }
            else
            {
                StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));
                //StartCoroutine(Wait4());
                //if (web.alreadyExists == true)
                //{
                //    //Debug.Log("HELLO");
                //    StartCoroutine(Wait3());
                //}
                StartCoroutine(Wait2());
                //else
                //{
                //    StartCoroutine(Wait2());
                //}
            }
        });
    }

    IEnumerator Wait()
    {
        DoNotMatch.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        DoNotMatch.SetActive(false);
    }

    IEnumerator Wait2()
    {
        //Debug.Log("WAIT2");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    IEnumerator Wait3()
    {
        //Debug.Log("WAIT3");
        yield return new WaitForSeconds(1f);
        web.alreadyExists = false;
    }

    IEnumerator Wait4()
    {
        //Debug.Log("WAIT4");
        yield return new WaitForSeconds(3f);
    }
}
