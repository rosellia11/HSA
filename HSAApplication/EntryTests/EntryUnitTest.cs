using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSA;
using System;

namespace EntryTests
{
    [TestClass]
    public class EntryUnitTests
    {
        //This method goes over all the cases I can think for ways to correctly and incorrectly create an entry.
        //Depending on the scenario an exception is thown or a valid entry is made and this checks that
        //this method also kills two birds with one stone because in creating the entries we are also calling 
        //checkValidDate(), checkDob and setStatus. On a larger scale project it would be best to split these into seperate
        //test but for this small of scale i think this is ok to do. ToString is also called in this method
        [TestMethod]
        public void EntryCreation()
        {
            string match1 ="Accepted,Antonio,Roselli,08221998,HSA,08222019";
            string match2 ="Accepted,Antonio,Roselli,08221998,HRA,08222019";
            string match3 ="Rejected,Tony,Gotrejected,10102010,FSA,08222019";
            string match4 ="Rejected,Bob,Noaccept,11112011,HSA,08222025";
            string match5 ="Rejected,Tim,Rejecto,08221998,HSA,12252025";
            Entry Entry1 = new Entry("Antonio", "Roselli", "08221998", "HSA", "08222019");
            Entry Entry2 = new Entry("Antonio", "Roselli", "08221998", "HRA", "08222019");
            Entry Entry3 = new Entry("Tony", "Gotrejected", "10102010", "FSA", "08222019");
            Entry Entry4 = new Entry("Bob", "Noaccept", "11112011", "HSA", "08222025");
            Entry Entry5 = new Entry("Tim", "Rejecto", "08221998", "HSA", "12252025");

            Assert.AreEqual(Entry1.ToString(), match1);
            Assert.AreEqual(Entry2.ToString(), match2);
            Assert.AreEqual(Entry3.ToString(), match3);
            Assert.AreEqual(Entry4.ToString(), match4);
            Assert.AreEqual(Entry5.ToString(), match5);
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "Roselli", "Wrong", "HSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "Roselli", "12221998", "HSA", "trash"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "Roselli", "08221998", "LOL", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "Roselli", "08222025", "HSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "Roselli", "13221998", "HSA", "08342019"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "Roselli", "08221998", "LOL", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry(null, "Roselli", "08221998", "FSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", null, "08221998", "HSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Tony", "Gotrejected", null, "HSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Bob", "Noaccept", "11112011", null, "08222025"));
            Assert.ThrowsException<Exception>(() => new Entry("Tim", "Rejecto", "08221998", "HSA", null));
            Assert.ThrowsException<Exception>(() => new Entry("", "Roselli", "08221998", "FSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Antonio", "", "08221998", "HSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Tony", "Gotrejected", "", "HSA", "08222019"));
            Assert.ThrowsException<Exception>(() => new Entry("Bob", "Noaccept", "11112011", "", "08222025"));
            Assert.ThrowsException<Exception>(() => new Entry("Tim", "Rejecto", "08221998", "HSA", ""));
        }
    }
 }
