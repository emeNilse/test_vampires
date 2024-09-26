using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShooterSpeaks : MonoBehaviour
{
    public GameObject speechTextPrefab, shooterPrefab;
    public string textToDisplay;

    public void StartSpeak()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject speechInstance = Instantiate(speechTextPrefab, pos, Quaternion.identity);
        speechInstance.GetComponent<TMP_Text>().text = textToDisplay;
    }
}
