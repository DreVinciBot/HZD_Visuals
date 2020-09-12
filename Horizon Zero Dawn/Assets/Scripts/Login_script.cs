using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login_script : MonoBehaviour
{
    public TMP_Text response;
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;
    public GameObject next_btn;

    // Start is called before the first frame update
    void Start()
    {
        next_btn.SetActive(false);
        LoginButton.onClick.AddListener(() => {
            //StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));

            if (PasswordInput.text == "robot" && UsernameInput.text != null)
            {
                response.text = "Please Wait...";
                StartCoroutine(Main.Instance.Web.RegisterUserID(UsernameInput.text));

                next_btn.SetActive(true);
            }
            else
            {
                response.text = "Wrong Credentials...";
            }

        });
    }

    public void nextButton()
    {
        next_btn.SetActive(true);
    }

}
