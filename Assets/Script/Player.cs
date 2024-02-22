using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    [SerializeField, Header("-移動速度-")]
    private float _moveSpeed;
    [SerializeField, Header("-ジャンプ速度-")]
    private float _jumpSpeed;

    private Vector2 _inputDirection;
    private Rigidbody2D _rigit;
    private bool _bJump;

    // Start is called before the first frame update
    void Start()
    {
        _rigit = GetComponent<Rigidbody2D>();
        _bJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        _Move();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _bJump = false;
        }

        if (collision.gameObject.tag == "GOAL")
        {
            //goal de riset
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void _HitFloor()
    {
        int layerMask = LayerMask.GetMask("Floor");
        Vector3 rayPos = transform.position - new Vector3(0.0f, transform.lossyScale.y / 2.0f);
        Vector3 raySize = new Vector3(transform.lossyScale.x - 0.1f, 0.1f);
        RaycastHit2D rayHit = Physics2D.BoxCast(rayPos, raySize, 0.0f, Vector2.zero, 0.0f, layerMask);
        if (rayHit.transform == null)
        {
            _bJump = true;
            return;
        }

        if (rayHit.transform.tag == "Floor" && _bJump) 
        {
            print("hit floor"); 
            _bJump = false;
        }
        
    }

    private void _Move()
    {
        if (_bJump) return;
        _rigit.velocity = new Vector2(_inputDirection.x * _moveSpeed, _rigit.velocity.y);
    }

    public void _OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || _bJump) return;
            _rigit.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            _bJump = true;

    }

 

}
