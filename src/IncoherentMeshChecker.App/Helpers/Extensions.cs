using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public const double MaximumDifferenceAllowed = 0.000001;

        public static bool IsApproximatelyEqualTo(this double initialValue,double value)
        {
            return ExtensionMethods.IsApproximatelyEqualTo(initialValue, value, MaximumDifferenceAllowed);
        }

        public static bool IsApproximatelyEqualTo(this double initialValue,double value, double maximumDifferenceAllowed)
        {
            return (Math.Abs(initialValue - value) < maximumDifferenceAllowed);
        }

        public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
    }
}
