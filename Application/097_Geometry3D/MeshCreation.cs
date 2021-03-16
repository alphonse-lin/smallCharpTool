using System;
using System.Collections.Generic;
using System.Text;
using g3;
using gs;

namespace UrbanX.Application.Geometry
{
    public class MeshCreation
    {

        #region 001_Baisc Functions for Generating/Exporting Mesh
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

        /// <summary>
        /// Create mesh from a list of points
        /// </summary>
        /// <param name="vectorListInput"></param>
        /// <param name="indicesResult"></param>
        /// <returns></returns>
        public static DMesh3 BoundarySrfFromPts(Vector3d[] vectorListInput, out int[] indicesResult)
        {
            // Use the triangulator to get indices for creating triangles
            var vectorList = new Vector3d[vectorListInput.Length - 1];
            for (int i = 0; i < vectorListInput.Length - 1; i++)
                vectorList[i] = vectorListInput[i];

            Triangulator tri = new Triangulator(vectorList);
            int[] indices = tri.Triangulate();
            indicesResult = indices;
            CreateMesh(vectorList, indices, out DMesh3 meshResult);
            return meshResult;
        }

        /// <summary>
        /// extrude a boundary loop of mesh and connect w/ triangle strip
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static DMesh3 ExtrudeMeshEdge(DMesh3 mesh, double height)
        {
            var meshResult = mesh;
            MeshBoundaryLoops loops = new MeshBoundaryLoops(mesh);
            EdgeLoop eLoop = new EdgeLoop(mesh);
            eLoop.Edges = loops[0].Edges;
            eLoop.Vertices = loops[0].Vertices;
            new MeshExtrudeLoop(meshResult, eLoop)
            {
                PositionF = (v, n, vid) => v + height * Vector3d.AxisZ
            }.Extrude();

            //MeshLoopClosure meshClose = new MeshLoopClosure(mesh, eLoop);
            //meshClose.Close_Flat();
            return meshResult;
        }

        /// <summary>
        /// Extrude a boundary Faces of mesh and connect w/ triangle strip
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static DMesh3 ExtrudeMeshFaces(DMesh3 mesh, int[] triangles, double height)
        {
            var meshResult = mesh;
            new MeshExtrudeFaces(meshResult, triangles,true)
            {
                ExtrudedPositionF = ((Func<Vector3d, Vector3f, int, Vector3d>)((v, n, vid) => v + height * Vector3d.AxisZ))
            }.Extrude();
            return meshResult;
        }

        /// <summary>
        /// Extrude mesh  with certain height and connect w/ triangle strip
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static DMesh3 ExtrudeMeshFromMesh(DMesh3 mesh, double height)
        {
            var meshResult = mesh;
            new MeshExtrudeMesh(meshResult)
            {
                ExtrudedPositionF = (v, n, vid) => v + height * Vector3d.AxisZ
            }.Extrude();
            return meshResult;
        }

        #endregion

        #region 002_Remesher
        public static DMesh3 SimpleRemesher(DMesh3 mesh)
        {
            var meshResult = mesh;
            var ids = mesh.MaxVertexID;

            Remesher r = new Remesher(meshResult);
            r.Precompute();

            MeshConstraints cons = new MeshConstraints();


            if (ids.Count > 0)
                foreach (int id in ids)
                    cons.SetOrUpdateVertexConstraint(id, VertexConstraint.Pinned);


            if (fixEdges)
            {
                MeshConstraintUtil.FixAllBoundaryEdges_AllowSplit(cons, dmesh, 1);
                MeshConstraintUtil.FixAllBoundaryEdges_AllowCollapse(cons, dmesh, 1);
                MeshConstraintUtil.PreserveBoundaryLoops(r);
            }


            for (int i = 0; i < ids.Count; i++)
            {
                //cons.SetOrUpdateEdgeConstraint(eid, new EdgeConstraint(useFlags));
                cons.SetOrUpdateVertexConstraint(ids[i], new VertexConstraint(true, 0));
            }



            if (m.Count > 1)
            {
                int set_id = 1;
                int[][] group_tri_sets = FaceGroupUtil.FindTriangleSetsByGroup(dmesh);
                foreach (int[] tri_list in group_tri_sets)
                {
                    MeshRegionBoundaryLoops loops = new MeshRegionBoundaryLoops(dmesh, tri_list);
                    foreach (EdgeLoop loop in loops)
                    {
                        MeshConstraintUtil.ConstrainVtxLoopTo(r, loop.Vertices,
                            new DCurveProjectionTarget(loop.ToCurve()), set_id++);
                    }
                }
            }

            //Set Parameters and remesh
            r.SetTargetEdgeLength(len);

            if (project)
            {
                r.SmoothSpeedT = 0.5;
                r.SetProjectionTarget(MeshProjectionTarget.Auto(dmesh));
            }
            else
            {
                r.SmoothSpeedT = 0.1;
                r.SetProjectionTarget(MeshProjectionTarget.Auto(dmesh));
            }

            // r.SetExternalConstraints(cons);
            r.EnableFlips = r.EnableSplits = r.EnableCollapses = true;
            r.EnableSplits = true;
            r.EnableSmoothing = true;


            for (int k = 0; k < iterations; ++k)
            {
                r.BasicRemeshPass();
            }
        }

        #endregion
    }
}
