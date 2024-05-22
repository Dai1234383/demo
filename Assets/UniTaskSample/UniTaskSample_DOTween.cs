using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class UniTaskSample_DOTween : MonoBehaviour
{
    // ---------------------------- SerializeField
    [SerializeField] private float _waitTime;

    [Header("DOTween")]
    [SerializeField] private Type _type;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [SerializeField] private Transform[] _pos;

    // ---------------------------- Field
    private Vector3 _initPos;


    // ---------------------------- UnityMessage
    private void Start()
    {
        //  ‰ŠúÝ’è
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 5000, sequencesCapacity: 200);
        _initPos = transform.position;

        transform.DOMove(_pos[0].position, _duration)
            .SetLink(gameObject)
            .SetEase(_ease)
            .OnComplete(() =>
            {
                transform.DOMove(_pos[1].position, _duration)
                    .SetLink(gameObject)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        transform.DOMove(_pos[2].position, _duration)
                            .SetLink(gameObject)
                            .SetEase(_ease)
                            .OnComplete(() =>
                            {
                                transform.DOMove(_pos[3].position, _duration)
                                    .SetLink(gameObject)
                                    .SetEase(_ease)
                                    .OnComplete(() =>
                                    {
                                        transform.position = _initPos;
                                    });
                            });
                    });
            });
    }


    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod
}
