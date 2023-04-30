using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// 크아 물풍선을 이런식으로 하면 괜찮을듯.
public class ObjectPool<T> : IObjectPool<T> where T : class
{
    private readonly Stack<T> _pool;
    private readonly Func<T> _createFunc; // 객체를 새로 생성할 때 호출할 콜백
    private readonly Action<T> _actionOnGet; // Get() 을 호출할 떄 작업할 콜백
    private readonly Action<T> _actionOnRelease; //Release() 를 호출할때 작업할 콜백
    private readonly int _maxSize;
    private readonly Action<T> _actionOnDestroy;
    public int CountAll { get; private set; }
    public int CountInactive => _pool.Count;
    public int CountActive => CountAll - CountInactive;

    public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null,
        Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null,
        int defaultCapacity = 150, int maxSize = 1000)
    {
        if (createFunc == null)
        {
            throw new ArgumentNullException("createFunc");
        }
        if (maxSize <= 0)
        {
            throw new ArgumentException("Max size must ve greator in Bullet ");
        }
        _pool = new Stack<T>(defaultCapacity);
        _createFunc = createFunc;
        _actionOnGet = actionOnGet;
        _actionOnRelease = actionOnRelease;
        _actionOnDestroy = actionOnDestroy;
    }
    // Pool 에 존재하는 객체의 개수
    // public int CountInactive => _pool.Count;
    // Pool 에서 객체를 가져오는 get 함수
    public T Get()
    {
        T result;
        if (CountInactive > 0)
        {
            result = _pool.Pop();
        }
        else
        {
            result = _createFunc();
            ++CountAll;
        }
        _actionOnGet?.Invoke(result);
        return result;
    }
    // Pool 에 객체를 반환하는 Release 함수
    public void Release(T element)
    {
        _actionOnRelease?.Invoke(element);
        if (CountInactive < _maxSize)
        {
            _pool.Push(element);
        }
        else
        {
            _actionOnDestroy?.Invoke(element);
        }
    }
    // Pool을 정리하는 Clear 함수
    public void Clear()
    {
        if (_actionOnDestroy != null)
        {
            foreach (T item in _pool)
            {
                _actionOnDestroy(item);
            }
        }
        _pool.Clear();
        CountAll = 0;
    }
}
public interface IObjectPool<T> where T : class
{
    // pool 에 존재하는 객체의 개수
    int CountInactive { get; }
    //pool 에서 객체를 가져온다.
    T Get();
    //pool 에 객체를 반환
    void Release(T element);
    // pool 정리
    void Clear();
}






