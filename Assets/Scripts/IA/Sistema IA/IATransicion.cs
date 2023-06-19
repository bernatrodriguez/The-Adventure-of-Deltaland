using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // Mostramos esta clase en el inspector
public class IATransicion // Controlamos la transici�n de un estado a otro
{
    public IADecision Decision; // Valor de decisi�n
    public IAEstado EstadoVerdadero; // Valor verdadero que regresa la decisi�n
    public IAEstado EstadoFalso; // Valor falso que regresa la decisi�n
}
