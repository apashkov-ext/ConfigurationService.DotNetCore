using System;
using System.Linq;
using ConfigurationManagementSystem.Application.Pagination;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.PaginationTests
{
    public class QueryableExtensionsTests
    {
        [Fact]
        public void ToPagedList_NullSourceArg_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => QueryableExtensions.AsPagedList<object>(null, new PaginationOptions(1, 1)));
        }

        [Fact]
        public void ToPagedList_NullOptionsArg_ThrowsException()
        {
            var source = GetSource(0);
            Assert.Throws<ArgumentNullException>(() => QueryableExtensions.AsPagedList(source, null));
        }

        [Fact]
        public void ToPagedList_NonEmpty_ReturnsNonEmptyData()
        {
            var source = GetSource(1);
            var options = new PaginationOptions(0, 10);

            var res = QueryableExtensions.AsPagedList(source, options);

            Assert.NotEmpty(res.Data);
        }

        [Fact]
        public void ToPagedList_NonEmpty_CorrectValues()
        {
            var source = GetSource(10);
            var expectedTotalLength = source.Count();
            var options = new PaginationOptions(2, 2);

            var res = QueryableExtensions.AsPagedList(source, options);
            var elements = res.Data;
            var elementsLength = elements.Length;

            Assert.Equal(expectedTotalLength, res.TotalElements);
            Assert.Equal(elementsLength, options.Limit);
            Assert.Equal(options.Offset, res.Offset);
            Assert.Equal(options.Limit, res.Limit);
        }

        private static IQueryable<TestQuerySourceItem> GetSource(int count)
        {
            var source = Enumerable.Range(1, count).Select(x => new TestQuerySourceItem(x)).AsQueryable();
            return source;
        }

        private class TestQuerySourceItem
        {
            public int Id { get; }

            public TestQuerySourceItem(int id)
            {
                Id = id;
            }
        }
    }
}
