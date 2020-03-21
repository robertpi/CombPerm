using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombPerm.Tests
{
    public class PermutationsTests
    {
        void AssertABCListPermutations(IEnumerable<IEnumerable<char>> results) 
        {
            var expecteds = new[] 
            {
                new [] { 'a', 'b', 'c' },
                new [] { 'a', 'c', 'b' },
                new [] { 'b', 'a', 'c' },
                new [] { 'b', 'c', 'a' },
                new [] { 'c', 'a', 'b' },
                new [] { 'c', 'b', 'a' },
            };
            var items = results.Zip(expecteds);
            foreach (var item in items)
            {
                Assert.AreEqual(item.Second, item.First);
            }
        }

        [Test]
        public void PermutateSortedList()
        {
            var input = new [] { 'a', 'b', 'c' };
            var results = Permutations.AllPermutation(input, true);

            AssertABCListPermutations(results);
        }

        [Test]
        public void PermutateUnsortedList()
        {
            var input = new[] { 'a', 'c', 'b' };
            var results = Permutations.AllPermutation(input);

            AssertABCListPermutations(results);
        }

        [Test]
        public void PermutateLazilyUnsortedList()
        {
            var input = new[] { 'a', 'c', 'b' };
            var results = Permutations.AllPermutationLazy(input);

            AssertABCListPermutations(results);
        }

        IEnumerable<char> GetList() 
        {
            yield return 'a';
            yield return 'b';
            yield return 'c';
        }

        [Test]
        public void PermutateLazyList()
        {
            var input = GetList();
            var results = Permutations.AllPermutationLazy(input);

            AssertABCListPermutations(results);
        }

        [Test]
        public void ProducesNextLexicalPermutation()
        {
            var input = new[] { 'a', 'c', 'b' };
            Assert.IsTrue(Permutations.Next(input));
            Assert.AreEqual(new[] { 'b', 'a', 'c' }, input);
        }

        [Test]
        public void WillNotPermutateHighestPermutation()
        {
            var input = new[] { 'c', 'b', 'a' };
            Assert.IsFalse(Permutations.Next(input));
            Assert.AreEqual(new[] { 'c', 'b', 'a' }, input);
        }

    }
}