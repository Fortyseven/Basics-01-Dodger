using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private const float WALL_X = 3.0f;
    private const float PLAYER_SPEED = 5.0f;
    private const float PLAYER_Y_OFFS = -2.5f;

    private bool mIsPlayerDead;

    public void Start()
    {
        ResetPlayer();
    }

    public void OnTriggerEnter2D( Collider2D col )
    {
        mIsPlayerDead = true;
        Time.timeScale = 0;
    }

    private void ResetPlayer()
    {
        mIsPlayerDead = false;        
        transform.position = new Vector3( 0, PLAYER_Y_OFFS );
    }

    public void Update()
    {
        transform.Translate(
            ( Input.GetAxis( "Horizontal" ) * Time.deltaTime ) * PLAYER_SPEED,
            0, 0
        );

        var pos = transform.position;
        pos.x = Mathf.Clamp( pos.x, -WALL_X, WALL_X );
        transform.position = pos;
    }
}
