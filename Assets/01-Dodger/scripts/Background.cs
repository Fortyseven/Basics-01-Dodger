using UnityEngine;

public class Background : MonoBehaviour
{

    private const float SPEED = 3.0f;
    private const float START_Y = 5.0f;
    private const float MAX_Y = 16.42f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate( 0, SPEED * Time.deltaTime, 0 );
        if ( transform.position.y > MAX_Y ) {
            var pos = transform.position;
            pos.y = START_Y;
            transform.position = pos;
        }
    }
}
