using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int highScore = 0;

    public int maxLivesLeft = 3;
    private void Awake()
    {
        GameObject[] gameManager = GameObject.FindGameObjectsWithTag("gameManager");

        if(gameManager.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
