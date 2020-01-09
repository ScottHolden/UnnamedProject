using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace UnnamedProject.Render.Software.UnitTests
{
    public class MathHelperTests
    {
        
        [Theory]
        [MemberData(nameof(SortData))]
        public void OrderVectorsByY_IsOrdered(Vector3 a, Vector3 b, Vector3 c)
		{
            (Vector3 s1, Vector3 s2, Vector3 s3) = MathHelper.OrderVectorsByY(a, b, c);
            Assert.True(s1.Y < s2.Y);
            Assert.True(s2.Y < s3.Y);
        }
        private static readonly Vector3 s_vectorWithY1 = new Vector3(0, 1, 0);
        private static readonly Vector3 s_vectorWithY2 = new Vector3(0, 2, 0);
        private static readonly Vector3 s_vectorWithY3 = new Vector3(0, 3, 0);
        public static readonly IEnumerable<object[]> SortData =
            new List<object[]>
            {
                new object[] { s_vectorWithY1, s_vectorWithY2, s_vectorWithY3 },
                new object[] { s_vectorWithY1, s_vectorWithY3, s_vectorWithY2 },
                new object[] { s_vectorWithY2, s_vectorWithY1, s_vectorWithY3 },
                new object[] { s_vectorWithY2, s_vectorWithY3, s_vectorWithY1 },
                new object[] { s_vectorWithY3, s_vectorWithY2, s_vectorWithY1 },
                new object[] { s_vectorWithY3, s_vectorWithY1, s_vectorWithY2 }
            };
    }
}
