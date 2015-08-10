using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    private const int MAX_ROCKS = 20;
    private const float WALL_X = 3.0f;
    private const float ROCK_SPAWN_Y = 5.5f;

    public GameObject RockPrefab;

    public Text UI_Clock;
    public Text UI_ClockBest;

    private float mGameTime = 0.0f;
    private float mBestGameTime = 0.0f;

    public float GameTime
    {
        get { return mGameTime; }
    }

    private GameObject[] mRocks = null;

    void Start()
    {
        mGameTime = 0.0f;

        mRocks = new GameObject[ MAX_ROCKS ];

        for ( int i = 0; i < MAX_ROCKS; i++ ) {
            mRocks[ i ] = (GameObject)Instantiate( RockPrefab, new Vector3( Random.Range( -WALL_X, WALL_X ), ROCK_SPAWN_Y, 0 ), Quaternion.identity );
            mRocks[ i ].GetComponent<Rock>().SetOwner( this );
        }
    }

    void Update()
    {
        updateScoreBoard();
    }

    private void updateScoreBoard()
    {
        string time_formatted = String.Format( "{0:F2}", mGameTime += Time.deltaTime );
        UI_Clock.text = time_formatted;

        if ( mGameTime > mBestGameTime ) {
            mBestGameTime = mGameTime;
            UI_ClockBest.text = time_formatted;
        }

    }
}
