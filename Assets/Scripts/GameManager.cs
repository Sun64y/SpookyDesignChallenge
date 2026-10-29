using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    #region SINGLETON
    public static GameManager single;
    private void Awake()
    {
        if (single == null) { single = this; } else { Destroy(gameObject); }
    }
    #endregion


    void Start()
    {

    }

    void Update()
    {

    }

    
}
