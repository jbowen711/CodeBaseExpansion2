using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Animator bileşenine referans
    public Animator animator;

    // Update her karede bir kez çağrılır
    void Update()
    {
        // Eğer sol mouse tıklanırsa
        if (Input.GetMouseButtonDown(0))
        {
            // Saldırı animasyonunu tetikle
            animator.CrossFade("Attack",0);
        }
        
    }
}
