using System.Collections;
using UnityEngine;

public interface IRecolector
{
    void AgregarItem(ItemScriptableObject item);
}

public class Item : MonoBehaviour
{
    [SerializeField] private Animator spriteDisplay;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float fuerzaSalto = 10f;
    [SerializeField] private float tiempoDeVida = 10f;


    private ItemScriptableObject datos;
    private Coroutine coroutineDestruccion;

    public void Inicializar(ItemScriptableObject datos)
    {
        this.datos = datos;
    }

    public void DroppearItem()
    {
        Vector2 direccion = new Vector2(
            Random.Range(-0.4f, 0.4f),
            Random.Range(0.8f, 1f)
        ).normalized;

        float magnitud = Random.Range(fuerzaSalto / 2, fuerzaSalto);

        rb.linearVelocity = direccion * magnitud;

        coroutineDestruccion = StartCoroutine(DestruirDespuesDeTiempo(tiempoDeVida));
    }

    private IEnumerator DestruirDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(coroutineDestruccion != null)
            StopCoroutine(coroutineDestruccion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IRecolector>(out var player))
            return;

        player.AgregarItem(datos);
        Destroy(gameObject);
    }
}
