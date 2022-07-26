using System;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var minValue = new double[] { int.MaxValue };
            var result = new int[checkpoints.Length];
            MakePermutations(new int[checkpoints.Length], 1, ref result, checkpoints, minValue);
            return result;
        }

        static void MakePermutations(int[] permutation, int position, ref int[] result, Point[] checkpoints, double[] minValue)
        {
            if (position == permutation.Length)
            {
                minValue[0] = PointExtensions.GetPathLength(checkpoints, permutation);
                result = (int[])permutation.Clone();
                return; 
            }
            else
            {
                for (int i = 1; i < permutation.Length; i++)
                {
                    var index = Array.IndexOf(permutation, i, 0, position);
                    if (index != -1)
                    {
                        continue;
                    }
                    permutation[position] = i;
                    if (PointExtensions.GetPathLength(checkpoints,
                                                      permutation.Take(position + 1).ToArray()) >= minValue[0])
                    {
                        continue;
                    }
                    MakePermutations(permutation, position + 1, ref result, checkpoints, minValue);
                }
            }
        }
    }
}