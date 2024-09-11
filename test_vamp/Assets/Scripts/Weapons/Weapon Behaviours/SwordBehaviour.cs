using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    List<GameObject> markedEnemies = new List<GameObject>();
    public WeaponsScriptableObjects weaponData;
    [SerializeField] float rotationSpeed;

    void Update()
    {
        //transform.parent.Rotate(-Vector3.forward, 360 * Time.deltaTime);
        StartCoroutine(Rotation(rotationSpeed));
        Destroy(gameObject, rotationSpeed);
    }

    IEnumerator Rotation(float duration)
    {
        Vector3 startRotation = transform.parent.eulerAngles;

        float endRotation = startRotation.z + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation.z, endRotation, t / duration) % 360.0f;
            transform.parent.eulerAngles = new Vector3(startRotation.x, startRotation.y, -zRotation);
            yield return null;
        }
        transform.parent.eulerAngles = startRotation;

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(weaponData.Damage);

            markedEnemies.Add(col.gameObject); //mark the enemy, so that it won't suffer damage more than once per garlic summon
        }
    }

}
