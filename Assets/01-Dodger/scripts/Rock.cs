using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    private const float MIN_MOVE_Y = 0.5f;
    private const float MAX_MOVE_Y = 2.0f;

    private const float MIN_RESPAWN_TIME = 0.1f;
    private const float MAX_RESPAWN_TIME = 1.0f;

    public GameObject MySprite;

    private float mSpeed;
    private float mRotation;
    private float mRotationSpeed;

    private float mRespawnDelay = 9999.9f;
    private bool mWasSpawned = false;
    private  Main mOwner = null;

    void Start()
    {
        iTween.Init( this.gameObject );
    }

    public void SetOwner( Main owner )
    {
        mOwner = owner;
        ResetRock();
    }

    private void ResetRock()
    {
        mRespawnDelay = Random.Range( MIN_RESPAWN_TIME, MAX_RESPAWN_TIME );
        //Debug.Log( "------------------" );
        //Debug.Log( "orig   = " + mRespawnDelay );
        //Debug.Log( "gametime = " + ( mOwner.GameTime + 1 ) );
        mRespawnDelay *= ( 1 / ( ( mOwner.GameTime + 1 ) / 60 ) );
        //Debug.Log( "scaled = " + mRespawnDelay );
        mRespawnDelay = 0;
        mWasSpawned = false;
    }

    private void SpawnRock()
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

        mWasSpawned = true;
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
            ResetRock();
            return;
        }
        doBounce();
    }

    void Update()
    {
        if ( !mWasSpawned ) {
            mRespawnDelay -= Time.deltaTime;
            if ( mRespawnDelay <= 0 ) {
                SpawnRock();
            }
            return;
        }

        //transform.Translate( 0, ( -3.0f * Time.deltaTime ) * mSpeed, 0 );

        // do rotation
        var rot = mRotation * Time.deltaTime;
        MySprite.transform.Rotate( 0, 0, rot );
        mRotation += ( 1.0f * mRotationSpeed );
    }
}
