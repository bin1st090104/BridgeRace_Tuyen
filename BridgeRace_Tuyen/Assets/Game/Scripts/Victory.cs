using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : UICanvas
{
    [SerializeField] private GameObject mapPrefab;
    public void ReplayButton()
    {
        GameObject preMap = UIManager.Ins.GetUI<MainMenu>().CurMap;
        Destroy(preMap);
        UIManager.Ins.GetUI<MainMenu>().CurMap = Instantiate(mapPrefab);
        Close(0);
    }
}
