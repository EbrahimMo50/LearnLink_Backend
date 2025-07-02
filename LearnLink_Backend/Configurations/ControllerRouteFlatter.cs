namespace LearnLink_Backend.Configurations;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

/// <summary>
/// used to lower case all upper cased letters inside controller route to lower case
/// </summary>
public class ControllerRouteFlatter : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
        => value == null ? null : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
}