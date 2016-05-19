using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public float bpm;
    public float tolerance;
	
	[HideInInspector]
	public bool canMove;
	
	// Queste variabili servono ad interfacciarsi col cubo verde/rosso di debug.
    public MeshRenderer cubeMesh;
    public Material red;
    public Material green;
	
    private float beat;
	
    private AudioSource audioSource;
	
	void Start()
    {
        canMove = false;
        beat = 60.0f / bpm;
		
        audioSource = GetComponent<AudioSource>();
		
        StartCoroutine( PlayBeat() );
	}
	
	// Questa funzione serve ad interfacciarsi col cubo verde/rosso di debug.
	void Update()
    {
        if ( canMove )
            cubeMesh.material = green;
        else
            cubeMesh.material = red;
		
        if ( Input.GetButtonDown( "Stealth" ) )
        {
            if ( canMove )
                Debug.Log( "OK!" );
            else
                Debug.Log( "Vaffanculo." );
        }
	}
	
    private IEnumerator PlayBeat()
    {
        float halfTolerance = tolerance / 2;

        yield return new WaitForSeconds( beat - halfTolerance );
		
        while ( true )
        {
            canMove = true;
            audioSource.Play(); // Placeholder per la musica.
            yield return new WaitForSeconds( halfTolerance );

            // ToDo: Chiama la funzione movimento di ogni torretta.
            yield return new WaitForSeconds( halfTolerance );
			
            canMove = false;
            yield return new WaitForSeconds( beat - tolerance );
        }
    }
}