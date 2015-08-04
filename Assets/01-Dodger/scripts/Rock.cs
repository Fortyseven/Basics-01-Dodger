using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    private const float MIN_MOVE_Y = 0.5f;
    private const float MAX_MOVE_Y = 2.0f;

    public GameObject MySprite;

    private float mSpeed = 0.0f;
    private float mRotation = 0.0f;
    private float mRotationSpeed = 0.0f;

    void Start()
    {
        iTween.Init( this.gameObject );
        resetRock();
    }

    private void resetRock()
    {
        var pos = transform.position;
        pos.y = 5.5f;
        pos.x = Random.Range( -3.0f, 3.0f );
        transform.position = pos;

        mSpeed = Random.Range( 0.8f, 3.0f );

        mRotationSpeed = Random.Range( -1.5f, 1.5f );

        //var ang = transform.localRotation.eulerAngles;
        //ang = new Vector3(  );
        //transform.localRotation = Quaternion.Euler( ang );
        //MySprite.transform.RotateAround( Vector3.zero, new Vector3( 0, 0, 1 ), Random.Range( 0.0f, 180.0f ) );
        //MySprite.transform.Rotate( 0,0, Random.Range( 0.0f, 360.0f ) );

        doBounce();
    }

    void doBounce()
    {
        float target_y = ( transform.position.y - MIN_MOVE_Y ) - Random.Range( 0, MAX_MOVE_Y );

        iTween.MoveTo( this.gameObject, iTween.Hash(
            "y", target_y,
            "easetype", iTween.EaseType.easeOutBounce,
            "time", Random.Range( 0.5f, 2.0f ),
            "oncomplete", "onBounceComplete"
        ) );
    }

    void onBounceComplete()
    {
        // Are we off-screen?
        if ( transform.position.y < -5.5f ) {
            resetRock();
            return;
        }
        doBounce();
    }

    void Update()
    {
        //transform.Translate( 0, ( -3.0f * Time.deltaTime ) * mSpeed, 0 );

        // do rotation
        var rot = mRotation * Time.deltaTime;
        MySprite.transform.Rotate( 0, 0, rot );
        mRotation += ( 1.0f * mRotationSpeed );


    }
}
