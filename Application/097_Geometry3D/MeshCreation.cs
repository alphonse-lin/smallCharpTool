using System;
using System.Collections.Generic;
using System.Text;
using g3;

namespace UrbanX.Application.Geometry
{
    public class MeshCreation
    {
        private VectorArray3d verticesCollection;
        private int[] triangles;
        public static bool CreateMesh(IEnumerable<Vector3f> vertices, int[] triangles, List<Vector3f> normals, out DMesh3 meshResult)
        {
            DMesh3 mesh = DMesh3Builder.Build(vertices, triangles, normals);
            meshResult = mesh;
            return mesh.CheckValidity();
        }

        public static void CreateMesh(IEnumerable<Vector3d> vertices, int[] triangles, out DMesh3 meshResult)
        {
            List<Vector3d> normals = new List<Vector3d>();
            foreach (var item in vertices)
                normals.Add(Vector3d.AxisZ);
                
            DMesh3 mesh = DMesh3Builder.Build(vertices, triangles,normals);
            meshResult = mesh;
            
        }

        public static string ExportMesh(string path, DMesh3 mesh)
        {
            IOWriteResult result = StandardMeshWriter.WriteFile(path, new List<WriteMesh>() { new WriteMesh(mesh) }, WriteOptions.Defaults);
            return result.message;
        }

        public MeshCreation(VectorArray3d VerticesLists, int[] Triangles)
        {
            verticesCollection = VerticesLists;
            triangles = Triangles;
        }

        public static DMesh3 BoundarySrfFromPts(Vector3d[] vectorList)
        {
            // Use the triangulator to get indices for creating triangles
            Triangulator tri = new Triangulator(vectorList);
            int[] indices = tri.Triangulate();
            CreateMesh(vectorList, indices, out DMesh3 meshResult);
            return meshResult;
        }

        /// <summary>
        /// PointSplatsGenerator 对Mesh里的每个点进行成面
        /// </summary>
        /// <returns></returns>
        public DMesh3 CreateMeshInEachVertex()
        {
            Func<int, Vector3d> PointF = CreatePtF;
            Func<int, Vector3d> NormalF = CreateNormalF;
            return PointSplatsGenerator.Generate(triangles, PointF, NormalF, 10d);
        }

        private Vector3d CreatePtF(int index)
        {
            Vector3d result = verticesCollection[index];
            return result;
        }

        private Vector3d CreateNormalF(int index)
        {
            Vector3d result = new Vector3d(0,0,1) ;
            return result;
        }
    }
}
