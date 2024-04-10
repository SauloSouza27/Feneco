using UnityEngine;
using Cinemachine;

public class FreezeCameraRotation : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachinePOV povAim;

    private bool isRotationFrozen = true;

    void Start()
    {
        povAim = virtualCamera.GetCinemachineComponent<CinemachinePOV>();

        povAim.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            OnfreezeRotation();
        }
        else
        {
            FreezeRotation();
        }
    }

    void FreezeRotation()
    {
        if (!isRotationFrozen)
        {
            povAim.enabled = false;
            isRotationFrozen = true;
        }
    }

    void OnfreezeRotation()
    {
        if (isRotationFrozen)
        {
            povAim.enabled = true;
            isRotationFrozen = false;
        }
    }
}