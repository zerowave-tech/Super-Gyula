using System.Collections;
using UnityEngine;

// behaviour only for class Blockcoin
public class BlockCoin : MonoBehaviour
{    
    // command first exicting moment
    private void Start()
    {
        // adding earned coin in counter
        coincounter.Instance.AddBlockDestroyed();
        // start Animate function(lower)
        StartCoroutine(Animate());
    }
    // IEnumerator make corutin, from smiple func
    private IEnumerator Animate()
    {
        // start location of coin
        Vector3 restingPosition = transform.localPosition;
        // sprite animation stretch
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;
        // move object upper
        yield return Move(restingPosition, animatedPosition);
        //move oject lower after .025 sec
        yield return Move(animatedPosition, restingPosition);
        // object disable
        Destroy(gameObject);
    }
    // where we'll move theobject
    private IEnumerator Move(Vector3 from, Vector3 to)
    {    //variable counter and end point
        float elapsed = 0f;
        float duration = 0.25f;
        // cycle will works, until time is gone
        while (elapsed < duration)
        {    // transform time to progress
            float t = elapsed / duration;
            // takes paralel between distance and time 
            transform.localPosition = Vector3.Lerp(from, to, t);
            // times gone,from last frame 
            elapsed += Time.deltaTime;
            // function sleep until next frame
            yield return null;
        }
                  // takes end time point  
        transform.localPosition = to;
    }
    // func when object arrive the command area
    void OnTriggerEnter(Collider other)
    {    // checking really player object arrive or not
           if (other.CompareTag("Player"))
        {
            // if yes, destroy it
            Destroy(gameObject);
        }
    }
}
