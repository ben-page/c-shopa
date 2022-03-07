namespace CShopa;

public class EntrypointCollection : IEntrypointCollection
{
    private int defaultEntrypointId = WellKnown.Values.DefaultEntrypointId;
    private string defaultEntrypoint;

    private readonly IDictionary<string, int> entrypoints;

    public EntrypointCollection(IDictionary<string, int> entrypoints)
    {
        this.entrypoints = entrypoints;

        if (!entrypoints.Values.Any(v => v == defaultEntrypointId))
        {
            throw new ArgumentException("Default entrypoint is expected.", nameof(entrypoints));
        }

        defaultEntrypoint = entrypoints.First(kvp => kvp.Value == defaultEntrypointId).Key;
    }

    public IReadOnlyCollection<string> Entrypoints => entrypoints.Keys.ToList();

    public string DefaultEntrypoint
    {
        get { return defaultEntrypoint; }
        set
        {
            if (!entrypoints.ContainsKey(value))
            {
                throw new ArgumentException($"Cannot change the default entrypoint to unknown entrypoint '{value}'");
            }

            defaultEntrypoint = value;
            defaultEntrypointId = entrypoints[defaultEntrypoint];
        }
    }

    public int this[string entrypoint] =>
        entrypoints.TryGetValue(entrypoint, out var entrypointId) 
            ? entrypointId 
            : defaultEntrypointId;
}
