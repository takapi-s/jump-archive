using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManeger : MonoBehaviour
{
    private Player _player;
    private Vector2 _initPos;


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _initPos = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        _FollowPlayer();    

        
    }

    private void _FollowPlayer()
    {
        if (_player == null) return;

        float x = _player.transform.position.x;
        float y = _player.transform.position.y;
        //x = Mathf.Clamp(x, _initPos.x, Mathf.Infinity);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
