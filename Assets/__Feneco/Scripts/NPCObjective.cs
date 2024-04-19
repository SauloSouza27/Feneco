using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObjective : NPC
{
    private bool isDelivered = false;

    //public new void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.name == "Area Deliver Escort")
    //    {
    //        isDelivered = true;
    //        if (!base.GetIsQuestUpdated())
    //        {
    //            base.CompleteQuest();
    //        }
    //    }
    //}
    
    public bool GetIsDelivered()
    {
        return isDelivered;
    }
}
