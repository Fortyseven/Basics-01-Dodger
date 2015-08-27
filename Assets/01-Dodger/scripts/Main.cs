using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    private const int MAX_ROCKS = 40;
    private const float WALL_X = 3.0f;
    private const float ROCK_SPAWN_Y = 5.5f;
    private const float MAX_CRAZY_TIME = 100.0f;

    public GameObject RockPrefab;

    public Text UI_Clock;
    public Text UI_ClockBest;

    private float _game_time;
    private float _best_game_time = 0.0f;

    public float GameTime { get { return _game_time; } }

    private GameObject[] _rocks = null;

    private int _num_active_rocks = 0;


    /****************************************/
    void Start()
    {
        _game_time = 0.0f;

        _rocks = new GameObject[ MAX_ROCKS ];

        for ( int i = 0; i < MAX_ROCKS; i++ ) {
            _rocks[ i ] = (GameObject)Instantiate( RockPrefab, new Vector3( Random.Range( -WALL_X, WALL_X ), ROCK_SPAWN_Y, 0 ), Quaternion.identity );
            _rocks[ i ].GetComponent<Rock>().SetOwner( this );
            // Rocks start off disabled; will be enabled slowly in wave manager
            _rocks[ i ].SetActive( false );
        }
    }

    /****************************************/
    void Update()
    {
        updateScoreBoard();

        // Adjust difficulty by gradually adding more rocks until we hit MAX_CRAZY_TIME where 
        // it's just all the rocks forever and ever.
        _num_active_rocks = Mathf.Clamp( Mathf.RoundToInt( MAX_ROCKS * ( _game_time / MAX_CRAZY_TIME ) ), 0, MAX_ROCKS );

        // Update which rocks are active
        for ( int i = 0; i < _num_active_rocks; i++ ) {
            if ( _rocks[ i ].activeInHierarchy )
                continue;

            _rocks[ i ].SetActive( true );
        }
    }

    /****************************************/
    private void updateScoreBoard()
    {
        string time_formatted = String.Format( "{0:F2}", _game_time += Time.deltaTime );
        UI_Clock.text = time_formatted;

        if ( _game_time > _best_game_time ) {
            _best_game_time = _game_time;
            UI_ClockBest.text = time_formatted;
        }
    }
}
