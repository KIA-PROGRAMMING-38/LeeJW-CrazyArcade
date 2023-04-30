using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


// ũ�� ��ǳ���� �̷������� �ϸ� ��������.
public class ObjectPool<T> : IObjectPool<T> where T : class
{
    private readonly Stack<T> _pool;
    private readonly Func<T> _createFunc; // ��ü�� ���� ������ �� ȣ���� �ݹ�
    private readonly Action<T> _actionOnGet; // Get() �� ȣ���� �� �۾��� �ݹ�
    private readonly Action<T> _actionOnRelease; //Release() �� ȣ���Ҷ� �۾��� �ݹ�
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
    // Pool �� �����ϴ� ��ü�� ����
    // public int CountInactive => _pool.Count;
    // Pool ���� ��ü�� �������� get �Լ�
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
    // Pool �� ��ü�� ��ȯ�ϴ� Release �Լ�
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
    // Pool�� �����ϴ� Clear �Լ�
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
    // pool �� �����ϴ� ��ü�� ����
    int CountInactive { get; }
    //pool ���� ��ü�� �����´�.
    T Get();
    //pool �� ��ü�� ��ȯ
    void Release(T element);
    // pool ����
    void Clear();
}






