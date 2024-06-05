using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    private void Start()
    {
        transform.DOMove(new Vector3(2, 0, 0), 1.0f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetLink(gameObject);
    }
}
