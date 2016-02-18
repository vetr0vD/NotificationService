using Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Test
{
    [TestFixture]
    public class WorkLazyNotificationServiceTest
    {
        [Test]
        public void SimpleServerTest()
        {
            IWorkLazyNotificationContract service = new WorkLazyNotificationService();

            service.SetWorkStatus(new WorkNotification() { Status = WorkNotificationStatus.Ready });
            service.SetWorkStatus(new WorkNotification() { Status = WorkNotificationStatus.Busy });

            var result = service.GetWorkStatus(DateTime.Now);

            Assert.AreEqual(WorkNotificationStatus.Busy, result.Status);
        }
    }
}
