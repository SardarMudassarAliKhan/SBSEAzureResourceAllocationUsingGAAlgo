namespace GAAlogImplementationForARM
{
    public class GeneticAlgorithm
    {
        private static Random random = new Random();
        private static string[] allowedSkuNames = new[] { "F1", "D1", "B1", "B2", "B3", "S1", "S2", "S3", "P1", "P2", "P3", "P4" };
        private const int populationSize = 20;
        private const int maxGenerations = 100;
        private const double mutationRate = 0.01;

        public class Candidate
        {
            public string SkuName { get; set; }
            public int SkuCapacity { get; set; }
            public double Fitness { get; set; }
        }

        public static Candidate Run()
        {
            List<Candidate> population = InitializePopulation();
            EvaluateFitness(population);

            for (int generation = 0; generation < maxGenerations; generation++)
            {
                List<Candidate> newPopulation = new List<Candidate>();

                while (newPopulation.Count < populationSize)
                {
                    Candidate parent1 = SelectParent(population);
                    Candidate parent2 = SelectParent(population);
                    Candidate child = Crossover(parent1, parent2);
                    Mutate(child);
                    newPopulation.Add(child);
                }

                EvaluateFitness(newPopulation);
                population = newPopulation.OrderByDescending(c => c.Fitness).ToList();
            }

            return population.First();
        }

        private static List<Candidate> InitializePopulation()
        {
            List<Candidate> population = new List<Candidate>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Candidate
                {
                    SkuName = allowedSkuNames[random.Next(allowedSkuNames.Length)],
                    SkuCapacity = random.Next(1, 4)
                });
            }
            return population;
        }

        private static void EvaluateFitness(List<Candidate> population)
        {
            foreach (var candidate in population)
            {
                candidate.Fitness = EvaluateCandidate(candidate);
            }
        }

        private static double EvaluateCandidate(Candidate candidate)
        {
            return random.NextDouble();
        }

        private static Candidate SelectParent(List<Candidate> population)
        {
            double totalFitness = population.Sum(c => c.Fitness);
            double randomValue = random.NextDouble() * totalFitness;

            double cumulativeFitness = 0.0;
            foreach (var candidate in population)
            {
                cumulativeFitness += candidate.Fitness;
                if (cumulativeFitness >= randomValue)
                {
                    return candidate;
                }
            }

            return population.Last();
        }

        private static Candidate Crossover(Candidate parent1, Candidate parent2)
        {
            return new Candidate
            {
                SkuName = random.NextDouble() < 0.5 ? parent1.SkuName : parent2.SkuName,
                SkuCapacity = random.NextDouble() < 0.5 ? parent1.SkuCapacity : parent2.SkuCapacity
            };
        }

        private static void Mutate(Candidate candidate)
        {
            if (random.NextDouble() < mutationRate)
            {
                candidate.SkuName = allowedSkuNames[random.Next(allowedSkuNames.Length)];
            }
            if (random.NextDouble() < mutationRate)
            {
                candidate.SkuCapacity = random.Next(1, 4);
            }
        }
    }
}
