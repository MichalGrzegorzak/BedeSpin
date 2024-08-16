using Bede.Spin.Core.Domain;

namespace Bede.Spin.Core.Services;

public interface IProbabilitiesGenerator
{
    Probability[] GenerateArray(int arrayLength);
}

public class ProbabilitiesGenerator : IProbabilitiesGenerator
{
    public Probability[] GenerateArray(int arrayLength)
    {
        var result = new Probability[arrayLength];
        for (int idx = 0; idx < arrayLength; idx++)
        {
            result[idx] = Probability.Random();
        }

        return result;
    }
}