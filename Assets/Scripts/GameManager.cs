using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singelton
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    #endregion
    public ItemDatabase itemDatabase;
    private void Awake()
    {
        Instance = this;
    }
}
