using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private EnemySlime _slime;

    public EnemySlime Slime => _slime;

    private void Start()
    {
        _slime = GetComponentInParent<EnemySlime>();
    }
}
