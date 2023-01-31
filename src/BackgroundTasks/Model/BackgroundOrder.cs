using System;

namespace BackgroundTasks.Model
{
    public class BackgroundOrder
    {
        private const int WaitingPeriodMinute = 30;
        public int OrderId { get; private set; }
        public int DeliveryId { get; private set; }
        public DateTime WaitingPeriodTime { get; }

        public BackgroundOrder(int orderId, int deliveryId)
        {
            OrderId = orderId;
            DeliveryId = deliveryId;
           // WaitingPeriodTime = DateTime.Now.AddMinutes(WaitingPeriodMinute);
            WaitingPeriodTime = DateTime.Now.AddSeconds(WaitingPeriodMinute);
        }
    }
}