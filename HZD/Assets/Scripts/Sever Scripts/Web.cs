using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using TMPro;

public class Web : MonoBehaviour
{
  
    public TMP_Text loginResponse;
    public bool state = false;
    public static string username_input;

    private string ngrok = "https://ba9eae6166ff.ngrok.io";

    void Start()
    {
        //StartCoroutine(GetRequest("http://localhost/ARNavigationStudy2020/GetID.php"));
        //StartCoroutine(GetRequest("https://example-php-files.s3.us-east-2.amazonaws.com/GetID.php"));
        //StartCoroutine(GetUsers());
        //StartCoroutine(Login("testuser", "123456"));
        //StartCoroutine(RegisterUser("testuser3", "tufts"));
        //StartCoroutine(RegisterUserID("testuser4"));
        //StartCoroutine(RegisterUserLevel(4));
        //StartCoroutine(RegisterUserTime1("4"));
        //StartCoroutine(RegisterUserCollected1(5));
        //StartCoroutine(RegisterUserCollected2(5));
        //StartCoroutine(RegisterUserPointContact1("test"));
        //StartCoroutine(RegisterUserPointContact2("test2"));
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

    public IEnumerator GetUsers()
    {
        //string uri = "http://24.60.202.6:80/ARNavigationStudy2020/GetUsers.php";
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

    //This function is for the AR Navigational Study
    public IEnumerator RegisterUserID(string username)
    {
        username_input = username;
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        //form.AddField("loginPass", password);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserID.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserID.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            //loginResponse.text = www.downloadHandler.text;
            
            if (www.isDone)
            {
                loginResponse.text = www.downloadHandler.text;
                

                //SceneManager.LoadScene("menu_scene");
            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Main.Instance.UserInfo.SetInfo(username);

                Debug.Log(www.downloadHandler.text);
                string message = www.downloadHandler.text;
    
                loginResponse.text = message;

                if (message == "created user")
                {
                    loginResponse.text = "Login Successful, Click next to continue.";
                    //SceneManager.LoadScene("menu_scene");                  
                }
                else
                {
                    loginResponse.text = message;
                }
            }
        }
    }

    public IEnumerator RegisterUserLevel(string level)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginLevel", level);

        print("u/n : " + username_input + " recieved: " + level);
     
        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterLevel.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterLevel.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserPointContact1(string contacts)
    {
       
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginPointContact_1", contacts);

        print("u/n : " + username_input + " recieved: " + contacts);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterPointContact1.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterLevel.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserPointContact2(string constacts)
    {
      
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginPointContact_2", constacts);

        print("u/n : " + username_input + " recieved: " + constacts);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterPointContact2.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterLevel.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserTime1(string time1)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginTime1", time1);

        print("u/n : " + username_input + " recieved: " + time1);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserTime1.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserTime1.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserTime2(string time2)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginTime2", time2);

        print("u/n : " + username_input + " recieved: " + time2);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserTime2.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserTime1.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserSequence1(string sequence1)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginSequence1", sequence1);

        print("u/n : " + username_input + " recieved: " + sequence1);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserSequence1.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserTime1.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserSequence2(string sequence2)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginSequence2", sequence2);

        print("u/n : " + username_input + " recieved: " + sequence2);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserSequence2.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserTime1.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserCollected1(int items1)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginCollected1", items1);

        print("u/n : " + username_input + " recieved: " + items1);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserCollected1.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserTime1.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }

    public IEnumerator RegisterUserCollected2(int items2)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username_input);
        form.AddField("loginCollected2", items2);

        print("u/n : " + username_input + " recieved: " + items2);

        //trying url with ip address with portforwarding
        string uri = ngrok + "/ARNavigationStudy2020/RegisterUserCollected2.php";

        // uri for localhost
        //string uri = "http://localhost/ARNavigationStudy2020/RegisterUserTime1.php";

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);            
            }
        }
    }
}
