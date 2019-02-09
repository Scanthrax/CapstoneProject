using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosAdj_Items : MonoBehaviour
{
    public Renderer[] items;
    public PosAdj_Character character;

	void Start () {
        foreach (var rend in items)
        {
            rend.material.color = character.color;
        }
	}
}
