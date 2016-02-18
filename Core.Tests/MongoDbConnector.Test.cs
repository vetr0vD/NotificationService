using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Core;
using Service;
using Data;

namespace Core.Tests
{
    [TestFixture]
    public class MongoDbConnectorTests
    {
        [Test]
        public void WriteData_WriteSimple()
        {
            var db = new MongoDbConnector("test");

            db.Connect();
            db.ClearData<WorkNotification>();

            var item = new WorkNotification(WorkNotificationStatus.Ready);

            db.WriteData(item);
            var result = db.ReadData(DateTime.Now);

            Assert.AreEqual(item.Status, result.Status);
            Assert.AreEqual(item.NotificationDate, result.NotificationDate);
        }

        [Test]
        public void WriteData_ReturnReadyTest()
        {
            var db = new MongoDbConnector("test");

            db.Connect();
            db.ClearData<WorkNotification>();

            var item1 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 1) };
            var item2 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 1) };
            var item3 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2015, 1, 1) };
            var item4 = new WorkNotification() { Status = WorkNotificationStatus.Ready, NotificationDate = new DateTime(2016, 1, 1) };

            db.WriteData(item1);
            db.WriteData(item2);
            db.WriteData(item4);
            db.WriteData(item3);

            var result = db.ReadData(new DateTime(2016, 1, 1));

            Assert.AreEqual(item4.Status, result.Status);
            Assert.AreEqual(item4.NotificationDate, result.NotificationDate);
        }

        [Test]
        public void WriteData_FutureDate()
        {
            var db = new MongoDbConnector("test");

            db.Connect();
            db.ClearData<WorkNotification>();

            var item1 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 1) };
            var item2 = new WorkNotification() { Status = WorkNotificationStatus.Ready, NotificationDate = new DateTime(2016, 1, 1) };
            var item3 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 2) };
            var item4 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 3) };
            var item5 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 4) };

            db.WriteData(item1);
            db.WriteData(item2);
            db.WriteData(item3);
            db.WriteData(item4);
            db.WriteData(item5);

            var result = db.ReadData(new DateTime(2016, 1, 1));

            Assert.AreEqual(item3.Status, result.Status);
            Assert.AreEqual(item3.NotificationDate, result.NotificationDate);
        }

        [Test]
        public void WriteData_FutureDateRepeat()
        {
            var db = new MongoDbConnector("test");

            db.Connect();
            db.ClearData<WorkNotification>();

            var item2 = new WorkNotification() { Status = WorkNotificationStatus.Ready, NotificationDate = new DateTime(2016, 1, 1) };
            var item3 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 2) };
            var item4 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 3) };
            var item5 = new WorkNotification() { Status = WorkNotificationStatus.Busy, NotificationDate = new DateTime(2016, 1, 4) };
            var item6 = new WorkNotification() { Status = WorkNotificationStatus.Ready, NotificationDate = new DateTime(2016, 1, 2) };

            db.WriteData(item2);
            db.WriteData(item3);
            db.WriteData(item4);
            db.WriteData(item5);
            db.WriteData(item6);

            var result = db.ReadData(new DateTime(2016, 1, 1));

            Assert.AreEqual(item4.Status, result.Status);
            Assert.AreEqual(item4.NotificationDate, result.NotificationDate);
        }
    }
}
