using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Receta
{
    public string Nombre;
    [Header("Primer Material")]
    public InventarioItem Item1;
    public int Item1CantidadRequerida;
    [Header("Segundo Material")]
    public InventarioItem Item2;
    public int Item2CantidadRequerida;

    [Header("Resultado")]
    public InventarioItem ItemResultado;
    public int ItemResultadoCantidad;
}
