using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component // Puede ser heredada por cualquier clase, pero lo que heredará será un componente
{
    private static T _instance;
    public static T Instance
    {
        get // Para poder regresar un valor
        {
            if (_instance == null) // Si la instancia es nula (no la tenemos, hay que buscarla)
            {
                _instance = FindObjectOfType<T>(); // Buscamos el objeto del tipo T (componente)
                if (_instance == null) // Si la instancia es aún nula, la buscamos forzosamente
                {
                    GameObject nuevoGO = new GameObject(); // Creamos un gameobject
                    _instance = nuevoGO.AddComponent<T>(); // La instancia es un gameobject con el componente T
                }
            }

            return _instance; // regresamos la instancia
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T; // La instancia es esta clase pero específicamente del tipo T (componente)
    }
}
