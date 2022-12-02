
using System.Collections.Generic;

/// <summary>
/// A basic descriptor including brief and k-v pairs
/// </summary>
public struct Descriptor
{
    /// <summary>
    /// Breif introduction, one sentence
    /// </summary>
    public string brief;
    /// <summary>
    /// K-V pairs that describe multiple properties
    /// </summary>
    public Dictionary<string, string> properties;
}