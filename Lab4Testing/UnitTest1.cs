using Lab4;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace Lab4Testing
{
    public class Tests
    {
        IBusinessLogic bl;
        [SetUp]
        public void Setup()
        {
            bl = new BusinessLogic();

            bl.ClearDatabase();

            Assert.AreEqual(InvalidFieldError.NoError, bl.AddEntry("TestClue1", "TestAnswer1", 0, "01/01/01"));
            Assert.AreEqual(InvalidFieldError.NoError, bl.AddEntry("TestClue2", "TestAnswer2", 1, "02/02/02"));
            Assert.AreEqual(InvalidFieldError.NoError, bl.AddEntry("TestClue3", "TestAnswer3", 2, "03/03/03"));
            Assert.AreEqual(InvalidFieldError.NoError, bl.AddEntry("TestClue4", "TestAnswer4", 2, "04/04/04"));
        }

        [Test]
        public void TestAddEntry()
        {
            Assert.AreEqual(InvalidFieldError.NoError, bl.AddEntry("TestClue5", "TestAnswer5", 0, "05/05/05"));
        }

        [Test]
        public void TestAddEntryInvalidClue()
        {
            int id = 1;
            Assert.AreEqual(InvalidFieldError.InvalidClueLength, bl.AddEntry("", "TestAnswer1", 0, "01/01/01"));
        }

        [Test]
        public void TestAddEntryInvalidAnswer()
        {
            Assert.AreEqual(InvalidFieldError.InvalidAnswerLength, bl.AddEntry("TestClue1", "", 0, "01/01/01"));
        }

        [Test]
        public void TestAddEntryInvalidDifficulty()
        {
            Assert.AreEqual(InvalidFieldError.InvalidDifficulty, bl.AddEntry("TestClue1", "TestAnswer1", 100, "01/01/01"));
        }

        [Test]
        public void TestEditEntryValid()
        {
            int id = bl.GetNextId() - 1;
            Assert.AreEqual(EntryEditError.NoError, bl.EditEntry("ModifiedTestClue4", "ModifiedTestAnswer4", 0, "10/10/10", id));
        }

        [Test]
        public void TestEditEntryInvalidClue()
        {
            int id = bl.GetNextId() - 1;
            Assert.AreEqual(EntryEditError.InvalidFieldError, bl.EditEntry("", "ModifiedTestAnswer4", 0, "10/10/10", id));
        }

        [Test]
        public void TestEditEntryInvalidAnswer()
        {
            int id = bl.GetNextId() - 1;
            Assert.AreEqual(EntryEditError.InvalidFieldError, bl.EditEntry("ModifiedTestClue4", "", 0, "10/10/10", id));
        }

        [Test]
        public void TestEditEntryInvalidDifficulty()
        {
            int id = bl.GetNextId() - 1;
            Assert.AreEqual(EntryEditError.InvalidFieldError, bl.EditEntry("ModifiedTestClue4", "ModifiedTestAnswer4", 50, "10/10/10", id));
        }

        [Test]
        public void TestGetEntries()
        {
            ObservableCollection<Entry> testEntries = bl.GetEntries();
            Assert.IsNotEmpty(testEntries);
        }

        [Test]
        public void TestDeleteEntryValid()
        {
            int nextId = bl.GetNextId();
            int[] ids = {nextId, nextId-1, nextId-2,nextId-3};
            Assert.AreEqual(EntryDeletionError.NoError, bl.DeleteEntry(ids[0]));
            Assert.AreEqual(EntryDeletionError.NoError, bl.DeleteEntry(ids[1]));
            Assert.AreEqual(EntryDeletionError.NoError, bl.DeleteEntry(ids[2]));
            Assert.AreEqual(EntryDeletionError.NoError, bl.DeleteEntry(ids[3])); 
        } 

        [Test]
        public void TestDeleteEntryInvalid()
        {
            int badId = 100;
            Assert.AreEqual(EntryDeletionError.EntryNotFound, bl.DeleteEntry(badId));
        }
    }
}