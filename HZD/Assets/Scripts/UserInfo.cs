using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    string UserName;
    /*
    public string UserID;
    public string UserPassword;
    public string Level;
    public string Collectibles;
    public string Totaltime;
    */

    public void SetInfo(string username)
    {
        UserName = username;
    }

}
