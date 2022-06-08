using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAnimation : MonoBehaviour
{
    private Animator animator;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.AnimationController();
    }

    private void AnimationController()
    {
        if (!this.gameManager.isCoopMode)
        {
            this.AnimationPlayerOne();
        } else
        {
            this.AnimationPlayerTwo();
        }
    }

    private void AnimationPlayerOne()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.AnimateLeft();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.AnimateIDLE();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.AnimateRight();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.AnimateIDLE();
        }
    }

    private void AnimationPlayerTwo()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.AnimateLeft();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            this.AnimateIDLE();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.AnimateRight();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            this.AnimateIDLE();
        }
    }

    private void AnimateLeft()
    {
        this.animator.SetBool("left", true);
        this.animator.SetBool("right", false);
    }

    private void AnimateRight()
    {
        this.animator.SetBool("right", true);
        this.animator.SetBool("left", false);
    }

    private void AnimateIDLE()
    {
        this.animator.SetBool("left", false);
        this.animator.SetBool("right", false);
    }
}
