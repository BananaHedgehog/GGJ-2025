using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject torch;
    public bool hasCharge;
    public bool isOn;
    public int onTime;
    public int rechargeTime;
    int onTimeRemaining;
    int rechargeTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        hasCharge = true;
        torch.SetActive(false);
        onTimeRemaining = onTime;
        rechargeTimeRemaining = rechargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasCharge && !isOn)
        {
            onTimeRemaining = onTime;
            rechargeTimeRemaining = rechargeTime;
            isOn = true;
            Invoke("OnTimer", 1f);
            torch.SetActive(true);
        }
    }

    public void OnTimer()
    {
        onTimeRemaining--;
        if (onTimeRemaining > 0)
        {
            Invoke("OnTimer", 1f);
        }
        else
        {
            hasCharge = false;
            isOn = false;
            torch.SetActive(false);
            Invoke("RechargeTimer", 1f);
        }
    }

    public void RechargeTimer()
    {
        rechargeTimeRemaining--;
        if (rechargeTimeRemaining > 0)
        {
            Invoke("RechargeTimer", 1f);
        }
        else
        {
            hasCharge = true;
            isOn = false;
        }
    }
}
