using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IncoherentMeshChecker.Shared.Extensions
{
    public static class DoubleExtension
    {
        public const double MaximumDifferenceAllowed = 0.000001;

        public static bool IsApproximatelyEqualTo(this double initialValue, double value)
        {
            return DoubleExtension.IsApproximatelyEqualTo(initialValue, value, MaximumDifferenceAllowed);
        }

        public static bool IsApproximatelyEqualTo(this double initialValue, double value, double maximumDifferenceAllowed)
        {
            return (Math.Abs(initialValue - value) < maximumDifferenceAllowed);
        }
    }
}