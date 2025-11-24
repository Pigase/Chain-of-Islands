using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> pool;

    public PoolMono(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }
    public bool HasFreeElement(out T element, bool activateImmediately = false)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                if (activateImmediately)
                    mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element, true))
        {
            return element;
        }
        if (autoExpand)
        {
            return this.CreateObject(true);
        }
        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }

    public T GetFreeElement(Vector3 position)
    {
        if (this.HasFreeElement(out var element, false)) // НЕ активируем сразу
        {
            element.transform.position = position; // Устанавливаем позицию ДО активации
            element.gameObject.SetActive(true); // Теперь активируем
            return element;
        }
        if (autoExpand)
        {
            var newElement = this.CreateObject(false); // Создаем неактивным
            newElement.transform.position = position; // Устанавливаем позицию
            newElement.gameObject.SetActive(true); // Активируем
            return newElement;
        }
        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }

    public T GetFreeElement(Transform spawnPoint)
    {
        return GetFreeElement(spawnPoint.position);
    }
}