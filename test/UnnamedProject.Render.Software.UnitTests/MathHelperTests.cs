using System.Numerics;
using Xunit;

namespace UnnamedProject.Render.Software.UnitTests
{
    public class MathHelperTests
    {
        
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(1, 3, 2)]
        [InlineData(2, 1, 3)]
        [InlineData(2, 3, 1)]
        [InlineData(3, 1, 2)]
        [InlineData(3, 2, 1)]
        public void OrderVectorsByY_IsOrdered(float y1, float y2, float y3)
		{
            var a = new Vector3(0, y1, 0);
            var b = new Vector3(0, y2, 0);
            var c = new Vector3(0, y3, 0);

            (Vector3 s1, Vector3 s2, Vector3 s3) = MathHelper.OrderVectorsByY(a, b, c);

            Assert.True(s1.Y < s2.Y);
            Assert.True(s2.Y < s3.Y);
        }

        private const int WithinBoundsXMin = 0;
        private const int WithinBoundsXMax = 10;
        private const int WithinBoundsYMin = 0;
        private const int WithinBoundsYMax = 10;

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(5, 5)]
        [InlineData(9, 0)]
        [InlineData(0, 9)]
        [InlineData(9, 9)]
        public void WithinBounds_IsWithin(int x, int y)
        {
            bool within = MathHelper.WithinBounds(x, y, WithinBoundsXMin, WithinBoundsXMax, WithinBoundsYMin, WithinBoundsYMax);

            Assert.True(within);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(11, 11)]
        [InlineData(5, 11)]
        [InlineData(11, 5)]
        public void WithinBounds_IsOutside(int x, int y)
        {
            bool within = MathHelper.WithinBounds(x, y, WithinBoundsXMin, WithinBoundsXMax, WithinBoundsYMin, WithinBoundsYMax);

            Assert.False(within);
        }
    }
}
