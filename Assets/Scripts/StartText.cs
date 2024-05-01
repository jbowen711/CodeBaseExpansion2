using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartText : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeOn = 5f;
    void Start()
    {
        StartCoroutine(DeactivateText());

        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DeactivateText()
    {
        yield return new WaitForSeconds(timeOn);
        gameObject.SetActive(false);
    }
}
