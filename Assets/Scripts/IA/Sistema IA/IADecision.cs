using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IADecision : ScriptableObject // Con abstract, toda aquella clase que herede de IAAccion implemente su m�todo principal (Decidir)
{
    public abstract bool Decidir(IAController controller); // Referenciamos la clase principal del enemigo con el controller
}
