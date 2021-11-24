namespace ContentPersonalization.Core.Models.Interfaces
{
    public interface IInterestArea
    {
        string InterestAreaKey { get; set; }
        string Name { get; set; }
    }

    public interface IWeightedInterestArea : IInterestArea
    {
        int Weight { get; set; }
    }

    public interface IScoredInterestArea : IInterestArea
    {
        double Score { get; set; }
    }
}
