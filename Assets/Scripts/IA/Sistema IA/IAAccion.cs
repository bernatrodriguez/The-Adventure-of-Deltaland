using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAAccion : ScriptableObject // Con abstract, toda aquella clase que herede de IAAccion implemente su método principal (Ejecutar)
{
    public abstract void Ejecutar(IAController controller); // Referenciamos la clase principal del enemigo con el controller
}
