using UnityEngine;

public class Cube : MonoBehaviour
{    
    [SerializeField] private Cube _cube;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] float _explosionForce;
    [SerializeField] float _explosionRadius;    

    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;
    private float _maxChanceOfDivision = 100f;    
    private float currentChanceOfDivision = 100f;

    private void OnMouseDown()
    {
        float _chanceOfDivision = Random.Range(0, _maxChanceOfDivision);

        if (_chanceOfDivision <= _cube.currentChanceOfDivision)
        {
            Init();
        }
           
        Destroy(gameObject);
    }

    public void Init()
    {
        int countCubes = Random.Range(_minCountCubes, _maxCountCubes);

        for (int i = 0; i < countCubes; i++)
        {
            Cube cube = Instantiate(_cube);            

            cube.transform.localScale /= 2;
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cube.currentChanceOfDivision /= 2f;

            Explode(transform.position);
        }
    }

    private void Explode(Vector3 position)
    {
        _rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
    }
}
