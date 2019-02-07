using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    #region PlayerAnimation Members
    // Components
    public Player m_player;
    public SpriteRenderer m_sprRender;
	
	// Fields
    public float m_fps = 26;
    public bool m_walk = false;
    public bool m_attack = false;
	public float m_attackTime;

    // Sprites
    public Sprite[] m_playerIdle;
    //public Sprite m_playerIdleShoot;
    public Sprite[] m_playerWalkUp;
    public Sprite[] m_playerWalkDown;
    public Sprite[] m_playerWalkLeftRight;
    //public Sprite[] m_playerWalkShoot;
	public Sprite[] m_playerAttack;
    #endregion

    #region PlayerAnimation Methods
    private void Start()
    {
        m_player = GetComponent<Player>();
        m_sprRender = GetComponent<SpriteRenderer>();
        m_walk = false;
        m_attack = false;
    }

    private void Update()
    {
		// ANIMATION STATES
        switch (m_player.m_playerState)
        {
            // IDLE
            case Player.PlayerState.IDLE:
                m_sprRender.sprite = m_playerIdle[1];
                break;

			// WALK
            case Player.PlayerState.WALK:
                if (!m_walk)
                {
                    StartCoroutine(Walking());
                }
                break;
			
			// ATTACK
			case Player.PlayerState.ATTACK:
                if (!m_attack)
                {
                    StartCoroutine(Attacking());
                }
                //m_sprRender.sprite = m_playerAttack;
                break;
			
            // IDLE SHOOT ANIMATION
            //case Player.PlayerState.IDLE_SHOOT:
            //    if (!m_attack)
            //    {
            //        StartCoroutine(Attacking());
            //    }
            //    m_sprRender.sprite = m_playerIdleShoot;
            //    break;

            // RUN SHOOT ANIMATION
            //case Player.PlayerState.WALK_SHOOT:
            //    if (!m_attack)
            //    {
            //        StartCoroutine(Attacking());
            //    }
            //    if (!m_walk)
            //    {
            //        StartCoroutine(Walking());
            //    }
            //    break;

            // JUMP ANIMATION
            //case Player.PlayerState.JUMP:
            //    m_sprRender.sprite = m_playerJump;
            //    break;

            // JUMP SHOOT ANIMATION
            //case Player.PlayerState.JUMP_SHOOT:
            //    if (!m_attack)
            //    {
            //        StartCoroutine(Attacking());
            //    }
            //    m_sprRender.sprite = m_playerJumpShoot;
            //    break;
        }

        //if (m_player.Facing == Player.PlayerFacing.LEFT)
        //{
        //    m_sprRender.flipX = true;
        //}
        //else if (m_player.Facing == Player.PlayerFacing.RIGHT)
        //{
        //    m_sprRender.flipX = false;
        //}
    }

    private IEnumerator Attacking()
    {
        m_attack = true;

        int index = 0;
        float timeToNextFrame = 1 / m_fps;
        while (m_player.m_playerState == Player.PlayerState.ATTACK)
        {
            yield return new WaitForSeconds(timeToNextFrame);

            if (++index == m_playerAttack.Length)
            {
                index = 0;
            }
            m_sprRender.sprite = m_playerAttack[index];
        }

        m_attack = false;
        yield break;
    }

    private IEnumerator Walking()
    {
        m_walk = true;
		
        int index = 0;
        float timeToNextFrame = 1 / m_fps;
        while (m_player.m_playerState == Player.PlayerState.WALK)
        {
            yield return new WaitForSeconds(timeToNextFrame);
            
            if (!m_attack)
            {
                if (++index == m_playerWalkDown.Length)
                {
                    index = 0;
                }
                m_sprRender.sprite = m_playerWalkDown[index];
                switch (m_player.m_facing) {
                    // UP
                    case Player.PlayerFacing.UP:
                        m_sprRender.sprite = m_playerWalkUp[index];
                        break;
                    // DOWN
                    case Player.PlayerFacing.DOWN:
                        m_sprRender.sprite = m_playerWalkDown[index];
                        break;
                    // LEFT
                    case Player.PlayerFacing.LEFT:
                        m_sprRender.flipX = false;
                        m_sprRender.sprite = m_playerWalkLeftRight[index];
                        break;
                    // RIGHT
                    case Player.PlayerFacing.RIGHT:
                        m_sprRender.flipX = true;
                        m_sprRender.sprite = m_playerWalkLeftRight[index];
                        break;
                }
            }
            //else
            //{
            //    if (++index == m_playerWalkShoot.Length)
            //    {
            //        index = 0;
            //    }
            //    m_sprRender.sprite = m_playerWalkShoot[index];
            //}
        }

        m_walk = false;
        yield break;
    }
    #endregion
}

//	  public PlayerAnimation m_playerAnimator;

//    public PlayerState m_playerState;
//    public enum PlayerState
//    {
//        IDLE,
//        WALK,
//        ATTACK
//    }
//
//    protected PlayerFacing m_facing;
//    public PlayerFacing Facing
//    {
//        get
//        {
//            if (forward.x == 1)
//            {
//                m_facing = PlayerFacing.RIGHT;
//                return m_facing;
//            }
//            else
//            {
//                m_facing = PlayerFacing.LEFT;
//                return m_facing;
//            }
//        }
//    }
//    public enum PlayerFacing
//    {
//        UP,
//        DOWN,
//        LEFT,
//        RIGHT
//    }