using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Rigidbody _rigidbody;

    private float _explosionForce = 10f;
    private float _explosionRadius = 5f;
    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;
    private float _maxChanceOfDivision = 100f;
    private float _currentChanceOfDivision = 100f;

    private void OnMouseDown()
    {
        float chanceOfDivision = Random.Range(0, _maxChanceOfDivision);

        if (chanceOfDivision <= _cube._currentChanceOfDivision)
        {
            Init();
        }
        else
        {
            Explode(transform.position);
        }

        Destroy(gameObject);
    }

    public void Init()
    {
        float coefficient = 2f;
        int countCubes = Random.Range(_minCountCubes, _maxCountCubes);

        for (int i = 0; i < countCubes; i++)
        {
            Cube cube = Instantiate(_cube);

            cube.transform.localScale /= coefficient;
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cube._currentChanceOfDivision /= coefficient;
            cube._explosionForce *= coefficient;
            cube._explosionRadius *= coefficient;
        }
    }

    private void Explode(Vector3 position)
    {
        foreach (Rigidbody explodableObjects in GetExplodableObjects())
        {
            explodableObjects.AddExplosionForce(_explosionForce, position, _explosionRadius , 0f, ForceMode.Impulse);            
        }        
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubs = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubs.Add(hit.attachedRigidbody);
            }
        }

        return cubs;
    }
}