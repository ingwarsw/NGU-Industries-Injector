using System.Collections.Generic;
using System.Linq;

namespace NGUIndustriesInjector
{
    public static class ExperimentUtilities
    {
        /// <summary>
        /// Returns a Dictionary whose Keys are the Index of Queued Experiments and whose Value is their Reward Factor
        /// </summary>
        /// <param name="weighted">Whether or not to weight reward based on existing rewards</param>
        /// <param name="experiments">The Player's list of Experiments</param>
        /// <param name="alreadyFrozen">This allow you to assume the passed Experiment's rewards when calculating weights. 
        /// Used for calculating weights when freezing multiple experiments.</param>
        /// <returns></returns>
        public static Dictionary<int, double> GetQueueIndexToRewardFactor(
            bool weighted, 
            Experiments experiments, 
            IEnumerable<Experiment> alreadyFrozen = null)
        {
            var queueIndexToRewardFactor = new Dictionary<int, double>();

            int queueIndex = 0;
            if (weighted)
            {
                var resourcesClone = experiments.resources.Select(r => r).ToList();
                if (experiments.activeExperiments != null)
                {
                    foreach (var exp in experiments.activeExperiments)
                    {
                        int rewardIndex = 0;
                        exp.rewards.ForEach(reward =>
                            resourcesClone[rewardIndex++] += reward);
                    }
                }

                if (alreadyFrozen != null)
                {
                    foreach (var exp in alreadyFrozen)
                    {
                        int rewardIndex = 0;
                        exp.rewards.ForEach(reward =>
                            resourcesClone[rewardIndex++] += reward);
                    }
                }

                int experimentIndex = 0;
                experiments.queuedExperiments.ForEach(experiment =>
                {
                    double weightedRewardSum = 0;
                    int resourceIndex = 0;
                    foreach (double resource in resourcesClone)
                    {
                        weightedRewardSum += (experiment.rewards[resourceIndex++] / resource);
                    }

                    var weight = weightedRewardSum / (experiment.totalEffortCost / 1000000);
                    if (alreadyFrozen?.Contains(experiment) ?? false)
                    {
                        Main.Debug($"Assuming weight 0 for Experiment {experimentIndex} since it's already frozen");
                        weight = 0;
                    }
                    experimentIndex++;
                    queueIndexToRewardFactor[queueIndex++] = weight;
                });
            }
            else
            {
                experiments.queuedExperiments.ForEach(experiment =>
                    queueIndexToRewardFactor[queueIndex++] = experiment.rewards.Sum() / experiment.totalEffortCost);
            }

            return queueIndexToRewardFactor;
        }
    }
}
