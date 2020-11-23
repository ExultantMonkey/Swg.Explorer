using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace Wxv.Swg.Common.Exporters
{
    public sealed class OBJMeshExporter
    {

        public IRepository Repository { get; private set; }
        public MeshFile MeshFile { get; private set; }

        public OBJMeshExporter(IRepository repository, MeshFile meshFile)
        {
            Repository = repository;
            MeshFile = meshFile;
        }

        public OBJMeshExporter(IRepository repository, byte[] data)
            : this(repository, new MeshFileReader().Load(data))
        {
        }

        private void WriteVertices(MeshFile.MeshGeometry geometry, StreamWriter writer)
        {
            foreach (var vertex in geometry.Vertexes)
            {
                writer.WriteLine("v {0} {1} {2}", vertex.Position.X.ToString("0.0######"), vertex.Position.Y.ToString("0.0######"), (vertex.Position.Z * -1).ToString("0.0######"));
            }
            writer.WriteLine("# {0} vertices", geometry.Vertexes.Count());
            writer.WriteLine();
        }

        private void WriteNormals(MeshFile.MeshGeometry geometry, StreamWriter writer)
        {
            foreach (var vertex in geometry.Vertexes)
            {
                writer.WriteLine("vn {0} {1} {2}", vertex.Normal.X.ToString("0.0######"), vertex.Normal.Y.ToString("0.0######"), (vertex.Normal.Z * -1).ToString("0.0######"));
            }
            writer.WriteLine("# {0} vertex normals", geometry.Vertexes.Count());
            writer.WriteLine();
        }

        private void writeIndices(MeshFile.MeshGeometry geometry, StreamWriter writer)
        {
            for (int index = 0; index <= geometry.Indexes.Count() - 3; index += 3)
            {
                writer.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", (geometry.Vertexes.Count() - geometry.Indexes.ElementAt(index + 2)) * -1, (geometry.Vertexes.Count() - geometry.Indexes.ElementAt(index + 1)) * -1, (geometry.Vertexes.Count() - geometry.Indexes.ElementAt(index)) * -1);
            }
            writer.WriteLine("# {0} polygons", geometry.Indexes.Count() / 3);
            writer.WriteLine();
        }

        private void WriteGeometry(MeshFile.MeshGeometry geometry, StreamWriter writer)
        {
            WriteVertices(geometry, writer);
            WriteNormals(geometry, writer);
            writeIndices(geometry, writer);
        }

        public void Export(string fileName)
        {
            using (var writer = new StreamWriter(fileName, false))
            {
                foreach (var geometry in MeshFile.Geometries)
                {
                    WriteGeometry(geometry, writer);
                }
            }
        }
    }
}
