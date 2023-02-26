using FluentAssertions;

namespace Collections.UnitTests
{
    public class CollectionTests
    {
        private Collection<int> nums;
        private Collection<string> strings;

        [SetUp]
        public void Setup()
        {
            this.nums = new Collection<int>();
            this.strings = new Collection<string>();
        }

        [TestCase(Constants.ExpectedInitialCount, Constants.ExpectedInitialCapacity)]
        public void Check_EmptyConstructor_When_CollectionIsEmpty_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange
            var expectedStringCollectionRepresentation = "[]";

            // Act 
            //We dont add items, we check just the initiated Collection

            // Assert
            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountOneItem, Constants.ExpectedInitialCapacity)]
        public void Add_OneItemToCollection_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange          
            var expectedStringCollectionRepresentation = "[1]";

            // Act 
            nums.Add(1);

            // Assert
            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountFiveItems, Constants.ExpectedInitialCapacity)]
        public void AddRange_ItemsToCollection_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange           
            var expectedStringCollectionRepresentation = "[1, 2, 3, 4, 5]";

            // Act 
            nums.AddRange(1, 2, 3, 4, 5);

            // Assert
            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountSixteenItems, Constants.ExpectedInitialCapacity)]
        public void AddRange_CountOfItemsEqualToCapacityOfCollection_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange          
            var expectedStringCollectionRepresentation = "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]";

            // Act 
            nums.AddRange(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            // Assert
            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountSeventeenItems, Constants.ExpectedDoubleInitialCapacity)]
        public void AddRange_CountOfItemsWithOneMoreThanCollectionCapacity_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange
            var expectedStringCollectionRepresentation = "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17]";

            // Act 
            nums.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });

            // Assert
            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountOneItem, Constants.ExpectedInitialCapacity)]
        public void Add_ItemsOfTypeString_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange            
            var expectedStringCollectionRepresentation = "[QA]";
            // Act 
            strings.Add("QA");

            // Assert
            Assertions.AssertCollections<string>(expectedCount, strings, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountSeventeenItems, Constants.ExpectedDoubleInitialCapacity)]
        public void AddRange_ItemsOfTypeStringWithOneMoreThanCapacity_IsSuccessful(int expectedCount, int expectedCapacity)
        {
            // Arrange            
            var expectedStringCollectionRepresentation = "[A, B, C, D, E, F, G, K, L, M, O, P, Q, R, S, T, U]";
            // Act 
            strings.AddRange("A", "B", "C", "D", "E", "F", "G", "K", "L", "M", "O", "P", "Q", "R", "S", "T", "U");

            // Assert
            Assertions.AssertCollections<string>(expectedCount, strings, expectedCapacity, expectedStringCollectionRepresentation);
        }

        [TestCase(Constants.ExpectedCountFourItems, Constants.ExpectedInitialCapacity)]
        public void Check_AddedItemAtCollectionIndexExists_IsSuccessfull(
            int expectedCount, int expectedCapacity)
        {
            // Arrange
            nums.AddRange(100, 900, 10000);
            var expectedStringCollectionRepresentation = "[100, 900, 0, 10000]";

            // Act
            nums.InsertAt(2, 0);

            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
            Assert.Zero(nums[2]);
        }

        [TestCase(Constants.ExpectedCountFourItems, Constants.ExpectedInitialCapacity)]
        public void Check_AddItemInCollection_OnInvalidIndex_ReturnsError(
            int expectedCount, int expectedCapacity)
        {
            // Arrange
            
            var expectedStringCollectionRepresentation = "[100, 900, 0, 10000]";
            nums.AddRange(100, 900, 0, 10000);
            // Act -> trying to insert number at invalid Index see down
            
            // Asert
            Assert.That(() => {nums.InsertAt(10, 00); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assertions.AssertCollections<int>(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
            Assert.Zero(nums[2]);
        }

        [Test]
        public void ExchangeTwoItemsAtgivenPositions_IsSuccessful()
        {
            //Arrange
            nums.AddRange(100, 900, 10000);
            var firstItemValue = nums[0];
            var lastItemValue = nums[2];

            var expectedValueAtIdx0 = 10000;
            var expectedValueAtidx2 = 100;

            // Act 
            nums.InsertAt(0, lastItemValue);
            nums.InsertAt(2, firstItemValue);

            // Assert that values at indexes 0 and 2 are exchanged
            Assert.AreEqual(expectedValueAtIdx0, nums[0]);
            Assert.AreEqual(expectedValueAtidx2, nums[2]);
        }

        [Test]
        public void RemoveItem_ByValidIndexFromCollection_IsSuccessful()
        {
            // Arrange          
            nums.AddRange(100, 900, 10000);

            Collection<int> expectedCollection = new Collection<int>(900, 10000);

            // Act
            nums.RemoveAt(0);

            // Assert
            nums.Should().BeEquivalentTo(expectedCollection);
            nums.Count.Should().Be(expectedCollection.Count);
        }

        [Test]
        public void RemoveItem_ByInvalidIndexFromCollection_ReturnsArgumentOutOfRangeException()
        {
            // Arrange          
            nums.AddRange(100, 900, 10000);
            Collection<int> expectedCollection = new Collection<int>(100, 900, 10000);

            // Act
            
            // Assert
            Assert.That(() => { var invalidIdxException = nums.RemoveAt(-1);},  Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.AreEqual(expectedCollection.Count, nums.Count);
            nums.Should().BeEquivalentTo(expectedCollection);
            nums.Count.Should().Be(expectedCollection.Count);
        }

        [TestCase(Constants.ExpectedInitialCount, Constants.ExpectedInitialCapacity)]
        public void Clear_Collection_IsSuccesssful(int expectedInitialCount, int expectedInitialCapacity)
        {
            // Arrange
            nums.AddRange(100, 900, 10000);
            string expectedCollectionStringRepresentation = "[]";

            // Act
            nums.Clear();

            // Assert
            Assertions.AssertCollections(expectedInitialCount, nums, expectedInitialCapacity, expectedCollectionStringRepresentation);
            //Assert.IsEmpty(nums.ToString());            
        }

        [TestCase(Constants.ExpectedCountOneItem, Constants.ExpectedInitialCapacity)]
        public void Add_ItemAtBeggnning_IsSuccessful(int expectedCount, int ExpectedCapacity)
        {
            // Arrange
            var ExpectedNumber = 1;
            var expectedStringCollectionRepresentation = "[1]";

            ///Act
            nums.Add(1);
            var expectedFirstNumber = 1;

            // Assert
            Assertions.AssertCollections(expectedCount, nums, ExpectedCapacity, expectedStringCollectionRepresentation);
            nums[0].Should().Be(expectedFirstNumber);
        }

        [TestCase(Constants.ExpectedCountOneItem, Constants.ExpectedInitialCapacity)]
        public void Get_ItemByValidIndex(int expectedCount, int expectedCapacity)
        {
            //Arrange           
            var number = 1000;
            var expectedStringCollectionRepresentation = "[1000]";
            nums.Add(number);

            //Act
            var actualNumber = nums[0];
            Assertions.AssertCollections(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
            actualNumber.Should().Be(number);
        }

        [TestCase(Constants.ExpectedCountOneItem, Constants.ExpectedInitialCapacity)]
        public void Get_ItemByInvalidIndex(int expectedCount, int expectedCapacity)
        {
            //Arrange           
            var number = 10000;
            var expectedStringCollectionRepresentation = "[10000]";
            nums.Add(number);

            var invalidIdx1 = -1;
            var invalidIdx2 = nums.Count;

            //Act
            // no action or action would be taking from the collection the invalid Index, so exception is thrown

            // Assert
            Assertions.AssertCollections(expectedCount, nums, expectedCapacity, expectedStringCollectionRepresentation);
            Assert.Multiple(() =>
            {
                Assert.That(() => { var actualNum = nums[invalidIdx1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>(), "Invalid -1 Idx");

                Assert.That(() => { var numFromInvalidIndex = nums[invalidIdx2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>(), "Invalid Idx numsCount");
            });
        }

        [Test]
        public void ToString_IsSuccessful()
        {
            // Arrange            
            var expectedStringnestedCollectionRepresentation = "[[Teddy, Gerry, Sofia, Victoria], [10, 20, 30, 31], []]";

            // Act
            strings.AddRange("Teddy", "Gerry", "Sofia", "Victoria");
            nums.AddRange(10, 20, 30, 31);
            var dates = new Collection<DateTime>();
            var nestedCollection = new Collection<object>(strings, nums, dates);
            string nestedCollectionToString = nestedCollection.ToString();

            // Assert
            Assert.AreEqual(expectedStringnestedCollectionRepresentation, nestedCollectionToString);
        }

        [Test, Timeout(1000)]
        // attribute to specity timeoutvalue in ms for the testcase If test runs longer than timeout specified it is cancelled and the test case is  reported as failer
        public void Test_Collection_1MilionItems_IsSuccessful() 
        {
            // Arrange
            var expectedStringCollectionRepresentation = "[]";
            const int itemsCount = 1000000;
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            // Act
            for (int i = nums.Count - 1; i >= 0 ; i--)
            {
                nums.RemoveAt(i);
            }

            // Assert
            Assert.AreEqual(expectedStringCollectionRepresentation, nums.ToString());
            Assert.IsTrue(nums.Capacity > nums.Count);
        }
    }
}