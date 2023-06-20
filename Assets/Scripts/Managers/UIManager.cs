using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats; // Es del tipo PersonajeStats (ScriptableObject que hemos creado)

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelCrafting;
    [SerializeField] private GameObject panelCraftingInfo;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;

    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDa�oTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI atributosDisponiblesTMP;

    // Vida
    private float vidaActual;
    private float vidaMax;
    
    // Man�
    private float manaActual;
    private float manaMax;

    // Experiencia
    private float expActual;
    private float expRequeridaNuevoNivel;

    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }

    private void ActualizarUIPersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime); // Actualizamos la barra de vida
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime); // Actualizamos la barra de man�
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime); // Actualizamos la barra de experiencia

        vidaTMP.text = $"{vidaActual}/{vidaMax}"; // Actualizamos el texto de vida
        manaTMP.text = $"{manaActual}/{manaMax}"; // Actualizamos el texto de man�
        expTMP.text = $"{expActual} XP"; // Actualizamos el texto de experiencia (puntos XP)
        // expTMP.text = $"{((expActual/expRequeridaNuevoNivel) * 100):F2}%"; // Actualizamos el texto de experiencia (porcentaje)

        nivelTMP.text = stats.Nivel.ToString(); // Actualizamos el nivel del personaje mostrado convirtiendo el stat en texto
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString(); // Actualizamos las monedas del personaje
    }

    private void ActualizarPanelStats()
    {
        if (panelStats.activeSelf == false) // Si el panel de stats no est� activo, es decir, no se muestra en pantalla
        {
            return; // No actualizamos, paramos aqu�
        }

        // Convertimos las variables a texto de TextMeshPro
        statDa�oTMP.text = stats.Da�o.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = $"{stats.PorcentajeCritico}%"; // Porcentaje, usamos string interpolation
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%"; // Porcentaje, usamos string interpolation
        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();

        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();
        atributosDisponiblesTMP.text = $"Puntos: {stats.PuntosDisponibles}"; // Porcentaje, usamos string interpolation
    }
    
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax) // M�todo para obtener los valores de vida
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }

    public void ActualizarManaPersonaje(float pManaActual, float pManaMax) // M�todo para obtener los valores de man�
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }

    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida) // M�todo para obtener los valores de man�
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        panelStats.SetActive(!panelStats.activeSelf); // Activamos o desactivamos el panel, cada vez que llamemos al m�todo
    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf); // Activamos o desactivamos el panel, cada vez que llamemos al m�todo
    }

    public void AbrirPanelCrafting()
    {
        panelCrafting.SetActive(true); // Activamos el panel, cada vez que llamemos al m�todo
    }

    public void CerrarPanelCrafting()
    {
        panelCrafting.SetActive(false); // Desactivamos el panel, cada vez que llamemos al m�todo
        AbrirCerrarPanelCraftingInformacion(false);
    }

    public void AbrirCerrarPanelCraftingInformacion(bool estado)
    {
        panelCraftingInfo.SetActive(estado); // Activamos o desactivamos el panel, cada vez que llamemos al m�todo
    }

    public void AbrirCerrarPanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf); // Activamos o desactivamos el panel, cada vez que llamemos al m�todo
    }

    public void AbrirCerrarPanelPersonajeQuests()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf); // Activamos o desactivamos el panel, cada vez que llamemos al m�todo
    }

    public void AbrirCerrarPanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf); // Activamos o desactivamos el panel, cada vez que llamemos al m�todo
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion) // Llamamos al m�todo correspondidente al panel que queremos abrir, seg�n la interacci�n extra del NPC
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quests:
                AbrirCerrarPanelInspectorQuests();
                break;
            case InteraccionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
            case InteraccionExtraNPC.Crafting:
                AbrirPanelCrafting();
                break;
        }
    }

    #endregion
}
