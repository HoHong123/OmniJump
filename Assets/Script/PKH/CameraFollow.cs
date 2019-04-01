﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    const int normal = 5;

    public float yPos;
    public float smoothTimeX;
    public float smoothTimeY;
    public float maxY;
    public float smoothZoomIn = 5;
    public float smoothZoomOut = 5;

    public Vector3 screenSize;

    private float SpacingX;
    [SerializeField] private float SpacingY;
    private Vector2 velocity = new Vector2(0, 0);
    private Transform player;
    //private PlayerController controller;
    public static Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
        //controller = player.GetComponent<PlayerController>();

        screenSize = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)) - mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        SpacingX = screenSize.x * 0.2f;
        SpacingY = screenSize.y * 0.3f;
    }
	
	void LateUpdate ()
    {
        float xPos = 0;

        if (player.GetComponent<PlayerController>().moveRight)
        {
            xPos = Mathf.Clamp(transform.position.x, player.position.x + SpacingX, player.position.x + SpacingX * 1.5f);
        }
        else
        {
            xPos = Mathf.Clamp(transform.position.x, player.position.x - SpacingX * 1.5f, player.position.x - SpacingX);
        }

        xPos = Mathf.SmoothDamp(transform.position.x, xPos, ref velocity.x, smoothTimeX);

        yPos = Mathf.Clamp(transform.position.y, player.position.y - SpacingY, player.position.y - SpacingY* 0.3f);
        yPos = Mathf.SmoothDamp(transform.position.y, yPos, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(xPos, yPos, -100);
    }
}