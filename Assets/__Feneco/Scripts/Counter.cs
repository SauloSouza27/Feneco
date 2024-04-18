using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] private bool active = true;

    private float initialTime = 0.0f;

    private TextMeshProUGUI counterText;

    private void Awake()
    {
        counterText = transform.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (Time.timeScale == 0) return;

        if (active)
        {
            initialTime += Time.deltaTime/Time.timeScale;
            ShowCounter(initialTime);
        }
    }
    private void ShowCounter (float time)
    {
        time += 1;

        float minute = Mathf.FloorToInt(time / 60);
        float second = Mathf.FloorToInt(time % 60);

        counterText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
}
