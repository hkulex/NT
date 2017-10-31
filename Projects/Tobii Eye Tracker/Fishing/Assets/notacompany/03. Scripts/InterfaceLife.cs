using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceLife : MonoBehaviour
{
    [SerializeField] private Sprite _SpriteDeath;

    public void Death()
    {
        this.GetComponent<Image>().sprite = _SpriteDeath;
    }
}