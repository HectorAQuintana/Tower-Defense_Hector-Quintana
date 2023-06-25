using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField]
    private PlayerStateSO playerState;
    [SerializeField]
    private GameObject towerIndicator;
    [SerializeField]
    private GameObject towerRangeIndicator;
    [SerializeField]
    private LayerMask maskToHit;

    private Camera cam;
    private GameObject towerToBuild;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private IEnumerator PositionTower()
    {
        while(playerState.IsBuilding)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 1000, maskToHit))
            {
                towerIndicator.SetActive(true);
                SetTowerIndicator(hit.point);

                if (Input.GetMouseButtonDown(0))
                {
                    BuildTower(hit.point);
                }
            }
            else
            {
                towerIndicator.SetActive(false);
            }

            yield return null;
        }

        towerIndicator.SetActive(false);
    }

    private void SetTowerIndicator(Vector3 position)
    {
        towerIndicator.transform.position = position;

        Vector3 rangeScale = Vector3.one * towerToBuild.GetComponent<Tower>().TowerScriptableObject.Range;
        rangeScale.y = towerRangeIndicator.transform.localScale.y;
        towerRangeIndicator.transform.localScale = rangeScale;
    }

    private void BuildTower(Vector3 position)
    {
        playerState.SetPlayerToIdle();

        GameObject newTower;

        if (!PoolManager.Pools.ContainsKey(towerToBuild.name))
        {
            newTower = PoolManager.InitializePool(towerToBuild, 20);
        }
        else
        {
            newTower = PoolManager.Pools[towerToBuild.name].Get;
        }

        newTower.transform.position = position;
    }

    public void TowerSelected(GameObject towerPrefab)
    {
        towerToBuild = towerPrefab;
        playerState.SetPlayerToBuilding();
        StartCoroutine(PositionTower());
    }
}
