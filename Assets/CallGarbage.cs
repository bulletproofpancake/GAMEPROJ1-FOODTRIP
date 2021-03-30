using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGarbage : MonoBehaviour
{

     Garbage garbage;
     private void Start()
     {
          garbage = FindObjectOfType<Garbage>();
     }

     public void RenewGarbage()
     {
          garbage.ChangeGarbage();
     }
}
