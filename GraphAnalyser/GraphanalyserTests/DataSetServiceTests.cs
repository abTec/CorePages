using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace GraphanalyserTests
{
    [TestClass]
    public class DataSetServiceTests
    {
        [TestMethod]
        public async Task NumberOfUsersIsOk()
        {
            var dss = new DataSetService(new DataSetRepository(ConfigureDbContext()));
            using (var sr = new StreamReader("input.txt"))
            {
                var ds = await dss.CreateDataSetAsync(sr.BaseStream);
                Assert.IsNotNull(ds);
                Assert.AreEqual(6, ds.NumberOfUsers);
            }
        }

        [TestMethod]
        public async Task NumberOfFriendsIsOk()
        {
            var dss = new DataSetService(new DataSetRepository(ConfigureDbContext()));
            using (var sr = new StreamReader("input.txt"))
            {
                var ds = await dss.CreateDataSetAsync(sr.BaseStream);
                Assert.IsNotNull(ds);
                Assert.IsNotNull(ds.Users.FirstOrDefault(u => u.ID == 0));
                Assert.IsNotNull(ds.Users.FirstOrDefault(u => u.ID == 5));
                Assert.AreEqual(5, ds.Users.First(u => u.ID == 0).NumberOfFriends);
                Assert.AreEqual(1, ds.Users.First(u => u.ID == 5).NumberOfFriends);
            }
        }

        private DataSetContext ConfigureDbContext() => new DataSetContext(new DbContextOptionsBuilder<DataSetContext>().UseInMemoryDatabase("DataSetContext.Tests").Options);

    }
}
