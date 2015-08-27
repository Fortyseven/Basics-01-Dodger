using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    private const int MAX_ROCKS = 1;
    private const float WALL_X = 3.0f;
    private const float ROCK_SPAWN_Y = 5.5f;

    public GameObject RockPrefab;

    public Text UI_Clock;
    public Text UI_ClockBest;

    private float _game_time = 0.0f;
    private float _best_game_time = 0.0f;

    public float GameTime { get { return _game_time; } }

    private GameObject[] _rocks = null;

    /****************************************/
    void Start()
    {
        _game_time = 0.0f;

        _rocks = new GameObject[ MAX_ROCKS ];

        for ( int i = 0; i < MAX_ROCKS; i++ ) {
            _rocks[ i ] = (GameObject)Instantiate( RockPrefab, new Vector3( Random.Range( -WALL_X, WALL_X ), ROCK_SPAWN_Y, 0 ), Quaternion.identity );
            _rocks[ i ].GetComponent<Rock>().SetOwner( this );
        }
    }

    /****************************************/
    void Update()
    {
        updateScoreBoard();
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
