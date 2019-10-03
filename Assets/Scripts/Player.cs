using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float money;
    public List<string> milestones;

    void Awake()
    {
        milestones.Add("Milestone 1, First Flight");
    }
}
