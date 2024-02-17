using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    public Transform targetCharacter; // Takip edilecek karakter
    public Animator targetAnimator; // Takip edilecek karakterin animatorü
    public Vector3 offset; // Karakterden gölgeye olan offset

    private Animator shadowAnimator; // Gölgenin animatorü

    void Start()
    {
        // Gölgenin animatorünü al
        shadowAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Gölgenin pozisyonunu karakterin pozisyonuna ve offset'e göre ayarla
        transform.position = targetCharacter.position + offset;

        // Gölgenin animasyon durumunu karakterin animasyon durumuna ayarla
        shadowAnimator.Play(targetAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash);

        // Gölgenin "isMoving" parametresini karakterin "isMoving" parametresine ayarla
        shadowAnimator.SetBool("isMoving", targetAnimator.GetBool("isMoving"));
    }
}
