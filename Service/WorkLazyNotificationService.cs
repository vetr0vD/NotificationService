using Core;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class WorkLazyNotificationService:
        IWorkLazyNotificationContract
    {
        private IDbConnector _connector;

        public WorkLazyNotificationService(): 
            this(ServiceLocator.ResolveDbConnector()) { }

        public WorkLazyNotificationService(IDbConnector connector)
        {
            _connector = connector;
        }

        #region IWorkLazyNotificationContract

        public WorkNotification GetWorkStatus(DateTimeOffset date)
        {
            _connector.Connect();
            return _connector.ReadData(date);
        }

        public void SetWorkStatus(WorkNotification notification)
        {
            _connector.Connect();
            _connector.WriteData(notification);
        }

        #endregion
    } 
}
