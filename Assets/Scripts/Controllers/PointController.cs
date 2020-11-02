using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public GameObject rope;
    public event Action<PointController> OnClick;

    internal Animator pointAnimator;
    internal Text orderText;

    public int Order;
    // Start is called before the first frame update
    void Start()
    {
        orderText = GetComponentInChildren<Text>();
        pointAnimator = GetComponent<Animator>();

        orderText.text = Order.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        OnClick(this);
    }

    public void StartClickAnimation()
    {
        pointAnimator.SetBool("PointClicked", true);
    }

}
