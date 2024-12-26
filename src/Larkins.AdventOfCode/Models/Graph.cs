namespace Larkins.AdventOfCode.Models;

public class Graph(Dictionary<string, HashSet<string>> connections)
{
    private List<string> largestClique = [];

    private void BronKerbosch(
        List<string> currentClique,
        List<string> potentialVertices,
        List<string> excludedVertices)
    {
        var isMaximalClique = potentialVertices.Count == 0 && excludedVertices.Count == 0;

        if (isMaximalClique)
        {
            if (currentClique.Count > largestClique.Count)
            {
                largestClique = [..currentClique];
            }

            return;
        }

        TryPotentialVertices(currentClique, potentialVertices, excludedVertices);
    }

    private void TryPotentialVertices(
        List<string> currentClique,
        List<string> potentialVertices,
        List<string> excludedVertices)
    {
        List<string> potentialVerticesCopy = [..potentialVertices];

        foreach (var vertex in potentialVerticesCopy)
        {
            // currentClique with potential vertex added.
            List<string> newClique = [..currentClique, vertex];

            // For this vertex find all potential vertices connected to it.
            List<string> newPotentialVertices =
                [..potentialVertices.FindAll(neighbor => connections[vertex].Contains(neighbor))];

            // For this vertex find all excluded vertices connected to it.
            List<string> newExcludedVertices =
                [..excludedVertices.FindAll(neighbor => connections[vertex].Contains(neighbor))];

            BronKerbosch(newClique, newPotentialVertices, newExcludedVertices);

            // At this level the vertex has been tried with the clique, so it doesn't need to be retried.
            // Only an upcoming vertex not connected to the excluded vertices can be successful in forming a clique.
            potentialVertices.Remove(vertex);
            excludedVertices.Add(vertex);
        }
    }

    public List<string> FindLargestClique()
    {
        largestClique = [];
        BronKerbosch([], connections.Keys.ToList(), []);

        return largestClique;
    }
}
