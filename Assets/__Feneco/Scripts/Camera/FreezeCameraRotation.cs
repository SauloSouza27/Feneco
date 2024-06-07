using UnityEngine;
using Cinemachine;

public class FreezeCameraRotation : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachinePOV povAim;


    void Start()
    {
        povAim = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    void Update()
    {

    }

    public void FreezeRotation()
    {               
         povAim.enabled = false;        
    }

    public void UnfreezeRotation()
    {
        povAim.enabled = true;
    }
}