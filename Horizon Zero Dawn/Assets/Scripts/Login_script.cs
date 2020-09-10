using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login_script : MonoBehaviour
{
    public TMP_Text response;
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            //StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));

            if (PasswordInput.text == "robot")
            {
                StartCoroutine(Main.Instance.Web.RegisterUserID(UsernameInput.text));
            }
            else
            {
                response.text = "Wrong Credentials...";
            }

        });
    }
}
