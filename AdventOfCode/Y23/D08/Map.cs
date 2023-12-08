namespace AdventOfCode.Y23.D08;

public class Map
{
    public required string Instructions { get; init; }
    public required IReadOnlyCollection<Node> Nodes { get; init; }

    public int GetStepCount()
    {
        Dictionary<string, Node> nodesByName = Nodes.ToDictionary(n => n.Name);
        Node currentNode = nodesByName["AAA"];
        int steps = 0;

        foreach (char instruction in GetInstructions())
        {
            if (currentNode.Name == "ZZZ")
                return steps;

            currentNode = instruction == 'L'
                ? nodesByName[currentNode.Left]
                : nodesByName[currentNode.Right];

            steps++;
        }

        return -1;
    }

    private IEnumerable<char> GetInstructions()
    {
        while (true)
            foreach (char c in Instructions)
                yield return c;
    }
}
