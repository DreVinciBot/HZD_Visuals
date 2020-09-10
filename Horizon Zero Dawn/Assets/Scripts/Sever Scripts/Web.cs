using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class Web : MonoBehaviour
{

    public TMP_Text loginResponse;

    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost/ARNavigationStudy2020/GetID.php"));
        //StartCoroutine(GetUsers());
        //StartCoroutine(Login("testuser", "123456"));
        //StartCoroutine(RegisterUser("testuser3", "tufts"));
        //StartCoroutine(RegisterUserID("testuser4"));
  
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator GetUsers()
    {
        string uri = "http://localhost/ARNavigationStudy2020/GetUsers.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        string uri = "http://localhost/ARNavigationStudy2020/Login.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {          
                Debug.Log(www.downloadHandler.text);
                string message = www.downloadHandler.text;

                loginResponse.text = message;

                if (message == "Login Success")
                {                   
                    SceneManager.LoadScene("menu_scene");
                }      
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        string uri = "http://localhost/ARNavigationStudy2020/RegisterUser.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator RegisterUserID(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        //form.AddField("loginPass", password);

        string uri = "http://localhost/ARNavigationStudy2020/RegisterUserID.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string message = www.downloadHandler.text;
           
                if (message == "created user")
                {
                    loginResponse.text = "Login Successful";
                    SceneManager.LoadScene("menu_scene");
                }
                else
                {
                    loginResponse.text = message;
                }
            }
        }
    }
}
