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

    public long GetGhostStepCount()
    {
        List<Node> startNodes = Nodes
            .Where(n => n.Name[2] == 'A')
            .ToList();

        List<long> stepsByStartNode = startNodes
            .Select(GetGhostStepCount)
            .ToList();

        // Nodes reach their end-states every ~10-20k steps, then repeat.
        return stepsByStartNode.Aggregate(1L, LeastCommonMultiple);
    }

    private long GetGhostStepCount(Node startNode)
    {
        Node currentNode = startNode;
        Dictionary<string, Node> nodesByName = Nodes.ToDictionary(n => n.Name);
        int steps = 0;

        foreach (char instruction in GetInstructions())
        {
            if (currentNode.Name[2] == 'Z')
                return steps;

            currentNode = instruction == 'L'
                ? nodesByName[currentNode.Left]
                : nodesByName[currentNode.Right];

            steps++;
        }

        return -1;
    }

    // https://www.c-sharpcorner.com/UploadFile/0c1bb2/program-to-find-lcm-lowest-common-multiples-of-two-numbers/
    public static long LeastCommonMultiple(long a, long b)
    {
        long num1, num2;

        if (a > b)
        {
            num1 = a;
            num2 = b;
        }
        else
        {
            num1 = b;
            num2 = a;
        }

        for (long i = 1; i <= num2; i++)
            if ((num1 * i) % num2 == 0)
                return i * num1;

        return num2;
    }

    private IEnumerable<char> GetInstructions()
    {
        while (true)
            foreach (char c in Instructions)
                yield return c;
    }
}
