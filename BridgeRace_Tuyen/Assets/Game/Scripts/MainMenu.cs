using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] private GameObject mapPrefab;
    private GameObject curMap;
    
    public GameObject CurMap { get { return curMap; } set { curMap = value; } }
    public void ButtonStart()
    {
        curMap = Instantiate(mapPrefab);
        Close(0);
    }
}
