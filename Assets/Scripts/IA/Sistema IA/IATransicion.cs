using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // Mostramos esta clase en el inspector
public class IATransicion // Controlamos la transición de un estado a otro
{
    public IADecision Decision; // Valor de decisión
    public IAEstado EstadoVerdadero; // Valor verdadero que regresa la decisión
    public IAEstado EstadoFalso; // Valor falso que regresa la decisión
}
