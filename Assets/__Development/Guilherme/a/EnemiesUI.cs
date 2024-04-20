using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    public GameObject enemyUI;
    public TextMeshProUGUI enemyNameText;
    public Image healthFillImage;
    public float camRayLength = 100f;

    void FixedUpdate()
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
                    enemyUI.SetActive(true);
                    enemyNameText.text = enemyScript.GetEnemyName();
                    healthFillImage.fillAmount = enemyScript.GetEnemyHP() / enemyScript.GetEnemyHP();
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