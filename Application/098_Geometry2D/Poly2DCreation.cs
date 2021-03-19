using System;
using System.Collections.Generic;
using System.Text;
using NTSGeometry=NetTopologySuite.Geometries;
using g3;
using System.IO;
using UrbanXX.IO.GeoJSON;
using NetTopologySuite.Geometries;

namespace UrbanX.Application.Geometry
{
    public class Poly2DCreation
    {
        public static NTSGeometry.Geometry[] CreateCircle(Vector2d[] origins, double radius)
        {
            var count = origins.Length;
            var circleList = new NTSGeometry.Geometry[count];
            for (int i = 0; i < count; i++)
            {
                NTSGeometry.Point temp = new NTSGeometry.Point(origins[i].x, origins[i].y);
                var circle = temp.Buffer(radius,NetTopologySuite.Operation.Buffer.EndCapStyle.Round);
                circleList[i] = circle;
            }
            return circleList;
        }

        public static NTSGeometry.Geometry[] CreateCircle(Vector2d[] origins, double[] radius)
        {
            var count = origins.Length;
            var circleList = new NTSGeometry.Geometry[count];
            for (int i = 0; i < count; i++)
            {
                NTSGeometry.Point temp = new NTSGeometry.Point(origins[i].x, origins[i].y);
                var circle = temp.Buffer(radius[i], NetTopologySuite.Operation.Buffer.EndCapStyle.Round);
                circleList[i] = circle;
            }
            return circleList;
        }

        public static Polygon[] CreatePolygon(Coordinate[][] polygonVertices)
        {
            var count = polygonVertices.Length;
            var polygonList = new Polygon[count];
            for (int i = 0; i < count; i++)
            {
                Polygon polygon = new Polygon(new LinearRing(polygonVertices[i]));
                polygonList[i] = polygon;
            }
            return polygonList;

        }

        public static Coordinate[][] ReadJsonData2D(string jsonFilePath)
        {
            StreamReader sr = File.OpenText(jsonFilePath);
            var feactureCollection = GeoJsonReader.GetFeatureCollectionFromJson(sr.ReadToEnd());
            Coordinate[][] polygonResult = new Coordinate[feactureCollection.Count][];

            for (int i = 0; i < feactureCollection.Count; i++)
            {
                //读取数据
                var jsonDic = feactureCollection[i].Geometry;
                polygonResult[i] = jsonDic.Coordinates;
            }
            return polygonResult;
            
        }

        public static void ContainsInGeo2D(NTSGeometry.Geometry[] mainGeoList, NTSGeometry.Geometry[] secGeoList )
        {

        }
    }
}
