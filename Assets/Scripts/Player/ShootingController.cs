using UnityEngine;

public class ShootingController : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawnLeft;
    public Transform bulletSpawnRight;

    public float initialSpeed = 24.0f;
    public float lifeTime = 0.5f;

	private float _overheadAngle = 15.0f;
	private float _overheadAngleMax = 45.0f;
	private float _overheadAngleMin = -15.0f;

    private bool _spawnSideToggle = false;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        CalculateOverheadAngle();
        CalculateBulletSpawnPosition();
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.F)) {
            Fire();
        }
    }

    void Fire() {
        var bulletSpawn = GetNextBulletSpawn();
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * initialSpeed;
        Destroy(bullet, lifeTime);
    }

    private Transform GetNextBulletSpawn() {
        _spawnSideToggle = !_spawnSideToggle;
        return _spawnSideToggle ? bulletSpawnLeft : bulletSpawnRight;
    }

	private void CalculateOverheadAngle() {
		_overheadAngle += Input.GetAxis("Mouse Y"); 
		_overheadAngle = Mathf.Max(_overheadAngle, _overheadAngleMin);
		_overheadAngle = Mathf.Min(_overheadAngle, _overheadAngleMax);
	}

    private void CalculateBulletSpawnPosition() {
		bulletSpawnLeft.rotation = GetSpawnAngles();
		bulletSpawnRight.rotation = GetSpawnAngles();
    }

    private Quaternion GetSpawnAngles() {
        return Quaternion.Euler(-1 * _overheadAngle + 105.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}