using System;
using UnityEngine;

public class Player : StateMachineMB
{
    public GameObject ScoreIndicatorPrefab;

    private const float WALL_X = 3.0f;
    private const float PLAYER_SPEED = 5.0f;
    private const float PLAYER_Y_OFFS = -2.5f;

    private bool _is_dead;
    private bool _is_jumping;

    private enum PlayerState
    {
        RUN,
        JUMPING
    }

    /*************************************************/
    private class StateJumping : State
    {
        public override Enum Name { get { return PlayerState.JUMPING; } }

        private float _jump_timer = 0.0f;

        public override void OnStateEnter( State from_state )
        {
            ( (Player)OwnerMB )._is_jumping = true;
            //Owner.GetComponent<Renderer>().material.color = new Color( 255, 0, 0 );
            _jump_timer = 1.0f;
            Owner.GetComponent<Animator>().Play( "jump" );
        }

        public override void OnStateExit( State to_state )
        {
            //Owner.GetComponent<Renderer>().material.color = new Color( 255, 255, 255 );
            ( (Player)OwnerMB )._is_jumping = false;
        }

        public override void OnUpdate()
        {
            //Debug.Log( "In the air!" );

            _jump_timer -= Time.deltaTime;
            if ( _jump_timer <= 0 ) {
                Owner.ChangeState( PlayerState.RUN );
                return;
            }

            Owner.States[ PlayerState.RUN ].OnUpdate();
        }

        public override void Start()
        {
            Debug.Log( "Start called" );
        }
    }

    /*************************************************/
    private class StateRun : State
    {
        public override Enum Name
        {
            get { return PlayerState.RUN; }
        }

        public override void Start()
        {
            Reset();
        }

        public override void Reset()
        {
            Owner.transform.position = new Vector3( 0, PLAYER_Y_OFFS );
        }

        public override void OnStateEnter( State from_state )
        {
        }

        public override void OnStateExit( State to_state )
        {
        }

        public override void OnUpdate()
        {
            if ( ( (Player)OwnerMB )._is_dead )
                return;

            // Left/right movement
            Owner.transform.Translate( ( Input.GetAxis( "Horizontal" ) * Time.deltaTime ) * PLAYER_SPEED, 0, 0 );

            // Clip to stage bounds
            var pos = Owner.transform.position;
            pos.x = Mathf.Clamp( pos.x, -WALL_X, WALL_X );
            Owner.transform.position = pos;

            // If we're not in the Run state, skip the rest of this.
            if ((Owner.CurrentState != this) && Owner.InTransition) {
                return;
            }

            // Handle player input
            if ( Input.GetButtonDown( "Jump" ) && !( (Player)OwnerMB )._is_jumping ) {
                    Owner.ChangeState( PlayerState.JUMPING );
                }
            }
        }
    }

    /*************************************************/
    public new void Start()
    {
        base.Start();

        AddState( new StateJumping() );
        AddState( new StateRun() );

        ResetPlayer();
    }

    public void OnTriggerEnter2D( Collider2D col )
    {
        // Invincible while in the air
        if ( CurrentState.Name.Equals( PlayerState.JUMPING ) && !InTransition ) {
            Instantiate( ScoreIndicatorPrefab, transform.position, Quaternion.identity );
            return;
        }

        _is_dead = true;
        Time.timeScale = 0;
    }

    private void ResetPlayer()
    {
        _is_dead = false;
        ChangeState( PlayerState.RUN );
        States[ PlayerState.RUN ].Reset();
    }
}
