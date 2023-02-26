namespace Collections.UnitTests
{
    public class Assertions
    {
        public static void AssertCollections<T>(
            int expectedCount, Collection<T> collection, int expectedCapacity, string expectedStringCollectionRepresentation)
        {
            Assert.Multiple(() => 
            {
                Assert.AreEqual(expectedCount, collection.Count, "CountProperty");
                Assert.AreEqual(expectedCapacity, collection.Capacity, "capacityProperty");
                Assert.AreEqual(expectedStringCollectionRepresentation, collection.ToString(), "capacityProperty");
            });              
        }
    }
} 
