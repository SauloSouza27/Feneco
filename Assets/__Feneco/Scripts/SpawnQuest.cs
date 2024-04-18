using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuest : MonoBehaviour
{

    private void Start()
    {
        SetQuestActive();
    }
    public void SetQuestActive()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
