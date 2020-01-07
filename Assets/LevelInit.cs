using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public GameObject ball;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            this.cam.transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y, -10);
        }
        catch (System.Exception ex)
        {
            print(ex.Message);
            print(ex.StackTrace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rbody = this.ball.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        this.cam.transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.Space)) {
            rbody.AddForce(new Vector2(0, 200));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rbody.AddForce(new Vector2(100, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            rbody.AddForce(new Vector2(-100, 0));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
