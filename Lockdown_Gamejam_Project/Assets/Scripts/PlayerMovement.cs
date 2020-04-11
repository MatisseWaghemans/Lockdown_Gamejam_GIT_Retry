using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rigidbody;
    private bool _hasSquashed = true;
    private bool _hasTurned = false;
    [SerializeField] private float _Squash = 0.2f;
    [SerializeField] private float Frequency = 2f;


    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _force;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private SpriteRenderer character;

    private bool _hasShot = true;

    public float Reloadtime = 1f;

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
            ShootGun();
        MoveGun();
    }

    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetAxisRaw("Horizontal") < 0 && !_hasTurned)
        {
            character.flipX = true;
            _hasTurned = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && _hasTurned)
        {
            character.flipX = false;
            _hasTurned = false;
        }

        movement = movement.normalized;

        if (movement.magnitude > 0.1f & _hasSquashed)
        {
            StartCoroutine(Squash());
        }
    }

    private void ShootGun()
    {
        if (_hasShot)
        {
            _hasShot = false;
            StartCoroutine(ShootCouroutine(Reloadtime));
        }
    }
    private void MoveGun()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - _gun.transform.position.y, lookAt.x - _gun.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        _gun.transform.rotation = Quaternion.Euler(-AngleDeg, 90, 0);
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.deltaTime);
    }


    IEnumerator ShootCouroutine(float Seconds)
    {

        Shoot();
        yield return new WaitForSeconds(0.1f);

        Shoot();
        yield return new WaitForSeconds(0.1f);

        Shoot();

        yield return new WaitForSeconds(Seconds);
        _hasShot = true;
        yield return null;
    }

    private void Shoot()
    {
        GameObject _bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, transform.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(_gun.transform.forward * _force, ForceMode2D.Impulse);
    }

    IEnumerator Squash()
    {
        float time = 0;
        _hasSquashed = false;


        float sin = 0;

        while (sin >= 0)
        {
             sin = _Squash * Mathf.Sin(time * Frequency);
            transform.localScale = Vector3.one + new Vector3(-sin, sin, -sin);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _hasSquashed = true;
        yield return null;
    }
}
