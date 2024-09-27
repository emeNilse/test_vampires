using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    public GameObject textPrefab, unitPrefab;

    public void DamageFloat(string DamageTaken)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        GameObject speechInstance = Instantiate(textPrefab, pos, Quaternion.identity);
        speechInstance.GetComponent<TMP_Text>().text = DamageTaken;
        //transform.position = Vector2.MoveTowards(this.transform.position, unitPrefab.transform.position, 5 * Time.deltaTime);
        // find a way to make the float follow the player?
    }
}
