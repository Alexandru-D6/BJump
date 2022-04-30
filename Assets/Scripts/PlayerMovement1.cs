using UnityEngine;
using Photon.Pun;

public class PlayerMovement1 : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private float time_double_tap = 0.2f;
    private float last_tap_time;
    private float power_time = 0f;
    private float last_update;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject gameManager;

    PhotonView view;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().gameObject;
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (IsGrounded() && rb.velocity.y <= 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (power_time == 0f && Input.GetButtonDown("Jump"))
            {
                float time_sincelast_click = Time.time - last_tap_time;
                if (time_sincelast_click <= time_double_tap)
                {
                    Debug.Log("SUPER POWER!");
                    last_update = Time.time;
                    power_time = Time.time;

                    if (gameManager.GetComponent<GameManager>().getPower() == "boostjump")
                        gameManager.GetComponent<GameManager>().Superpower(view.ViewID);
                    else
                    {
                        view.RPC("Superpower", RpcTarget.Others, view);
                    }
                }
                last_tap_time = Time.time;
            }
            if (power_time != 0 && Time.time - power_time >= 10f)
            {
                Debug.Log("YOU CAN USIT AGAIN");
                power_time = 0;

            }

            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void setPowerJump(float boost)
    {
        jumpingPower = boost;
    }

    public void setSpeed()
    {
        speed /= 2.0f;
        jumpingPower /= 2.0f;
        transform.GetComponent<Rigidbody2D>().gravityScale /= 2.0f;
    }

    public void restoreSpeed()
    {
        speed *= 2.0f;
        jumpingPower *= 2.0f;
        transform.GetComponent<Rigidbody2D>().gravityScale *= 2.0f;
    }
}
