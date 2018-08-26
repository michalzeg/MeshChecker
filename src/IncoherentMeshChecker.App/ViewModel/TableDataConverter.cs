using IncoherentMeshChecker.Helpers;
using IncoherentMeshChecker.Model.Elements;
using IncoherentMeshChecker.Model.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IncoherentMeshChecker.Converter
{

    public class TableDataConverter
    {
        public ICollection<Element> Elements { get; private set; }
        public ICollection<Node> Nodes { get; private set; }

        private ICollection<ElementTable> elementTable;
        private ICollection<NodeTable> nodeTable;
        private CancellationToken cancelToken;

        public List<string> ErrorList
        {
            get
            {
                return this.errorList.Distinct().ToList();
            }
        }
        private List<string> errorList;
        public bool HasErrors
        {
            get
            {
                return !(this.errorList.Count == 0);
            }
        }

        public TableDataConverter(ICollection<ElementTable> elementTable, ICollection<NodeTable> nodeTable, CancellationToken cancelToken)
        {
            this.errorList = new List<string>();

            this.nodeTable = nodeTable;
            this.elementTable = elementTable;
            this.cancelToken = cancelToken;
            
        }
        public void Convert()
        {
            this.nodeTableToNodes();
            this.elementTableToElement();
        }

        private void elementTableToElement()
        {
            
            //2 Divide collection to 3 different collections
            // beam elements n=>n.Node3 ==0
            // triangle collection n=n.Node4 ==0
            //quad element -> rest
            // create appropriate collection
            this.Elements = new HashSet<Element>();
            this.createBeamElements();
            this.createQuadElements();
            this.createTriangleElements();
        }

        private void createBeamElements()
        {
            var beamElementsList = elementTable.Where(e => e.Node3 == 0 && e.Node4 == 0);

            var elements = new HashSet<Element>();
            foreach (ElementTable element in beamElementsList)
            {
                var nodes = new List<Node>();
                nodes.Add(this.getNode(element.Node1));
                nodes.Add(this.getNode(element.Node2));
                
                this.Elements.Add(new BeamElement(element.Element, nodes));
                this.cancelToken.ThrowIfCancellationRequested();
            }
            
        }
        private void createTriangleElements()
        {
            var triangleElementList = elementTable.Where(e => e.Node4 == 0 && e.Node3 > 0);
            foreach (ElementTable element in triangleElementList)
            {
                var nodes = new List<Node>();
                nodes.Add(this.getNode(element.Node1));
                nodes.Add(this.getNode(element.Node2));
                nodes.Add(this.getNode(element.Node3));
                this.Elements.Add(new TriangleElement(element.Element, nodes));
                this.cancelToken.ThrowIfCancellationRequested();
            }
        }
        private void createQuadElements()
        {
            var quadElements = elementTable.Where(e => e.Node3 > 0 && e.Node4 > 0);
            foreach (ElementTable element in quadElements)
            {
                var nodes = new List<Node>();
                nodes.Add(this.getNode(element.Node1));
                nodes.Add(this.getNode(element.Node2));
                nodes.Add(this.getNode(element.Node3));
                nodes.Add(this.getNode(element.Node3));
                this.Elements.Add(new QuadElement(element.Element, nodes));
                this.cancelToken.ThrowIfCancellationRequested();
            }
        }

        private void nodeTableToNodes()
        {
            this.Nodes = new HashSet<Node>();
            foreach (var nodeElement in this.nodeTable)
            {
                PointD pointCoordinate = new PointD(nodeElement.X, nodeElement.Y, nodeElement.Z);
                Node node = new Node(nodeElement.Node, pointCoordinate);
                this.Nodes.Add(node);
                this.cancelToken.ThrowIfCancellationRequested();
            }
        }

        private Node getNode(int nodeNumber)
        {
            //use HashSet instead of List
            Node node = this.Nodes.FirstOrDefault(n => n.Number == nodeNumber);
            if (node == null)
            {
                string errorText = string.Format("Node {0} does not exist", nodeNumber);
                this.errorList.Add(errorText);
            }
            return node;
            
        }
    }
    
}
