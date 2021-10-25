using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Image HpBar;
    private ShipPlayer shipPlayer;
    // Start is called before the first frame update
    void Start()
    {
        shipPlayer = FindObjectOfType<ShipPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = Mathf.Clamp01(shipPlayer.HpLeft);
    }
}
