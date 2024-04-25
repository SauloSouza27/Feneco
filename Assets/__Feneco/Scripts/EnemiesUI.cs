using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private GameObject enemyUI;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float camRayLength = 100f;

    void FixedUpdate()
    {
        if (!Application.isEditor)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, camRayLength))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                {
                    EnemyController enemyScript = hit.collider.gameObject.GetComponent<EnemyController>();

                    if (enemyScript != null)
                    {
                        float sliderValue = (float)enemyScript.HealthPoints / (float)enemyScript.MaxHealthPoints;

                        enemyUI.SetActive(true);
                        enemyNameText.text = enemyScript.EnemyName;
                        healthSlider.value = sliderValue;
                    }
                    else
                    {
                        enemyUI.SetActive(false);
                    }
                }
                else
                {
                    enemyUI.SetActive(false);
                }
            }
            else
            {
                enemyUI.SetActive(false);
            }
        }            
    }
}