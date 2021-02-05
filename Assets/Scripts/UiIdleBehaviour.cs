using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CheckValidButton = GameController;


public class UiIdleBehaviour : MonoBehaviour
{
    private Animator animator;
    private float destroyTime = 1.5f;
    public bool wasPress = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.CheckStartButtons())
        {
            animator.SetBool("wasPress", true);
            wasPress = true;
        }

        if (wasPress)
        {
            if (destroyTime <= 0)
            {
                Destroy(this.gameObject);
                return;
            }

            destroyTime -= Time.deltaTime;
        }
    }
}
