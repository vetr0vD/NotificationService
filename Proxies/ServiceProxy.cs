using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Proxies
{
    public class ServiceProxy :
        ClientBase<IWorkLazyNotificationContract>,
        IWorkLazyNotificationContract
    {
        public Data.WorkNotification GetWorkStatus(DateTimeOffset date)
        {
            return base.Channel.GetWorkStatus(date);
        }

        public void SetWorkStatus(Data.WorkNotification notification)
        {
            base.Channel.SetWorkStatus(notification);
        }
    }
}
