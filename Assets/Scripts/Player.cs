using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private int speed = 30;
    private float jumpPower = 999f;
    private float saltando;
    private Rigidbody2D rb2d;
    private float distancia;
    private LayerMask whatIsLadder;
    private bool escalando;
    private float jumpForce = 7;

    Animator animador;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        saltando = 0f;
        float x = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, 0) * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            saltando = 1f;
        }

        if (Input.GetKeyDown("left ctrl"))
        {
            animador.SetBool("agachado", true);
        }
        if (!Input.GetKeyDown("left ctrl"))
        {
            animador.SetBool("agachado", false);
        }

        animador.SetFloat("salto", saltando);
        animador.SetFloat("andar", x);


        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distancia, whatIsLadder);

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                escalando = true;
            }

        }
        else
        {
            escalando = false;
        }
        if (escalando)
        {
            float y = Input.GetAxisRaw("Vertical");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, y) * speed;
            rb2d.gravityScale = 0;

        }
        else
        {
            rb2d.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "escena2")
        {
            SceneManager.LoadScene("escena2");
        }
        if (collision.gameObject.name == "escena1")
        {
            SceneManager.LoadScene("escena1");
        }
    }
}
