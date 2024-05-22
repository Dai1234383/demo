using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using System.Threading;
using System.Collections.Generic;

public class UniTaskSample : MonoBehaviour
{
    // ---------------------------- SerializeField
    [SerializeField] private float _waitTime;

    [Header("DOTween")]
    [SerializeField] private Type _type;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [SerializeField] private Transform[] _pos;
    [SerializeField] private float _scale;

    // ---------------------------- Field


    // ---------------------------- UnityMessage
    private async void Start()
    {
        //  初期設定
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 5000, sequencesCapacity: 200);

        //  基本的なタスク
        Debug.Log("基本的なタスク");
        await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));

        //  キャンセル処理を実装したタスク
        Debug.Log("キャンセル処理を実装したタスク");
        var delayTask = UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: this.destroyCancellationToken);
        if (await delayTask.SuppressCancellationThrow()) { return; }

        //  UniTask型のメソッドを呼び出すタスク
        Debug.Log("UniTask型のメソッドを呼び出すタスク");
        var moveTask = MoveT(this.destroyCancellationToken);
        if (await moveTask.SuppressCancellationThrow()) { return; }

        //  同時再生するタスク
        Debug.Log("同時再生するタスク");
        var allTask = AllTask(this.destroyCancellationToken);
        if (await allTask.SuppressCancellationThrow()) { return; }
    }


    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod
    /// <summary>
    /// アニメーションを順に行うタスク
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask MoveT(CancellationToken ct)
    {
        for(int i=0;_pos.Length>i;i++)
        {
            await MoveTask(gameObject, _pos[i].position, _duration, _ease, ct);
        }

        //foreach(var pos  in _pos)
        //{
        //    await MoveTask(gameObject, pos.position, _duration, _ease, ct);
        //}
    }

    /// <summary>
    /// 同時再生を行うタスク
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask AllTask(CancellationToken ct)
    {
        var tasks = new List<UniTask>
        {
            MoveTask(gameObject, _pos[0].position, _duration, _ease, ct),
            SingleScaleTask(gameObject,_scale,_duration,_ease,ct),
        };

        await UniTask.WhenAll(tasks);   //  同時に再生する

    }

    /// <summary>
    /// 単一移動アニメーション
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    private async UniTask MoveTask
        (GameObject obj
        ,Vector3 pos
        ,float duration
        ,Ease ease
        ,CancellationToken ct)
    {
        await obj.transform.DOMove(pos, duration)
            .SetLink(obj)
            .SetEase(ease)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }

    /// <summary>
    /// 単一スケールアニメーション
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask SingleScaleTask
        (GameObject obj
        , float scale
        , float duration
        , Ease ease
        , CancellationToken ct)
    {
        await transform.DOScale(_scale, _duration)
            .SetLink(gameObject)
            .SetEase(_ease)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }
}