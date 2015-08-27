using UnityEngine;
using System.Collections;

public class DestroyOnAnimationComplete : MonoBehaviour
{
    void Update()
    {
        if ( !GetComponentInChildren<Animation>().isPlaying ) {
            Destroy( this.gameObject );
        }
    }
}
