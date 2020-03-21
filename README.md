Combinations and Permutations
=============================

Just the permutations for now.

At the core of this permutations library is the ablity to
gererate the [next lexicographical permutation](https://www.nayuki.io/page/next-lexicographical-permutation-algorithm) in place on
array. This allows a user to generate all permutations 
of an array of items without allocating a new array for 
each result. 

Example usage:

```
    var input = new[] { 'a', 'c', 'b' };
    Assert.IsTrue(Permutations.Next(input));
    Assert.AreEqual(new[] { 'b', 'a', 'c' }, input);

```