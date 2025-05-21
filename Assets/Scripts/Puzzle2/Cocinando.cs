using System.Collections;
using UnityEngine;

public class Cocinando : MonoBehaviour
{
    public Transform cloneObj;
    private AudioSource cooking;

    void Start()
    {
        cooking = GetComponent<AudioSource>();
        cooking.Play();
        StartCoroutine(cookTimer());
    }

    IEnumerator cookTimer()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
        Instantiate(cloneObj, new Vector3((float)-3.052, (float)2, (float)20.438), cloneObj.rotation);
        yield return new WaitForSeconds(0.5f);
        cooking.Stop();
    }
}