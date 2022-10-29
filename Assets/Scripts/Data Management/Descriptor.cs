
using System.Collections.Generic;

/// <summary>
/// A basic descriptor including brief and k-v pairs
/// </summary>
struct Descriptor
{
    /// <summary>
    /// Name of this descriptor
    /// </summary>
    public string name;
    /// <summary>
    /// ID of this descriptor
    /// </summary>
    public int id;
    /// <summary>
    /// Breif introduction, one sentence
    /// </summary>
    public string brief;
    /// <summary>
    /// K-V pairs that describe multiple properties
    /// </summary>
    public Dictionary<string, string> properties;
}