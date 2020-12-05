using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.smoothDeltaTime * 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("JudgeLine"))
        {
            //Debug.Log("판정!");
            source.PlayOneShot(clip);
        }
    }
}
