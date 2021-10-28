using System;
using System.Collections.Generic;
using System.Text;

namespace SensiML_Test_App.Models
{
    class JsonImportModel
    {        
    }
public class ModelIndexes
{
    public string _0 { get; set; }
}

public class ClassMaps
{
    //[JsonProperty("1")] public string _1 { get; set; }
    public string _2 { get; set; }
    public string _0 { get; set; }
}

public class ModelDescription
{
    public string Name { get; set; }
    public ClassMaps ClassMaps { get; set; }
    public string ModelType { get; set; }
    public List<string> FeatureNames { get; set; }
    public List<int> AIF { get; set; }
    public List<int> Category { get; set; }
    public List<List<int>> Vector { get; set; }
    public List<int> Identifiers { get; set; }
    public int DistanceMode { get; set; }
}

public class Root
{
    public int NumModels { get; set; }
    public ModelIndexes ModelIndexes { get; set; }
    public List<ModelDescription> ModelDescriptions { get; set; }
}
}
