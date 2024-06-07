using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuest : MonoBehaviour
{
    private GameObject[] children = null;

    private void Start()
    {
        if(transform.childCount != 0)
        {
            children = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i).gameObject;
                children[i].SetActive(false);
            }
        }
    }

    public void SetQuestActive()
    {
        if(children != null)
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetActive(true);
            }
        }
    }
}
