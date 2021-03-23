﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.Exercises
{
    /// <summary>
    /// Your task is to apply LINQ `Where` method, so all tests will be passed.
    /// Make sure to preview test data located in TestData.cs file.
    /// Don't modify assertions or test data, all you need to do is to apply LINQ method so `result` variable will have expected value(s).
    /// </summary>
    [TestClass]
    public class Restriction
    {
        [TestMethod]
        public void Where_n_is_greater_than_1_return_3_ints()
        {
            // First test is solved to show you how to use these exercises.
            IEnumerable<int> result = TestData.Numbers.Where(n => n > 1);

            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Where_n_is_less_than_or_equal_to_0_returns_expected_ints()
        {
            IEnumerable<int> result = TestData.Numbers.Where(x=>x<=0).ToList();

            Assert.AreEqual(5, result.Count());
            Assert.IsTrue(new[] { -3, -1, -4, -1, -5 }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_n_multiplied_by_2_is_greater_than_5()
        {
            IEnumerable<int> result = TestData.Numbers.Where(n=>n*2>5).ToList();

            Assert.IsTrue(new[] { 3, 5 }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_n_is_even()
        {
            IEnumerable<int> result = TestData.Numbers.Where(n=>n%2==0);

            Assert.IsTrue(new[] { 2, -4 }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_index_of_n_is_odd()
        {
            IEnumerable<int> result = TestData.Numbers.Where((x,index)=>index%2==0);

            //было пропущено 2
            Assert.IsTrue(new[] { 1, 1, 2, 3, 5 }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_n_is_even_and_n_is_less_than_0()
        {
            IEnumerable<int> result = TestData.Numbers.Where(n=>n%2==0&&n<0);

            Assert.IsTrue(new[] { -4 }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_n_quare_minus_2_times_n_is_greater_than_n()
        {
            // n * n - 2 * n
            IEnumerable<int> result = TestData.Numbers.Where(n=>n*n-2*n>n);

            Assert.IsTrue(new[] { -3, -1, -4, -1, 5, -5 }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_string_length_is_shorter_than_5_letters_returns_1_string()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=> x.Length<5);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Where_string_length_is_9_returns_expected_strings()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=>x.Length==9);

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(new[] { "swordfish" }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_string_starts_with_s()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=>x.StartsWith("s"));

            Assert.IsTrue(new[] { "swordfish", "shark" }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_string_has_i_as_a_second_letter()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=>x[1]=='i');

            Assert.IsTrue(new[] { "tiger", "lion" }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_string_contains_e()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=>x.Contains("e"));

            Assert.IsTrue(new[] { "tiger", "penguin", "elephant" }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_string_ends_with_uppercase_t()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=>x.EndsWith("t"));

            Assert.IsTrue(new[] { "elephant" }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_substring_equals_to_io()
        {
            IEnumerable<string> result = TestData.Animals.Where(x=>x.Contains("io"));

            Assert.IsTrue(new[] { "lion" }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_person_firstname_and_lastname_starts_with_same_letter()
        {
            IEnumerable<TestData.Person> result = TestData.People.Where(x=>x.FirstName[0]==x.LastName[0]);

            Assert.IsTrue(new[] { TestData.People[3] }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_person_was_born_before_1990()
        {
            IEnumerable<TestData.Person> result = TestData.People.Where(x=>x.Born.Year<1990);

            Assert.IsTrue(new[] { TestData.People[1], TestData.People[3] }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_person_was_born_on_day_with_even_number()
        {
            IEnumerable<TestData.Person> result = TestData.People.Where(x=>x.Born.Day%2==0);

            Assert.IsTrue(new[] { TestData.People[0], TestData.People[3] }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_person_was_born_on_monday_21st()
        {
            IEnumerable<TestData.Person> result = TestData.People.Where(x=>x.Born.DayOfWeek==DayOfWeek.Monday&&x.Born.Day==21);

            Assert.IsTrue(new[] { TestData.People[2] }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_person_had_18_years_or_more_in_2000()
        {
            IEnumerable<TestData.Person> result = TestData.People.Where(x=>2000-x.Born.Year>18);

            Assert.IsTrue(new[] { TestData.People[1], TestData.People[3] }.SequenceEqual(result));
        }

        [TestMethod]
        public void Where_person_lastname_contains_ll_and_sum_of_year_month_day_is_greater_than_2000()
        {
            IEnumerable<TestData.Person> result = TestData.People.Where(x=>x.LastName.Contains("ll")&&x.Born.Year+x.Born.Month+x.Born.Day>2000);

            Assert.IsTrue(new[] { TestData.People[2] }.SequenceEqual(result));
        }
    }
}