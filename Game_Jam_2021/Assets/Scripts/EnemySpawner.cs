using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    
    public Enemy enemyPrefab;
    public Transform pilot;

    public float trajectoryVariance = 15.0f; 
    public float spawnRate = 2.0f;
    public float spawnDistance = -15.0f;
    public int spawnAmount = 1;

    private void Start(){
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn(){
        for (int i = 0; i < this.spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = this.pilot.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Enemy enemy = Instantiate(this.enemyPrefab, spawnPoint, rotation);

            enemy.size = Random.Range(enemy.minSize, enemy.maxSize);
            enemy.SetTrajectory(rotation * -spawnDirection);

        }
    }


}
