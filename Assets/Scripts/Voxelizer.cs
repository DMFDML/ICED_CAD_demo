using UnityEngine;

public class Voxelizer : MonoBehaviour
{
    public int voxelResolution = 16;
    public float voxelSize = 1f;

    void Start()
    {
        VoxelGrid voxelGrid = CreateVoxelGrid();

        VoxelizeObject(voxelGrid);

        // You can now use the voxelGrid data for further processing or visualization.
    }

    VoxelGrid CreateVoxelGrid()
    {
        VoxelGrid voxelGrid = new VoxelGrid(voxelResolution, voxelSize);
        return voxelGrid;
    }

    void VoxelizeObject(VoxelGrid voxelGrid)
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Mesh mesh = meshFilter.sharedMesh;

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                Vector3 worldPos = transform.TransformPoint(mesh.vertices[i]);
                voxelGrid.SetVoxelState(worldPos, true);
            }
        }
    }
}

public class VoxelGrid
{
    private bool[,,] voxelData;
    private int resolution;
    private float voxelSize;

    public VoxelGrid(int resolution, float voxelSize)
    {
        this.resolution = resolution;
        this.voxelSize = voxelSize;
        voxelData = new bool[resolution, resolution, resolution];
    }

    public void SetVoxelState(Vector3 worldPosition, bool state)
    {
        Vector3 localPosition = worldPosition / voxelSize + new Vector3(0.5f, 0.5f, 0.5f) * resolution;
        int x = Mathf.FloorToInt(localPosition.x);
        int y = Mathf.FloorToInt(localPosition.y);
        int z = Mathf.FloorToInt(localPosition.z);

        if (x >= 0 && x < resolution && y >= 0 && y < resolution && z >= 0 && z < resolution)
        {
            voxelData[x, y, z] = state;
        }
    }
}