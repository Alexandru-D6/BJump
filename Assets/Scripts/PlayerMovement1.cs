using UnityEngine;
using Photon.Pun;

public class PlayerMovement1 : MonoBehaviourPun
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private float time_double_tap = 0.2f;
    private float last_tap_time;
    private float power_time = 0f;
    private float last_update;

    private Vector3 target;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject gameManager;

    PhotonView view;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().gameObject;
        view = GetComponent<PhotonView>();
        target = transform.position;
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }else
            {
                target = transform.position;
            }

            if (target == transform.position)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }else
            {
                horizontal = ((transform.position.x - target.x) > 0f) ? -1f : 1f;
            }

            if (IsGrounded() && rb.linearVelocity.y <= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            }

            if (power_time == 0f && Input.GetMouseButtonUp(0))
            {
                float time_sincelast_click = Time.time - last_tap_time;
                if (time_sincelast_click <= time_double_tap)
                {
                    last_update = Time.time;
                    power_time = Time.time;

                    if (gameManager.GetComponent<GameManager>().getPower() == "boostjump")
                        gameManager.GetComponent<GameManager>().Superpower(view.ViewID, gameManager.GetComponent<GameManager>().getPower());
                    else
                    {
                        var objects = FindObjectsOfType<GameObject>();
                        foreach (var aa in objects)
                        {
                            PhotonView _view = aa.GetComponent<PhotonView>();
                            if (_view != null && !_view.IsMine)
                            {
                                _view.RPC("Superpower_RPC", RpcTarget.Others, view.ViewID, gameManager.GetComponent<GameManager>().getPower());
                            }
                        }
                    }
                    if (PhotonNetwork.IsMasterClient)
                    {
                        gameManager.GetComponent<GameManager>().setEnabledImage(false);
                    }
                }
                last_tap_time = Time.time;
            }
            if (power_time != 0 && Time.time - power_time >= 10f)
            {

                power_time = 0;

                if (PhotonNetwork.IsMasterClient)
                {
                    gameManager.GetComponent<GameManager>().setEnabledImage(true);
                }

            }

            Flip();
        }
    }

    [PunRPC]
    void Superpower_RPC(int _view, string en_power)
    {
        if (view.IsMine)
        {
            gameManager.GetComponent<GameManager>().Superpower(_view, en_power);
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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
