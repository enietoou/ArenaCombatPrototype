using System.Collections.Generic;
using Combat.Interfaces;

public static class TargetRegistry
{
    public static readonly List<ITargetable> AllTargets = new List<ITargetable>();
}
