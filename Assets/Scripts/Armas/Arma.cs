using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma // Tipos de arma
{
    Magia,
    Melee
}

[CreateAssetMenu(menuName = "Personaje/Arma")] // Lo añadimos al menú de Unity para facilitar la creación de armas
public class Arma : ScriptableObject
{
    [Header("Config")]
    public Sprite ArmaIcono; // Icono del arma
    public Sprite IconoSkill; // Icono de la habilidad del arma
    public TipoArma Tipo; // Tipo de arma
    public float Daño; // Daño del arma

    [Header("Arma Magica")]
    public Proyectil ProyectilPrefab; // Prefab del proyectil
    public float ManaRequerida; // Maná requerido para el uso del arma

    [Header("Stats")]
    public float ChanceCritico; // Probabilidad de crítico
    public float ChanceBloqueo; // Probabilidad de bloqueo
}
