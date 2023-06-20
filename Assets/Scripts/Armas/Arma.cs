using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma // Tipos de arma
{
    Magia,
    Melee
}

[CreateAssetMenu(menuName = "Personaje/Arma")] // Lo a�adimos al men� de Unity para facilitar la creaci�n de armas
public class Arma : ScriptableObject
{
    [Header("Config")]
    public Sprite ArmaIcono; // Icono del arma
    public Sprite IconoSkill; // Icono de la habilidad del arma
    public TipoArma Tipo; // Tipo de arma
    public float Da�o; // Da�o del arma

    [Header("Arma Magica")]
    public Proyectil ProyectilPrefab; // Prefab del proyectil
    public float ManaRequerida; // Man� requerido para el uso del arma

    [Header("Stats")]
    public float ChanceCritico; // Probabilidad de cr�tico
    public float ChanceBloqueo; // Probabilidad de bloqueo
}
