using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RandomScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0,5);

        print(num);



    }

}
